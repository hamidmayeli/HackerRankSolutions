// Strings: Making Anagrams
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

    // Complete the makeAnagram function below.
    static int makeAnagram(string a, string b) {
        var sum = 0;

        var aChars = CreateDictionary(a);
        var bChars = CreateDictionary(b);

        foreach (var item in aChars)
        {
            if (bChars.TryGetValue(item.Key, out var value))
                sum += Math.Abs(item.Value - value);
            else
                sum += item.Value;
        }

        foreach (var item in bChars)
            if (!aChars.TryGetValue(item.Key, out var value))
                sum += item.Value;

        return sum;
    }

    static Dictionary<char, int> CreateDictionary(string str)
    {
        var dic = new Dictionary<char, int>();

        for (var index = 0; index < str.Length; index++)
        {
            if (dic.TryGetValue(str[index], out var value))
                dic[str[index]] = value + 1;
            else
                dic[str[index]] = 1;
        }

        return dic;
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string a = Console.ReadLine();

        string b = Console.ReadLine();

        int res = makeAnagram(a, b);

        textWriter.WriteLine(res);

        textWriter.Flush();
        textWriter.Close();
    }
}
