// https://www.hackerrank.com/challenges/the-quickest-way-up/problem
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

    // Complete the quickestWayUp function below.
    static int quickestWayUp(int[][] ladders, int[][] snakes)
    {
        var jumps = new SortedList<int, int>();

        foreach (var item in ladders)
            jumps.Add(item[0] - 1, item[1] - 1);

        foreach (var item in snakes)
            jumps.Add(item[0] - 1, item[1] - 1);

        var board = new int[100];
        var visited = new bool[100];

        var queue = new Queue<int>();
        queue.Enqueue(0);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (visited[current]) continue;

            visited[current] = true;

            for (int index = 1; index <= 6; index++)
            {
                var next  = current + index;

                if (board[next] > 0) continue;

                var nextCount = board[current] + 1;

                if (jumps.ContainsKey(next))
                {
                    visited[next] = true;
                    board[next] = nextCount;
                    next = jumps[next];
                }

                if(next >= 99) return nextCount;

                board[next] = nextCount;
                queue.Enqueue(next);
            }
        }

        return -1;
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int t = Convert.ToInt32(Console.ReadLine());

        for (int tItr = 0; tItr < t; tItr++) {
            int n = Convert.ToInt32(Console.ReadLine());

            int[][] ladders = new int[n][];

            for (int i = 0; i < n; i++) {
                ladders[i] = Array.ConvertAll(Console.ReadLine().Split(' '), laddersTemp => Convert.ToInt32(laddersTemp));
            }

            int m = Convert.ToInt32(Console.ReadLine());

            int[][] snakes = new int[m][];

            for (int i = 0; i < m; i++) {
                snakes[i] = Array.ConvertAll(Console.ReadLine().Split(' '), snakesTemp => Convert.ToInt32(snakesTemp));
            }

            int result = quickestWayUp(ladders, snakes);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
