// Frequency Queries
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

    // Complete the freqQuery function below.
    static List<int> freqQuery(List<List<int>> queries)
    {
        var data = new Dictionary<int, int>();
        var frequency = new Dictionary<int, int>();

        var result = new List<int>();

        foreach (var command in queries)
        {
            if (command[0] == 1)
            {
                if (data.TryGetValue(command[1], out var val))
                {
                    if (frequency.TryGetValue(val, out var val2))
                        frequency[val] = Math.Max(0, val2 - 1);

                    data[command[1]] = ++val;

                    if (frequency.TryGetValue(val, out var val3))
                        frequency[val] = val3 + 1;
                    else
                        frequency[val] = 1;
                }
                else
                {
                    data.Add(command[1], 1);
                    if (frequency.TryGetValue(1, out var val3))
                        frequency[1] = val3 + 1;
                    else
                        frequency[1] = 1;
                }
            }
            else if (command[0] == 2)
            {
                if (data.TryGetValue(command[1], out var val))
                {
                    if (frequency.TryGetValue(val, out var val2))
                        frequency[val] = Math.Max(0, val2 - 1);

                    data[command[1]] = Math.Max(0, --val);

                    if (frequency.TryGetValue(val, out var val3))
                        frequency[val] = val3 + 1;
                    else
                        frequency[val] = 1;
                }
            }
            else
            {
                result.Add(frequency.TryGetValue(command[1], out var val) && val > 0 ? 1 : 0);
            }
        }
        return result;
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int q = Convert.ToInt32(Console.ReadLine().Trim());

        List<List<int>> queries = new List<List<int>>();

        for (int i = 0; i < q; i++) {
            queries.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(queriesTemp => Convert.ToInt32(queriesTemp)).ToList());
        }

        List<int> ans = freqQuery(queries);

        textWriter.WriteLine(String.Join("\n", ans));

        textWriter.Flush();
        textWriter.Close();
    }
}
