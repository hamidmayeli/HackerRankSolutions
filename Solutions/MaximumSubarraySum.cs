// Maximum Subarray Sum
// score: 0.375
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

    // Complete the maximumSum function below.
    static long maximumSum(long[] a, long m) {
        var remainings = new long[a.Length];

        remainings[0] = a[0] % m;

        for (int index = 1; index < a.Length; index++)
            remainings[index] = (remainings[index - 1] + a[index]) % m;

        long max = 0L;

        for (int i = 0; i < a.Length; i++)
        {
            max = Math.Max(max, remainings[i]);
            
            if (i > 0)
                max = Math.Max(max, (remainings[i] - remainings[i - 1]) % m);

            for (int j = i + 1; j < a.Length; j++)
            {
                max = Math.Max(max, (remainings[j] - remainings[i] + m) % m);
            }
        }

        return max;
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int q = Convert.ToInt32(Console.ReadLine());

        for (int qItr = 0; qItr < q; qItr++) {
            string[] nm = Console.ReadLine().Split(' ');

            int n = Convert.ToInt32(nm[0]);

            long m = Convert.ToInt64(nm[1]);

            long[] a = Array.ConvertAll(Console.ReadLine().Split(' '), aTemp => Convert.ToInt64(aTemp))
            ;
            long result = maximumSum(a, m);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
