// Largest Rectangle 
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

    // Complete the largestRectangle function below.
    class Data{
        public readonly int Index;
        public readonly int Size;

        public Data(int index, int size){
            Index = index;
            Size = size;
        }
    }

    // Complete the largestRectangle function below.
    static long largestRectangle(int[] h)
    {
        var max = 0L;
        var stack = new Stack<Data>();

        for (var index = 0; index < h.Length; index++)
        {
            if (!TryPeek(stack, out var value) || value.Size < h[index])
                stack.Push(new Data(index, h[index]));

            else if (value.Size > h[index])
            {
                var lastBlockIndex = 0;

                do
                {
                    max = Math.Max(max,
                        value.Size * (index - value.Index));

                    lastBlockIndex = value.Index;

                    stack.Pop();
                    TryPeek(stack, out value);
                } while (value != null && value.Size > h[index]);

                if(value == null)
                    stack.Push(new Data(0, h[index]));

                else if(value.Size < h[index])
                    stack.Push(new Data(lastBlockIndex, h[index]));
            }
        }

        while (TryPop(stack, out var val))
        {
            max = Math.Max(max,
                val.Size * (h.Length - val.Index));
        }

        return max;
    }

    static bool TryPeek(Stack<Data> stack, out Data value)
    {
        value = null;

        if (stack.Count == 0) return false;

        value = stack.Peek();
        return true;
    }

    static bool TryPop(Stack<Data> stack, out Data value)
    {
        value = null;

        if (stack.Count == 0) return false;

        value = stack.Pop();
        return true;
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine());

        int[] h = Array.ConvertAll(Console.ReadLine().Split(' '), hTemp => Convert.ToInt32(hTemp))
        ;
        long result = largestRectangle(h);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
