// Min Max Riddle
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

    // Complete the riddle function below.
    static long[] riddle(long[] arr)
    {
        int N = arr.Length;
        long[] result = new long[N];
        long[] span = new long[N];

        var deckValues = new Stack<long>();
        var deckIndexes = new Stack<long>();
        deckIndexes.Push(-1L);

        // count number of ge elements to the left
        for (int index = 0; index < N; index++)
        {
            while (TryPeek(deckValues, out var val) && val >= arr[index])
            {
                deckValues.Pop();
                deckIndexes.Pop();
            }
            span[index] = index - deckIndexes.Peek() - 1;
            deckValues.Push(arr[index]);
            deckIndexes.Push(index);
        }

        // count number of ge elements to the right
        deckValues.Clear();
        deckIndexes.Clear();
        deckIndexes.Push(N);
        for (int index = N - 1; index >= 0; index--)
        {
            while (TryPeek(deckValues, out var val) && val >= arr[index])
            {
                deckValues.Pop();
                deckIndexes.Pop();
            }
            span[index] += deckIndexes.Peek() - index - 1;
            deckValues.Push(arr[index]);
            deckIndexes.Push(index);
        }

        // fill results
        for (int i = 0; i < N; i++)
            result[(int)span[i]] = Math.Max(result[(int)span[i]], arr[i]);

        // fill the gaps
        for (int i = N - 2; i >= 0; i--)
            result[i] = Math.Max(result[i], result[i + 1]);

        return result;
    }
    
    static bool TryPeek<T>(Stack<T> stack, out T value)
    {
        value = default(T);

        if (stack.Count == 0) return false;

        value = stack.Peek();
        return true;
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine());

        long[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), arrTemp => Convert.ToInt64(arrTemp))
        ;
        long[] res = riddle(arr);

        textWriter.WriteLine(string.Join(" ", res));

        textWriter.Flush();
        textWriter.Close();
    }
}
