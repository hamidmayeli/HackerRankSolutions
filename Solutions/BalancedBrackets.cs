// Balanced Brackets
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

    // Complete the isBalanced function below.
    static string isBalanced(string s)
    {
        var openings = new[] { '[', '{', '(' };

        var stack = new Stack<char>();

        foreach (var c in s.ToCharArray())
        {
            if (openings.Contains(c))
                stack.Push(c);

            else if (c == ']')
                if (!TryPeek(stack, out var c1) || c1 != '[')
                    return "NO";
                else
                    stack.Pop();

            else if (c == ')')
                if (!TryPeek(stack, out var c2) || c2 != '(')
                    return "NO";
                else
                    stack.Pop();

            else if (c == '}')
                if (!TryPeek(stack, out var c3) || c3 != '{')
                    return "NO";
                else
                    stack.Pop();
        }

        return stack.Count == 0 ? "YES" : "NO";
    }

    static bool TryPeek(Stack<char> stack, out char value)
    {
        value = default(char);

        if (stack.Count == 0) return false;

        value = stack.Peek();
        return true;
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int t = Convert.ToInt32(Console.ReadLine());

        for (int tItr = 0; tItr < t; tItr++) {
            string s = Console.ReadLine();

            string result = isBalanced(s);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
