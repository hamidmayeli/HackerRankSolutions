// Making Candies
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

    // Complete the minimumPasses function below.
    static long minimumPasses(long machines, long workers, long cost, long goal)
    {
        var lower = 1L;
        var upper = 1000000000000L;

        while (lower < upper)
        {
            long mid = (lower + upper) / 2;
            if (CanProduce(machines, workers, cost, goal, mid))
                upper = mid;
            else
                lower = mid + 1;
        }

        return lower;
    }

    static bool CanProduce(long machines, long workers, long price, long target, long days)
    {
        if (machines >= (target + workers - 1) / workers) return true;

        var current = machines * workers;
        days--;

        if (days == 0) return false;

        while (true)
        {
            var remaining = target - current;
            var daysWithCurrentSettings = (remaining + machines * workers - 1) / (machines * workers);

            if (daysWithCurrentSettings <= days) return true;

            if (current < price)
            {
                remaining = price - current;
                daysWithCurrentSettings = (remaining + machines * workers - 1) / (machines * workers);
                days -= daysWithCurrentSettings;
                if (days < 1) return false;
                current += daysWithCurrentSettings * machines * workers;
            }

            current -= price;
            if (machines > workers)
                workers++;
            else
                machines++;
        }
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] mwpn = Console.ReadLine().Split(' ');

        long m = Convert.ToInt64(mwpn[0]);

        long w = Convert.ToInt64(mwpn[1]);

        long p = Convert.ToInt64(mwpn[2]);

        long n = Convert.ToInt64(mwpn[3]);

        long result = minimumPasses(m, w, p, n);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
