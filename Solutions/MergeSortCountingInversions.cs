// Merge Sort: Counting Inversions
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

    // Complete the countInversions function below.
    static long countInversions(int[] arr) {
        if (arr.Length == 1) return 0;

        var mid = arr.Length / 2;
        var left = new int[mid];
        var right = new int[arr.Length - mid];

        for (var index = 0; index < left.Length; index++)
            left[index] = arr[index];

        for (var index = 0; index < right.Length; index++)
            right[index] = arr[mid + index];

        var result = countInversions(left) + countInversions(right);

        var l = 0;
        var r = 0;

        while (l < left.Length || r < right.Length)
        {
            if (l == left.Length || (r < right.Length && right[r] < left[l]))
            {
                arr[r + l] = right[r];
                r++;
                result += left.Length - l;
            }
            else
            {
                arr[r + l] = left[l];
                l++;
            }
        }

        return result;
    }


    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int t = Convert.ToInt32(Console.ReadLine());

        for (int tItr = 0; tItr < t; tItr++) {
            int n = Convert.ToInt32(Console.ReadLine());

            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), arrTemp => Convert.ToInt32(arrTemp))
            ;
            long result = countInversions(arr);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
