// https://www.hackerrank.com/challenges/coin-change/problem
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

class Result
{

    /*
     * Complete the 'getWays' function below.
     *
     * The function is expected to return a LONG_INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. LONG_INTEGER_ARRAY c
     */

    static List<long> Coins;
    static long?[,] Map;

    public static long getWays(int n, List<long> c)
    {
        c.Sort();
        Coins = c;
        Map = new long?[n, Coins.Count];

        return Count(n, Coins.Count - 1);
    }

    private static long Count(long target, int maxCointIndex)
    {
        if (target == 0) return 1;
        if (target < 0) return 0;
        if (maxCointIndex < 0) return 0;

        if (Map[target - 1, maxCointIndex].HasValue)
            return Map[target - 1, maxCointIndex].Value;

        var sum = Count(target, maxCointIndex - 1) +
            Count(target - Coins[maxCointIndex], maxCointIndex);

        Map[target - 1, maxCointIndex] = sum;

        return sum;
    }
}


class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int n = Convert.ToInt32(firstMultipleInput[0]);

        int m = Convert.ToInt32(firstMultipleInput[1]);

        List<long> c = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(cTemp => Convert.ToInt64(cTemp)).ToList();

        // Print the number of ways of making change for 'n' units using coins having the values given by 'c'

        long ways = Result.getWays(n, c);

        textWriter.WriteLine(ways);

        textWriter.Flush();
        textWriter.Close();
    }
}
