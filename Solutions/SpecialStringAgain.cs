// Special String Again
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

    class Point
    {
        public char text;
        public long counter;

        public Point(char t, long c)
        {
            text = t;
            counter = c;
        }
    }

    // Complete the substrCount function below.
    static long substrCount(int n, string s) {
        var count = 0L;
        var repeatingCounter = 1L;
        var list = new List<Point>();

        for (int i = 1; i < s.Length; i++)
        {
            if (s[i] == s[i - 1])
            {
                repeatingCounter++;
            }
            else
            {
                list.Add(new Point(s[i - 1], repeatingCounter));
                repeatingCounter = 1L;
            }
        }

        list.Add(new Point(s[s.Length - 1], repeatingCounter));

        for (int i = 0; i < list.Count; i++)
            count += (list[i].counter + 1) * list[i].counter / 2;

        for (int i = 1; i < list.Count - 1; i++)
            if (list[i].counter == 1 && list[i - 1].text == list[i + 1].text)
                count += Math.Min(list[i - 1].counter, list[i + 1].counter);

        return count;
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine());

        string s = Console.ReadLine();

        long result = substrCount(n, s);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
