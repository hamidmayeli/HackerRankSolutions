// https://www.hackerrank.com/challenges/equal/problem
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

class Solution
{

    // Complete the equal function below.
    static int equal(int[] arr)
    {
        Array.Sort(arr);
        var min = arr[0];

        for (int index = 0; index < arr.Length; index++)
            arr[index] -= min;

        var result = int.MaxValue;

        for (int index = 0; index < 5; index++)
            result = Math.Min(result, CalcFor(arr, index));

        return result;
    }

    private static int CalcFor(int[] arr, int delta)
    {
        var total = 0;

        for (int index = 0; index < arr.Length; index++)
        {
            var temp = arr[index] + delta;

            var fives = temp / 5;
            temp %= 5;

            var twos = temp / 2;
            temp %= 2;

            total += fives + twos + temp % 2;
        }

        return total;
    }

    static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int t = Convert.ToInt32(Console.ReadLine());

        for (int tItr = 0; tItr < t; tItr++)
        {
            int n = Convert.ToInt32(Console.ReadLine());

            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), arrTemp => Convert.ToInt32(arrTemp))
            ;
            int result = equal(arr);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
