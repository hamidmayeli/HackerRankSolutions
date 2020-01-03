// Sherlock and Anagrams
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

    // Complete the sherlockAndAnagrams function below.
    static int sherlockAndAnagrams(string s)
    {
        var dic = new Dictionary<string, int>();

        for (var len = 1; len < s.Length; len++)
        {
            for (int index = 0; index <= s.Length - len; index++)
            {
                var key = getKey(s.Substring(index, len));

                if (dic.ContainsKey(key))
                    dic[key]++;
                else
                    dic[key] = 1;
            }
        }

        return dic.Where(x => x.Value > 1).Sum(x => x.Value * (x.Value - 1) / 2);
    }
    
    static string getKey(string value)
    {
        var sb = new StringBuilder(value.Length);
        
        foreach(var @char in value.ToCharArray().OrderBy(x => x))
            sb.Append(@char);
 
        return sb.ToString();
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int q = Convert.ToInt32(Console.ReadLine());

        for (int qItr = 0; qItr < q; qItr++) {
            string s = Console.ReadLine();

            int result = sherlockAndAnagrams(s);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
