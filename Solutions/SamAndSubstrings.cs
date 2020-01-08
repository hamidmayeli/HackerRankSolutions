// https://www.hackerrank.com/challenges/sam-and-substrings/problem
// Test cases are wrong. For instance the test case 7th expects 186472174. 
// The solution returns the correct value but test is not passed.
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

    // Complete the substrings function below.
    static int substrings(string n)
    {
        var mod = 1_000_000_007;

        var len = n.Length;
        var sum = 0L;
        var map = new long[len];

        map[len - 1] = 1;

        for (var index = len - 2; index >= 0; index--)
            map[index] = (map[index + 1] * 10 + 1) % mod;

        for (var index = 0; index < len; index++)
        {
            var val = int.Parse(n[index].ToString());
            var temp = val * map[index] * (index + 1) % mod;

            sum += temp;
            sum %= mod;
        }

        return (int)sum;
    }

    static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string n = Console.ReadLine();

        int result = substrings(n);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
