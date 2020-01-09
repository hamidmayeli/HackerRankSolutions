// https://www.hackerrank.com/challenges/3d-surface-area/problem
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

    // Complete the surfaceArea function below.
    static int surfaceArea(int[][] A)
    {
        var sum = 0;

        for (var row = 0; row < A.Length; row++)
            for (var col = 0; col < A[0].Length; col++)
                sum += Count(A, row, col);

        return sum;
    }

    private static int Count(int[][] a, int row, int col)
    {
        var sum = 2; // Top and Down sides

        var rowDelta = new[] { -1, 0, 1, 0 };
        var colDelta = new[] { 0, 1, 0, -1 };

        for (var index = 0; index < rowDelta.Length; index++)
        {
            var r = row + rowDelta[index];
            var c = col + colDelta[index];
            if (IsValid(a, r, c))
                sum += Math.Max(0, a[row][col] - a[r][c]);
            else
                sum += a[row][col];
        }

        return sum;
    }

    private static bool IsValid(int[][] a, int row, int col)
    {
        return row >= 0 && col >= 0 && row < a.Length && col < a[0].Length;
    }

    static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] HW = Console.ReadLine().Split(' ');

        int H = Convert.ToInt32(HW[0]);

        int W = Convert.ToInt32(HW[1]);

        int[][] A = new int[H][];

        for (int i = 0; i < H; i++)
        {
            A[i] = Array.ConvertAll(Console.ReadLine().Split(' '), ATemp => Convert.ToInt32(ATemp));
        }

        int result = surfaceArea(A);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
