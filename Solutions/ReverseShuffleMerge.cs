// Reverse Shuffle Merge
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

    // Complete the reverseShuffleMerge function below.
    static string reverseShuffleMerge(string s)
    {
        int a = 'a';
        var m = 'z' - a + 1;
        var frequency = new int[m];

        for (var index = 0; index < frequency.Length; index++)
            frequency[index] = 0;

        foreach (var c in s.ToCharArray())
            frequency[c - a]++;


        var count = new int[m];

        for (var index = 0; index < frequency.Length; index++)
            count[index] = frequency[index] / 2;

        var top = -1;
        var stack = new int[s.Length];
        for (int n = s.Length; --n >= 0;)
        {
            int c = s[n] - a;
            frequency[c]--;
            if (count[c] < 1) continue;

            count[c]--;
            while (top >= 0 &&
                stack[top] > c &&
                frequency[stack[top]] > count[stack[top]])
            {
                count[stack[top--]]++; // Increment and then pop the stack
            }
            stack[++top] = c; // Push c on to the stack
        }

        return string.Concat(stack.Take(top + 1).Select(x => (char)(x + a)));
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string s = Console.ReadLine();

        string result = reverseShuffleMerge(s);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
