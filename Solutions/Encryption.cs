// Encryption
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

    // Complete the encryption function below.
    static string encryption(string s) {
        s = s.Replace(" ", "");

        var rows = (int)Math.Floor(Math.Sqrt(s.Length));
        var cols = (int)Math.Ceiling(Math.Sqrt(s.Length));

        if(rows * cols < s.Length)
            rows++;
            
        var result = "";

        for (int c = 0; c < cols; c++)
        {
            for (int r = 0; r < rows; r++)
            {
                var index = r * cols + c;
                result += index < s.Length ? s.Substring(index, 1) : "";
            }

            result += " ";
        }

        return result;
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string s = Console.ReadLine();

        string result = encryption(s);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
