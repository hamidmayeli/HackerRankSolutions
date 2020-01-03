// Poisonous Plants
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

    // Complete the poisonousPlants function below.
    static int poisonousPlants(int[] p) {
        var deletedOnDay = new int[p.Length];
        var min = p[0];
        var max = 0;
        var stack = new Stack<int>();

        stack.Push(0);

        for (var x = 1; x < p.Length; x++)
        {
            if (p[x] > p[x - 1])
                deletedOnDay[x] = 1;

            min = Math.Min(min, p[x]);

            while (stack.Count() > 0 && p[stack.Peek()] >= p[x])
            {
                if (p[x] > min)
                    deletedOnDay[x] = Math.Max(deletedOnDay[x], deletedOnDay[stack.Peek()] + 1);

                stack.Pop();
            }

            max = Math.Max(max, deletedOnDay[x]);
            stack.Push(x);
        }

        return max;
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine());

        int[] p = Array.ConvertAll(Console.ReadLine().Split(' '), pTemp => Convert.ToInt32(pTemp))
        ;
        int result = poisonousPlants(p);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
