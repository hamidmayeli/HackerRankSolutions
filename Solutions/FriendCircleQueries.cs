// Friend Circle Queries
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

    // Complete the maxCircle function below.
    static int[] maxCircle(int[][] queries)
    {
        var ds = new DisjointSets();
        var result = new int[queries.Length];

        var max = 0;

        for (int index = 0; index < queries.Length; index++)
        {
            var left = ds.FindOrAdd(queries[index][0]);
            var right = ds.FindOrAdd(queries[index][1]);

            max = Math.Max(max, ds.Union(left, right));
            result[index] = max;
        }

        return result;
    }

    class DisjointSets
    {
        Dictionary<int, int> Parents = new Dictionary<int, int>();
        Dictionary<int, int> Counts = new Dictionary<int, int>();
        Dictionary<int, int> Level = new Dictionary<int, int>();

        public void AddNew(int key)
        {
            Parents.Add(key, key);
            Counts.Add(key, 1);
            Level.Add(key, 1);
        }

        public int Find(int value) => Parents[value] == value ? value : Find(Parents[value]);

        public int FindOrAdd(int value)
        {
            if(Parents.ContainsKey(value))
                return Find(value);
            
            AddNew(value);
            return value;
        }

        public int Union(int left, int right)
        {
            if(left == right) return Counts[left];

            int newLevel;
            if (Level[left] > Level[right])
            {
                newLevel = Level[left];
            }
            else if (Level[left] < Level[right])
            {
                newLevel = Level[right];
                var temp = left;
                left = right;
                right = temp;
            }
            else
            {
                newLevel = Level[left] + 1;
            }

            Parents[right] = left;
            Counts[left] = Counts[left] + Counts[right];
            Level[left] = newLevel;

            return Counts[left];
        }
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int q = Convert.ToInt32(Console.ReadLine());

        int[][] queries = new int[q][];

        for (int i = 0; i < q; i++) {
            queries[i] = Array.ConvertAll(Console.ReadLine().Split(' '), queriesTemp => Convert.ToInt32(queriesTemp));
        }

        int[] ans = maxCircle(queries);

        textWriter.WriteLine(string.Join("\n", ans));

        textWriter.Flush();
        textWriter.Close();
    }
}
