// Crossword Puzzle
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Solution {

    // Complete the crosswordPuzzle function below.
    static string[] crosswordPuzzle(string[] crossword, string words)
    {
        var wordsArray = words.Split(';').ToArray();
        if (TryFill(crossword.Select(x => x.Replace("-", ".")).ToArray(), wordsArray, out var result))
            return result;
        else
            throw new Exception("Could not find a solution.");
    }

    static bool TryFill(
        string[] crossword,
        string[] words,
        out string[] result)
    {
        result = crossword.Select(x => x).ToArray();
        var regex = new Regex("[^\\+]*\\.[^\\+]*");

        var colCount = crossword[0].Length;
        var rowCount = crossword.Length;

        var pivotCrossword = new string[colCount];

        for (var i = 0; i < rowCount; i++)
            for (var j = 0; j < colCount; j++)
                pivotCrossword[j] = (pivotCrossword[j] ?? "") + crossword[i][j];

        for (var index = 0; index < rowCount; index++)
        {
            foreach (Match match in regex.Matches(result[index]))
            {
                var matchRegex = new Regex(match.Value);
                foreach (var word in words.Where(x => x.Length == match.Value.Length && matchRegex.IsMatch(x)))
                {
                    var temp = result[index];
                    for (var j = 0; j < match.Length; j++)
                        result[index] = Replace(result[index], match.Index + j, word[j]);

                    if (TryFill(result, words.Where(x => x != word).ToArray(), out var grandResult))
                    {
                        result = grandResult;
                        return true;
                    }
                    else
                    {
                        result[index] = temp;
                    }

                    //for (int j = 0; j < match.Length; j++)
                    //    pivotCrossword[match.Index + j] = Replace(
                    //        pivotCrossword[match.Index + j], 
                    //        index + j, 
                    //        word[j]);
                }
            }

            foreach (Match match in regex.Matches(pivotCrossword[index]))
            {
                var matchRegex = new Regex(match.Value);
                foreach (var word in words.Where(x => x.Length == match.Value.Length && matchRegex.IsMatch(x)))
                {
                    var temp = "";

                    for (var j = 0; j < match.Length; j++)
                    {
                        temp += match.Value[j];
                        result[match.Index + j] = Replace(
                            result[match.Index + j],
                            index,
                            word[j]);
                    }

                    if (TryFill(result, words.Where(x => x != word).ToArray(), out var grandResult))
                    {
                        result = grandResult;
                        return true;
                    }
                    else
                    {
                        for (var j = 0; j < match.Length; j++)
                        {
                            temp += pivotCrossword[match.Index + j];
                            result[match.Index + j] = Replace(
                                result[match.Index + j],
                                index + j,
                                temp[j]);
                        }
                    }
                }
            }
        }

        return !result.Any(x => regex.Matches(x).Count > 0);
    }

    static string Replace(string input, int index, char @char)
    {
        var array = input.ToCharArray();
        array[index] = @char;
        return string.Concat(array);
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] crossword = new string [10];

        for (int i = 0; i < 10; i++) {
            string crosswordItem = Console.ReadLine();
            crossword[i] = crosswordItem;
        }

        string words = Console.ReadLine();

        string[] result = crosswordPuzzle(crossword, words);

        textWriter.WriteLine(string.Join("\n", result));

        textWriter.Flush();
        textWriter.Close();
    }
}
