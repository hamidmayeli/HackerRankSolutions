// https://www.hackerrank.com/challenges/the-grid-search/problem
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

    // Complete the gridSearch function below.
    static string gridSearch(string[] G, string[] P)
    {
        var firsLine = P[0];

        for (var row = 0; row < G.Length; row++)
        {
            var indexOf = G[row].IndexOf(firsLine);

            while (indexOf >= 0)
            {
                if (IsMatch(G, P, row, indexOf))
                    return "YES";

                indexOf = G[row].IndexOf(firsLine, indexOf + 1);
            }
        }

        return "NO";
    }

    private static bool IsMatch(string[] g, string[] p, int firstRow, int firstCol)
    {
        if (firstCol + p[0].Length > g[0].Length ||
            firstRow + p.Length > g.Length) return false;

        for (var row = 0; row < p.Length; row++)
            for (var col = 0; col < p[row].Length; col++)
                if (g[row + firstRow][col + firstCol] != p[row][col])
                    return false;

        return true;
    }

    static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int t = Convert.ToInt32(Console.ReadLine());

        for (int tItr = 0; tItr < t; tItr++)
        {
            string[] RC = Console.ReadLine().Split(' ');

            int R = Convert.ToInt32(RC[0]);

            int C = Convert.ToInt32(RC[1]);

            string[] G = new string[R];

            for (int i = 0; i < R; i++)
            {
                string GItem = Console.ReadLine();
                G[i] = GItem;
            }

            string[] rc = Console.ReadLine().Split(' ');

            int r = Convert.ToInt32(rc[0]);

            int c = Convert.ToInt32(rc[1]);

            string[] P = new string[r];

            for (int i = 0; i < r; i++)
            {
                string PItem = Console.ReadLine();
                P[i] = PItem;
            }

            string result = gridSearch(G, P);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
