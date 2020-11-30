using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace ConceptCatDiv
{
    internal class DictDataLoader
    {

        public DictionaryData LoadDictionaryFile(string InputFile, System.Text.Encoding SelectedEncoding, char CSVDelimiter, char CSVQuote)
        {


            string rawDictionaryText = "";



            using (var stream = File.OpenRead(InputFile))
            using (var reader = new StreamReader(stream, encoding: SelectedEncoding))
            {

                rawDictionaryText = reader.ReadToEnd();


            }


            return ParseDictionary(rawDictionaryText, CSVDelimiter, CSVQuote);
            

        }




        internal DictionaryData ParseDictionary(string rawDictionaryText, char CSVDelimiter, char CSVQuote)
        {





            DictionaryData dictionaryData = new DictionaryData();

            dictionaryData.RawDictionaryText = rawDictionaryText;


            //parse out the the dictionary file
            dictionaryData.MaxWords = 0;

            //yeah, there's levels to this thing
            dictionaryData.FullDictionaryMap = new Dictionary<string, Dictionary<int, Dictionary<string, string>>>();

            dictionaryData.FullDictionaryMap.Add("Wildcards", new Dictionary<int, Dictionary<string, string>>());
            dictionaryData.FullDictionaryMap.Add("Standards", new Dictionary<int, Dictionary<string, string>>());

            dictionaryData.WildCardArrays = new Dictionary<int, string[]>();
            dictionaryData.PrecompiledWildcards = new Dictionary<string, Regex>();


            Dictionary<int, List<string>> WildCardLists = new Dictionary<int, List<string>>();

            dictionaryData.ConceptMap = new Dictionary<string, string[]>();







            using (var reader = new StringReader(rawDictionaryText))
            {

                //  ____                   _       _         ____  _      _   ____        _           ___  _     _           _   
                // |  _ \ ___  _ __  _   _| | __ _| |_ ___  |  _ \(_) ___| |_|  _ \  __ _| |_ __ _   / _ \| |__ (_) ___  ___| |_ 
                // | |_) / _ \| '_ \| | | | |/ _` | __/ _ \ | | | | |/ __| __| | | |/ _` | __/ _` | | | | | '_ \| |/ _ \/ __| __|
                // |  __/ (_) | |_) | |_| | | (_| | ||  __/ | |_| | | (__| |_| |_| | (_| | || (_| | | |_| | |_) | |  __/ (__| |_ 
                // |_|   \___/| .__/ \__,_|_|\__,_|\__\___| |____/|_|\___|\__|____/ \__,_|\__\__,_|  \___/|_.__// |\___|\___|\__|
                //            |_|                                                                             |__/               


                var data = CsvParser.ParseHeadAndTail(reader, CSVDelimiter, CSVQuote);

                var header = data.Item1;
                var lines = data.Item2;



                dictionaryData.NumCats = header.Count - 1;

                //now that we know the number of categories, we can fill out the arrays
                dictionaryData.CatNames = new string[dictionaryData.NumCats];
                //DictData.CatValues = new string[DictData.NumCats];

                dictionaryData.CategoryOrder = new Dictionary<int, string>();
                //Map Out the Categories
                for (int i = 1; i < dictionaryData.NumCats + 1; i++)
                {
                    dictionaryData.CatNames[i - 1] = header[i];
                    dictionaryData.CategoryOrder.Add(i - 1, header[i]);
                }


                foreach (var lineobject in lines)
                {
                    string[] line = (string[])lineobject.ToArray();

                    string[] WordsInLine = line[0].Trim().Split('|');

                    string Concept = WordsInLine[0];

                    //set the new item into our conceptmap dictionary
                    string[] CategoriesArray = line.Skip(1).Take(line.Length - 1).ToArray();


                    dictionaryData.ConceptMap.Add(WordsInLine[0], new string[] { });

                    //fill out the list of concepts associated with each word
                    for (int i = 0; i < CategoriesArray.Length; i++)
                    {

                        if (!String.IsNullOrWhiteSpace(CategoriesArray[i].Trim()))
                        {
                            var obj = dictionaryData.ConceptMap[Concept];
                            Array.Resize(ref obj, obj.Length + 1);
                            dictionaryData.ConceptMap[Concept] = obj;
                            dictionaryData.ConceptMap[Concept][obj.Length - 1] = dictionaryData.CatNames[i];
                        }
                    }

                    //now we add the actual entries for each word in the row into our FullDictionary
                    foreach (string WordToCode in WordsInLine)
                    {
                        string WordToCodeTrimmed = WordToCode.Trim();

                        int Words_In_Entry = WordToCodeTrimmed.Split(' ').Length;
                        if (Words_In_Entry > dictionaryData.MaxWords) dictionaryData.MaxWords = Words_In_Entry;


                        if (WordToCodeTrimmed.Contains("*"))
                        {

                            if (dictionaryData.FullDictionaryMap["Wildcards"].ContainsKey(Words_In_Entry))
                            {
                                dictionaryData.FullDictionaryMap["Wildcards"][Words_In_Entry].Add(WordToCodeTrimmed.ToLower(), Concept);
                                WildCardLists[Words_In_Entry].Add(WordToCodeTrimmed);
                            }
                            else
                            {
                                dictionaryData.FullDictionaryMap["Wildcards"].Add(Words_In_Entry, new Dictionary<string, string> { { WordToCodeTrimmed.ToLower(), Concept } });
                                WildCardLists.Add(Words_In_Entry, new List<string>());
                                WildCardLists[Words_In_Entry].Add(WordToCodeTrimmed);

                            }


                            dictionaryData.PrecompiledWildcards.Add(WordToCodeTrimmed.ToLower(), new Regex("^" + Regex.Escape(WordToCodeTrimmed.ToLower()).Replace("\\*", ".*"), RegexOptions.Compiled));

                        }
                        else
                        {
                            if (dictionaryData.FullDictionaryMap["Standards"].ContainsKey(Words_In_Entry))
                            {
                                dictionaryData.FullDictionaryMap["Standards"][Words_In_Entry].Add(WordToCodeTrimmed.ToLower(), Concept);
                            }
                            else
                            {
                                dictionaryData.FullDictionaryMap["Standards"].Add(Words_In_Entry, new Dictionary<string, string> { { WordToCodeTrimmed.ToLower(), Concept } });
                            }

                        }

                    }


                }

                for (int i = dictionaryData.MaxWords; i > 0; i--)
                {
                    if (WildCardLists.ContainsKey(i)) dictionaryData.WildCardArrays.Add(i, WildCardLists[i].ToArray());
                }
                WildCardLists.Clear();
                dictionaryData.DictionaryLoaded = true;


            }


            return dictionaryData;


        }






    }
}
