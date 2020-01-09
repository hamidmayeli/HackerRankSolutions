// https://www.hackerrank.com/challenges/magic-square-forming/problem
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

class Solution
{

    // Complete the formingMagicSquare function below.
    static int formingMagicSquare(int[][] s)
    {
        var types = new List<int[,]>{new int[,]
        {
            { 8, 1, 6 },
            { 3, 5, 7 },
            { 4, 9, 2 },
        }};

        types.Add(Flip(types[0]));

        types.AddRange(Rotate(types[0], 4));
        types.AddRange(Rotate(types[1], 4));

        var min = int.MaxValue;

        foreach (var item in types)
        {
            var sum = 0;

            for (var row = 0; row < 3; row++)
                for (var col = 0; col < 3; col++)
                    sum += Math.Abs(item[row, col] - s[row][col]);

            min = Math.Min(sum, min);
        }

        return min;
    }

    private static IEnumerable<int[,]> Rotate(int[,] source, int count)
    {
        if (count == 0) yield break;

        var result = new int[3, 3];

        for (var row = 0; row < 3; row++)
            for (var col = 0; col < 3; col++)
                result[row, col] = source[col, 2 - row];

        yield return result;

        foreach (var item in Rotate(result, count - 1))
            yield return item;
    }

    static int[,] Flip(int[,] source)
    {
        var result = new int[3, 3];

        for (var row = 0; row < 3; row++)
            for (var col = 0; col < 3; col++)
                result[row, col] = source[row, 2 - col];

        return result;
    }

    static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int[][] s = new int[3][];

        for (int i = 0; i < 3; i++)
        {
            s[i] = Array.ConvertAll(Console.ReadLine().Split(' '), sTemp => Convert.ToInt32(sTemp));
        }

        int result = formingMagicSquare(s);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
