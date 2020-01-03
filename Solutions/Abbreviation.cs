// Abbreviation
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

    // Complete the abbreviation function below.
    static string abbreviation(string a, string b) {
        var diff = 'a' - 'A';

        var m = a.Length;
        var n = b.Length;
        var dp = new bool[n + 1, m + 1];

        dp[0, 0] = true;

        for (int i = 0; i < n + 1; i++)
        {
            for (int j = 0; j < m + 1; j++)
            {
                if (i == 0 && j != 0)
                    dp[i, j] = a[j - 1] > 'Z' && dp[i, j - 1];

                else if (i != 0 && j != 0)
                    if (a[j - 1] == b[i - 1])
                        dp[i, j] = dp[i - 1, j - 1];
                    else if (a[j - 1] - diff == b[i - 1])
                        dp[i, j] = dp[i - 1, j - 1] || dp[i, j - 1];
                    else if (!(a[j - 1] < 'a' && b[i - 1] < 'a'))
                        dp[i, j] = dp[i, j - 1];
            }
        }

        return dp[n, m] ? "YES" : "NO";
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int q = Convert.ToInt32(Console.ReadLine());

        for (int qItr = 0; qItr < q; qItr++) {
            string a = Console.ReadLine();

            string b = Console.ReadLine();

            string result = abbreviation(a, b);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
