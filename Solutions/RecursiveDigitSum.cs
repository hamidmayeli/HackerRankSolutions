// Recursive Digit Sum
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

    // Complete the superDigit function below.
    static int superDigit(string n, int k) {
        Console.WriteLine($"{k} => {n}");

        if(n.Length == 1)
            return int.Parse(n);
        
        var sum = 0L;
        for(var index = 0; index < n.Length; index++){
            sum += int.Parse(n.Substring(index, 1));
        }

        return superDigit((sum * k).ToString(), 1);
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] nk = Console.ReadLine().Split(' ');

        string n = nk[0];

        int k = Convert.ToInt32(nk[1]);

        int result = superDigit(n, k);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
