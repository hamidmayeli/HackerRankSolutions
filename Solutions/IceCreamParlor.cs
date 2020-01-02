// https://www.hackerrank.com/challenges/ctci-ice-cream-parlor/problem
// Test cases are faulty, so I have change the biolerplate. (By triming the inputs)
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

    // Complete the whatFlavors function below.
    static void whatFlavors(int[] cost, int money)
    {
        var dic = new Dictionary<int, List<int>>();
        for (var index = 0; index < cost.Length; index++)
        {
            if (dic.TryGetValue(cost[index], out var val))
                val.Add(index + 1);
            else
                dic.Add(cost[index], new List<int> { index + 1 });
        }

        var first = 0;
        var second = 0;

        foreach (var c in cost)
        {
            var needed = money - c;
            if (needed == c)
            {
                if (dic[c].Count > 1)
                {
                    first = dic[c][0];
                    second = dic[c][1];
                    break;
                }
            }
            else if (dic.TryGetValue(needed, out var list))
            {
                first = dic[c][0];
                second = list[0];
                break;
            }
        }

        var min = Math.Min(first, second);
        var max = Math.Max(first, second);
        Console.WriteLine($"{min} {max}");
    }

    static void Main(string[] args)
    {
        int t = Convert.ToInt32(Console.ReadLine().Trim());

        for (int tItr = 0; tItr < t; tItr++)
        {
            int money = Convert.ToInt32(Console.ReadLine().Trim());

            int n = Convert.ToInt32(Console.ReadLine().Trim());

            int[] cost = Array.ConvertAll(Console.ReadLine().Trim().Split(' ')
            .Select(x => x.Trim()).ToArray(), costTemp => Convert.ToInt32(costTemp));

            whatFlavors(cost, money);
        }
    }
}
