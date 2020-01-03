// New Year Chaos
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

        // Complete the minimumBribes function below.
        //static void minimumBribes(int[] q)
        //{
        //    var bribesCount = 0;
        //    Func<int, int> getOriginal = (int index) => q[index];
        //    Func<int, int> getCurrent = (int index) => index + 1;

        //    for (var index = 0; index < q.Length; index++)
        //    {
        //        if (getOriginal(index) > getCurrent(index) + 2 /* 2 bribes*/)
        //        {
        //            Console.WriteLine("Too chaotic");
        //            return;
        //        }

        //        if (getOriginal(index) > getCurrent(index))
        //            bribesCount += getOriginal(index) - getCurrent(index);
        //    }

        //    Console.WriteLine(bribesCount);
        //}

        static void minimumBribes(int[] q)
        {
            int ans = 0;

for (var i = q.Length - 1; i >= 0; i--) {
        if (q[i] - (i + 1) > 2) {
            Console.WriteLine("Too chaotic");
            return;
        }

        for (int j = Math.Max(0, q[i] - 2); j < i; j++)
            if (q[j] > q[i]) ans++;
}

            Console.WriteLine(ans);
        }

        static void Main(string[] args)
        {
            int t = Convert.ToInt32(Console.ReadLine());

            for (int tItr = 0; tItr < t; tItr++)
            {
                int n = Convert.ToInt32(Console.ReadLine());

                int[] q = Array.ConvertAll(Console.ReadLine().Split(' '), qTemp => Convert.ToInt32(qTemp))
                ;
                minimumBribes(q);
            }
        }
    }
