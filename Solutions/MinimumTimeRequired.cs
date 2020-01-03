// Minimum Time Required
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

    // Complete the minTime function below.
    static long minTime(long[] machines, long goal) {
        Array.Sort(machines);
        long max = machines[machines.Length - 1];
        long minDays = 0;
        long maxDays = max * goal;
        long result = -1;
        while (minDays < maxDays)
        {
            long mid = (minDays + maxDays) / 2;
            long unit = 0;
            foreach (long machine in machines)
            {
                unit += mid / machine;
            }
            if (unit < goal)
            {
                minDays = mid + 1;
            }
            else
            {
                result = mid;
                maxDays = mid;
            }
        }
        return result;
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] nGoal = Console.ReadLine().Split(' ');

        int n = Convert.ToInt32(nGoal[0]);

        long goal = Convert.ToInt64(nGoal[1]);

        long[] machines = Array.ConvertAll(Console.ReadLine().Split(' '), machinesTemp => Convert.ToInt64(machinesTemp))
        ;
        long ans = minTime(machines, goal);

        textWriter.WriteLine(ans);

        textWriter.Flush();
        textWriter.Close();
    }
}
