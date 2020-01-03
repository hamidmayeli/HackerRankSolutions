// Recursion: Davis' Staircase
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

    // Complete the stepPerms function below.
    static int stepPerms(int n) {
        var map = new Dictionary<int, int>();
        map.Add(1, 1);
        map.Add(2, 2);
        map.Add(3, 4);

        return count(n, map);
    }
    static int count(int n, Dictionary<int, int> map){
        if(map.ContainsKey(n)) return map[n];
        int res = 0;
        res += count(n - 1, map);
        res += count(n - 2, map);
        res += count(n - 3, map);
        map.Add(n, res);
        return res;
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int s = Convert.ToInt32(Console.ReadLine());

        for (int sItr = 0; sItr < s; sItr++) {
            int n = Convert.ToInt32(Console.ReadLine());

            int res = stepPerms(n);

            textWriter.WriteLine(res);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
