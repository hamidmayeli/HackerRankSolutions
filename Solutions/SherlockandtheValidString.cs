// Sherlock and the Valid String
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

    // Complete the isValid function below.
    static string isValid(string s)
    {
        var dic = CreateDictionary(s);
        var countDic = new Dictionary<int, int>();

        foreach (var item in dic)
        {
            if (countDic.TryGetValue(item.Value, out var value))
                countDic[item.Value] = value + 1;
            else
                countDic[item.Value] = 1;
        }

        switch (countDic.Count)
        {
            case 1: return "YES";
         
            case 2:
                var first = countDic.First();
                var last = countDic.Last();

                if (first.Value == 1 && first.Key == 1)
                    return "YES";

                if (last.Value == 1 && last.Key == 1)
                    return "YES";

                if (last.Value > 1 && first.Value > 1)
                    return "NO";

                if (Math.Abs(last.Key - first.Key) > 1)
                    return "NO";
                else
                    return "YES";

            default: return "NO";
        }
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

        string s = Console.ReadLine();

        string result = isValid(s);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
