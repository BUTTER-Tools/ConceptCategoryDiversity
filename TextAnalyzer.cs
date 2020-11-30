using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptCatDiv
{
    class TextAnalyzer
    {


        private DictionaryData dictData;


        public TextAnalyzer(DictionaryData dictionaryData)
        {
            dictData = dictionaryData;
        }





        public Dictionary<string, string> Analyze(string[] rawWords, bool outputCapturedText, bool outputRawCategoryCounts)
        {
            Dictionary<string, string> results = new Dictionary<string, string>();

            string[] Words = rawWords.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            results.Add("TokenCount", Words.Length.ToString());
            results.Add("TTR_Overall", ((Words.Distinct().Count() / (double)Words.Length) * 100).ToString());


            #region set up all of the variables we need to score results
            Dictionary<string, ulong> DictionaryResults = new Dictionary<string, ulong>();
            foreach (string Concept in dictData.ConceptMap.Keys) DictionaryResults.Add(Concept, 0);
            ulong NumberOfMatches = 0;
            StringBuilder CapturedText = new StringBuilder();
            List<string> NonmatchedTokens = new List<string>();
            #endregion 



            #region actually look for matches
            //now we actually get down to analyzing
            for (int i = 0; i < Words.Length; i++)
            {
                bool TokenMatched = false;
                //iterate over n-grams, starting with the largest possible n-gram (derived from the user's dictionary file)
                for (int NumberOfWords = dictData.MaxWords; NumberOfWords > 0; NumberOfWords--)
                {

                    //make sure that we don't overextend past the array
                    if (i + NumberOfWords - 1 >= Words.Length) continue;


                    //make the target string
                    string TargetString;
                    if (NumberOfWords > 1)
                    {
                        TargetString = String.Join(" ", Words.Skip(i).Take(NumberOfWords).ToArray());
                    }
                    else
                    {
                        TargetString = Words[i];
                    }


                    //look for an exact match

                    if (dictData.FullDictionaryMap["Standards"].ContainsKey(NumberOfWords))
                    {
                        if (dictData.FullDictionaryMap["Standards"][NumberOfWords].ContainsKey(TargetString))
                        {

                            //add in the number of words found
                            NumberOfMatches += (ulong)NumberOfWords;

                            //increment results
                            DictionaryResults[dictData.FullDictionaryMap["Standards"][NumberOfWords][TargetString]] += 1;


                            //manually increment the for loop so that we're not testing on words that have already been picked up
                            i += NumberOfWords - 1;
                            //break out of the lower level for loop back to moving on to new words altogether
                            TokenMatched = true;

                            if (outputCapturedText) CapturedText.Append(TargetString + " ");

                            break;
                        }
                    }
                    //if there isn't an exact match, we have to go through the wildcards
                    if (dictData.WildCardArrays.ContainsKey(NumberOfWords))
                    {
                        for (int j = 0; j < dictData.WildCardArrays[NumberOfWords].Length; j++)
                        {
                            if (dictData.PrecompiledWildcards[dictData.WildCardArrays[NumberOfWords][j]].Matches(TargetString).Count > 0)
                            {

                                //add in the number of words found
                                NumberOfMatches += (ulong)NumberOfWords;

                                //increment results
                                DictionaryResults[dictData.FullDictionaryMap["Wildcards"][NumberOfWords][dictData.WildCardArrays[NumberOfWords][j]]] += 1;

                                //manually increment the for loop so that we're not testing on words that have already been picked up
                                i += NumberOfWords - 1;
                                //break out of the lower level for loop back to moving on to new words altogether
                                TokenMatched = true;

                                if (outputCapturedText) CapturedText.Append(TargetString + " ");

                                break;

                            }
                        }
                    }


                }

                //this is what we do if we didn't find any match in our dictionary
                if (!TokenMatched) NonmatchedTokens.Add(Words[i]);

            }
            #endregion







            results.Add("CapturedText", CapturedText.ToString());
            results.Add("DictPct", (((double)NumberOfMatches / Words.Length) * 100).ToString());
            results.Add("TTR_NonDict", (((double)NonmatchedTokens.Distinct().Count() / NonmatchedTokens.Count()) * 100).ToString());




            #region aggregate result data
            //pull together the results here
            Dictionary<string, ulong[]> CompiledResults = new Dictionary<string, ulong[]>();
            foreach (string TopLevelCategory in dictData.CatNames)
            {
                CompiledResults.Add(TopLevelCategory, new ulong[2] { 0, 0 });
            }

            foreach (string ConceptKey in dictData.ConceptMap.Keys)
            {
                if (DictionaryResults[ConceptKey] > 0)
                {
                    for (int i = 0; i < dictData.ConceptMap[ConceptKey].Length; i++)
                    {
                        //if the Concept was found in the text, increment the first index (i.e., the number of unique concepts) by 1
                        CompiledResults[dictData.ConceptMap[ConceptKey][i]][0] += 1;
                        //if the Concept was found in the text, add the number of times it occurred
                        CompiledResults[dictData.ConceptMap[ConceptKey][i]][1] += DictionaryResults[ConceptKey];
                    }
                }
            }
            #endregion


            #region calculate CWR scores
            //this is where we actually calulate and output the CWR scores
            for (int i = 0; i < dictData.CategoryOrder.Count; i++)
            {
                if (Words.Length > 0)
                {
                    results.Add(dictData.CatNames[i] + "_CWR", (((double)CompiledResults[dictData.CategoryOrder[i]][0] / Words.Length) * 100.0).ToString());
                }
                else
                {
                    results.Add(dictData.CatNames[i] + "_CWR", "");
                }
            }
            #endregion

            #region calculate CCR scores
            //this is where we actually calulate and output the CCR scores
            for (int i = 0; i < dictData.CategoryOrder.Count; i++)
            {
                if (CompiledResults[dictData.CategoryOrder[i]][0] > 0)
                {
                    results.Add(dictData.CatNames[i] + "_CCR", (((double)CompiledResults[dictData.CategoryOrder[i]][0] / CompiledResults[dictData.CategoryOrder[i]][1]) * 100.0).ToString());
                }
                else
                {
                    results.Add(dictData.CatNames[i] + "_CCR", "");
                }
            }
            #endregion

            //this is if the user asked for the raw counts per category
            if (outputRawCategoryCounts)
            {

                for (int i = 0; i < dictData.CategoryOrder.Count; i++)
                {

                    results.Add(dictData.CatNames[i] + "_Count",  CompiledResults[dictData.CategoryOrder[i]][1].ToString());
                    results.Add(dictData.CatNames[i] + "_Unique", CompiledResults[dictData.CategoryOrder[i]][0].ToString());

                }


            }



            return results;

        }




    }
}
