// https://www.hackerrank.com/challenges/matrix-rotation-algo/problem
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

    // Complete the matrixRotation function below.
    static void matrixRotation(List<List<int>> matrix, int r)
    {
        var ringsCount = Math.Min(matrix.Count, matrix[0].Count) / 2;

        for (var index = 0; index < ringsCount; index++)
            Rotate(matrix, index, r);

        var sbRow = new StringBuilder();

        for (var row = 0; row < matrix.Count; row++)
        {
            var sbCol = new StringBuilder();

            for (var col = 0; col < matrix[0].Count; col++)
                sbCol.Append(" " + matrix[row][col]);

            sbRow.AppendLine(sbCol.ToString().Trim());
        }

        Console.Write(sbRow.ToString());
    }

    private static void Rotate(List<List<int>> matrix, int ringOffset, int rotationCount)
    {
        var rowMoves = new[] { 0, 1, 0, -1 };
        var colMoves = new[] { 1, 0, -1, 0 };
        var effectiveMove = 0;

        var rowsCount = matrix.Count - 2 * ringOffset;
        var colsCount = matrix[0].Count - 2 * ringOffset;

        var itemsCount = 2 * (rowsCount + colsCount - 2);

        var map = new Data[itemsCount];
        var values = new int[itemsCount];

        map[0] = new Data(ringOffset, ringOffset);
        values[0] = matrix[ringOffset][ringOffset];

        for (var index = 1; index < itemsCount; index++)
        {
            var row = map[index - 1].Row + rowMoves[effectiveMove];
            var col = map[index - 1].Col + colMoves[effectiveMove];

            if (IsInvalid(row, col, rowsCount, colsCount, ringOffset))
            {
                effectiveMove++;
                row = map[index - 1].Row + rowMoves[effectiveMove];
                col = map[index - 1].Col + colMoves[effectiveMove];
            }

            map[index] = new Data(row, col);
            values[index] = matrix[row][col];
        }

        for (var index = 0; index < itemsCount; index++)
        {
            var effective = (itemsCount - (rotationCount % itemsCount) + index) % itemsCount;
            var data = map[effective];
            matrix[data.Row][data.Col] = values[index];
        }
    }

    private static bool IsInvalid(int newRow, int newCol, int rowsCount, int colsCount, int ringOffset)
    {
        return newRow < ringOffset ||
            newCol < ringOffset ||
            newRow - ringOffset >= rowsCount ||
            newCol - ringOffset >= colsCount;
    }

    class Data
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Data(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }

    static void Main(string[] args)
    {
        string[] mnr = Console.ReadLine().TrimEnd().Split(' ');

        int m = Convert.ToInt32(mnr[0]);

        int n = Convert.ToInt32(mnr[1]);

        int r = Convert.ToInt32(mnr[2]);

        List<List<int>> matrix = new List<List<int>>();

        for (int i = 0; i < m; i++)
        {
            matrix.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(matrixTemp => Convert.ToInt32(matrixTemp)).ToList());
        }

        matrixRotation(matrix, r);
    }
}
