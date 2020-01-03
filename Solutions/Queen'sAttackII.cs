// Queen's Attack II
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

    // Complete the queensAttack function below.
    static int queensAttack(int n, int k, int r_q, int c_q, int[][] obstacles) {
        r_q--;
        c_q--;

        var obstaclesCache = new Dictionary<int, Dictionary<int, int>>();

        foreach (var obstacle in obstacles)
        {
            var row = obstacle[0] - 1;
            var col = obstacle[1] - 1;
            if (obstaclesCache.TryGetValue(row, out var list))
            {
                if (!list.ContainsKey(col))
                    list.Add(col, 1);
            }
            else
            {
                list = new Dictionary<int, int>
                {
                    { col, 1 }
                };
                obstaclesCache.Add(row, list);
            }
        }

        Func<int, int, bool> isValid = (int r, int c) =>
        {
            return
                r >= 0 &&
                c >= 0 &&
                r < n &&
                c < n &&
                (
                    !obstaclesCache.TryGetValue(r, out var cols) ||
                    !cols.ContainsKey(c)
                );
        };

        var rowsOffset = new[] { -1, -1, -1, 0, 0, 1, 1, 1 };
        var colsOffset = new[] { -1, 0, 1, -1, 1, -1, 0, 1 };

        var result = 0;

        for (int index = 0; index < rowsOffset.Length; index++)
        {
            var counter = 1;
            while (isValid(r_q + counter * rowsOffset[index], c_q + counter * colsOffset[index]))
            {
                counter++;
                result++;
            }
        }

        return result;
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] nk = Console.ReadLine().Split(' ');

        int n = Convert.ToInt32(nk[0]);

        int k = Convert.ToInt32(nk[1]);

        string[] r_qC_q = Console.ReadLine().Split(' ');

        int r_q = Convert.ToInt32(r_qC_q[0]);

        int c_q = Convert.ToInt32(r_qC_q[1]);

        int[][] obstacles = new int[k][];

        for (int i = 0; i < k; i++) {
            obstacles[i] = Array.ConvertAll(Console.ReadLine().Split(' '), obstaclesTemp => Convert.ToInt32(obstaclesTemp));
        }

        int result = queensAttack(n, k, r_q, c_q, obstacles);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
