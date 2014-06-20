using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BuildingBlocks.Blocks
{
    public static class StructureReader
    {

        private static string levelName;

        //returns a level read as Color[z][y][x]
        /// <exception cref="FormatException">thrown because the format is wrong, or the implementation of the documentation (see maps/readme.txt).</exception>
        public static Color?[, ,] loadLevel(string specificLevelPath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(specificLevelPath))
                {
                    Color?[][][] level = readLevel(sr);
                    return ArrayToOtherArray(level, level.Length);
                }
            }
            catch (System.FormatException e)
            {
                throw new System.FormatException(e.Message);
                //EditorUtility.DisplayDialog("whoops", "a faulty puzzle was loaded, check formatting. errormessage: "+e.Message+"\n"+e.StackTrace, "Ok");
            }


        }

        public static Color?[, ,] loadRandomLevelFromMaps()
        {
            string directoryPath = Application.dataPath + "/maps/";
            string[] maps = Directory.GetFiles(directoryPath);


            List<string> mapsList = new List<string>();
            foreach (string map in maps)
            {
                mapsList.Add(map);
            }

            //ArrayList.
            int times = maps.Length;

            //string map = getRandomMap (maps);



            while (mapsList.Count != 0)
            {
                string mapCandidate = getAndRemoveRandomMap(mapsList);
                if (mapCandidate.Contains(".structure"))
                {
                    if (!mapCandidate.Contains(".meta"))
                        return loadLevel(mapCandidate);
                }
            }

            throw new System.ExecutionEngineException("loadRandomLevel failed to load random level, do you have levels?");
        }

        private static Color?[, ,] ArrayToOtherArray(Color?[][][] array, int size)
        {
            Color?[, ,] OtherArray = new Color?[size, size, size];

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    for (int z = 0; z < size; z++)
                    {
                        //due to dimensional view differences and wanting a logical format for .structure
                        //these dimensions are interchanged asswell.
                        OtherArray[y, x, z] = array[x][y][z];
                    }
                }
            }

            return OtherArray;
        }

        private static string getAndRemoveRandomMap(List<string> maps)
        {
            int random = Mathf.FloorToInt(Random.Range(0, maps.Count));

            string map = maps[random];
            maps.RemoveAt(random);

            return map;
        }

        private static Color?[][][] readLevel(StreamReader sr)
        {
            int size = readSizeOption(sr.ReadLine());

            string test = sr.ReadToEnd();
            //EditorUtility.DisplayDialog ("info", test+"show it", "ok");

            //find Size times a pattern according to ["anything without [ or ]"]
            string[] blocks = findAndMatchAsSingleLine(test, "(?:\\[([^\\[\\]]*)\\])", size);

            Color?[][][] level = new Color?[size][][];

            for (int i = 0; i < size; i++)
            {
                level[i] = readBlock(blocks[i], size);
            }

            return level;
        }

        private static int readSizeOption(string sizeOption)
        {
            //read the xx from [size=xx]
            return int.Parse(findAndMatch(sizeOption, "\\[size=(\\d*)\\]"));
            //return 10;
        }

        private static Color?[][] readBlock(string block, int size)
        {
            //find Size times a pattern according to {"anything without { or }"}
            string[] rows = findAndMatchAsSingleLine(block, "(?:\\{([^\\{\\}])*\\})", size);

            Color?[][] result = new Color?[size][];

            //fill the y dimension with x dimension array
            for (int i = 0; i < size; i++)
            {
                result[i] = readRow(rows[i], size);
            }

            return result;

        }

        //finds the characters, and returns their respective colors
        private static Color?[] readRow(string row, int size)
        {
            //find Size times a pattern according to "any lowercase letter or number"
            string[] colorChars = findAndMatchAsSingleLine(row, "([0-9a-z])+", size);

            Color?[] result = new Color?[size];

            for (int i = 0; i < size; i++)
            {
                result[i] = ColorModel.matchColor(char.Parse(colorChars[i]));
                if (char.Parse(colorChars[i]) == ColorModel.NONECHAR)
                    result[i] = null;

            }

            return result;
        }

        private static string findAndMatch(string text, string pattern)
        {
            Match match = Regex.Match(text, pattern);

            if (match.Success)
            {
                return "" + match.Groups[1].Value;
            }
            else
            {
                throw new System.FormatException("invalid value found in " + text + "using: " + pattern);
            }
        }

        private static string[] findAndMatchAsSingleLine(string text, string pattern, int times)
        {
            MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.Singleline);
            string[] results = new string[times];

            if (matches.Count != times)
            {
                throw new System.FormatException("did not find as many groups as expected in regex expression, found: " + matches.Count + "but expected: " + times);
            }

            //if a match is found, and the correct number of matches are found
            for (int i = 0; i < times; i++)
            {
                Match match = matches[i];
                if (match.Success)
                {// && match.Groups.Count == times) {
                    results[i] = match.Groups[0].Value;
                }
                else
                {
                    if (!match.Success)
                        throw new System.FormatException("did not succeed in matching pattern");

                    throw new System.FormatException("invalid value found in " + text + "using: " + pattern + "count-times = " + (match.Groups.Count - times) + " " + match.Success);
                }
            }
            return results;
        }
    }
}
