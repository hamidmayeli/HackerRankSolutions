// Flipping the Matrix
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

    // Complete the flippingMatrix function below.
    static int flippingMatrix(int[][] matrix)
    {
        var sum = 0;
        var n = matrix.Length / 2;
        for (var row = 0; row < n; row++)
        {
            for (var col = 0; col < n; col++)
            {
                sum += getMaxPossibleFor(matrix, row, col);
            }
        }

        return sum;
    }

    static int getMaxPossibleFor(int[][] matrix, int row, int col)
    {
        var n2 = matrix.Length - 1;
        return Math.Max(
            Math.Max(
                matrix[row][col],
                matrix[n2 - row][col]),

            Math.Max(
                matrix[row][n2 - col],
                matrix[n2 - row][n2 - col])
        );
    }

    static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int q = Convert.ToInt32(Console.ReadLine());

        for (int qItr = 0; qItr < q; qItr++)
        {
            int n = Convert.ToInt32(Console.ReadLine());

            int[][] matrix = new int[2 * n][];

            for (int i = 0; i < 2 * n; i++)
            {
                matrix[i] = Array.ConvertAll(Console.ReadLine().Split(' '), matrixTemp => Convert.ToInt32(matrixTemp));
            }

            int result = flippingMatrix(matrix);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
