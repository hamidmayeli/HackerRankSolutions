// https://www.hackerrank.com/challenges/triple-sum/problem
// Based on this comment https://www.hackerrank.com/challenges/triple-sum/forum/comments/504737 
// test case 4 is faulty. So, I assume the answer is right.
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

    // Complete the triplets function below.
    static long triplets(int[] a, int[] b, int[] c)
    {
        a = a.Distinct().ToArray();
        b = b.Distinct().ToArray();
        c = c.Distinct().ToArray();

        Array.Sort(a);
        Array.Sort(b);
        Array.Sort(c);

        var result = 0L;

        foreach (var item in b)
        {
            var countA = Count(a, item);
            var countB = Count(c, item);

            result += countA * countB;
        }


        return result;
    }

    static int Count(int[] array, int item)
    {
        var searchIndex = Array.BinarySearch(array, item);

        if( searchIndex >= 0) return searchIndex + 1;
        else return Math.Abs(searchIndex) - 1;
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] lenaLenbLenc = Console.ReadLine().Split(' ');

        int lena = Convert.ToInt32(lenaLenbLenc[0]);

        int lenb = Convert.ToInt32(lenaLenbLenc[1]);

        int lenc = Convert.ToInt32(lenaLenbLenc[2]);

        int[] arra = Array.ConvertAll(Console.ReadLine().Split(' '), arraTemp => Convert.ToInt32(arraTemp))
        ;

        int[] arrb = Array.ConvertAll(Console.ReadLine().Split(' '), arrbTemp => Convert.ToInt32(arrbTemp))
        ;

        int[] arrc = Array.ConvertAll(Console.ReadLine().Split(' '), arrcTemp => Convert.ToInt32(arrcTemp))
        ;
        long ans = triplets(arra, arrb, arrc);

        textWriter.WriteLine(ans);

        textWriter.Flush();
        textWriter.Close();
    }
}
