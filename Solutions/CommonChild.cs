// Common Child
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

    // Complete the commonChild function below.
    static int commonChild(string s1, string s2)
    {
        var arry = new int[s1.Length + 1, s2.Length + 1];

        for (int i = 0; i < arry.GetLength(0); i++)
            arry[i, 0] = 0;

        for (int i = 0; i < arry.GetLength(1); i++)
            arry[0, i] = 0;

        for (int i = 1; i < arry.GetLength(0); i++)
            for (int j = 1; j < arry.GetLength(1); j++)
                if (s1[i - 1] == s2[j - 1])
                    arry[i, j] = arry[i - 1, j - 1] + 1;
                else
                    arry[i, j] = Math.Max(arry[i, j - 1], arry[i - 1, j]);

        return arry[arry.GetLength(0) - 1, arry.GetLength(1) - 1];
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string s1 = Console.ReadLine();

        string s2 = Console.ReadLine();

        int result = commonChild(s1, s2);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
