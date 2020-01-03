// Morgan and a String
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

    // Complete the morganAndString function below.
    static string morganAndString(string a, string b) {
        var result = new StringBuilder(a.Length + b.Length);
    var ia = 0;
    var ib = 0;

    while (ia < a.Length && ib < b.Length)
    {
        switch (BreakTie(a, b, ia, ib))
        {
            case -1:
            case 0:
                var aa = a[ia];
                while (ia < a.Length && aa == a[ia])
                {
                    result.Append(aa);
                    ia++;
                }
                break;
            case 1:
                var bb = b[ib];
                while (ib < b.Length && bb == b[ib])
                {
                    result.Append(bb);
                    ib++;
                }
                break;
        }
    }

    result.Append(a.Substring(ia));
    result.Append(b.Substring(ib));

    return result.ToString();
}
static int BreakTie  (string a, string b, int ja, int jb) 
{
    while (ja < a.Length && jb < b.Length)
    {
        if (a[ja] < b[jb])
            return -1;
        if (a[ja] > b[jb])
            return 1;
        ja++;
        jb++;
    }
    return ja < a.Length ? -1 : 1;
}

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int t = Convert.ToInt32(Console.ReadLine());

        for (int tItr = 0; tItr < t; tItr++) {
            string a = Console.ReadLine();

            string b = Console.ReadLine();

            string result = morganAndString(a, b);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
