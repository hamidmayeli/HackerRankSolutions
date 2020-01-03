// Maximum Xor
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

    class Trie
    {
        Trie left;
        Trie right;

        public void Insert(Trie head, int n)
        {
            for (int i = 31; i >= 0; i--)
            {
                int value = (n >> i) & 1;
                if (value == 0)
                {//move left
                    if (head.left == null)
                        head.left = new Trie();
                    head = head.left;
                }
                else
                {//move right
                    if (head.right == null)
                        head.right = new Trie();
                    head = head.right;
                }
            }
        }

        public uint MaxXor(Trie head, int n)
        {
            uint max = 0;
            for (int i = 31; i >= 0; i--)
            {
                int value = (n >> i) & 1;
                //for max xor you have to move alternate
                if (value == 0)
                {//move right
                    if (head.right != null)
                    {
                        max += (uint)Math.Pow(2, i);
                        head = head.right;
                    }
                    else
                        head = head.left;
                }
                else
                {//move left
                    if (head.left != null)
                    {
                        max += (uint)Math.Pow(2, i);
                        head = head.left;
                    }
                    else
                        head = head.right;
                }
            }
            return max;
        }
    }
    static int[] maxXor(int[] arr, int[] queries)
    {
        int[] result = new int[queries.Length];
        Trie head = new Trie();
        for (int i = 0; i < arr.Length; i++)
        {
            head.Insert(head, arr[i]);
        }
        for (int i = 0; i < queries.Length; i++)
        {
            result[i] = Convert.ToInt32(head.MaxXor(head, queries[i]));
        }
        return result;
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine());

        int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), arrTemp => Convert.ToInt32(arrTemp))
        ;
        int m = Convert.ToInt32(Console.ReadLine());

        int[] queries = new int [m];

        for (int i = 0; i < m; i++) {
            int queriesItem = Convert.ToInt32(Console.ReadLine());
            queries[i] = queriesItem;
        }

        int[] result = maxXor(arr, queries);

        textWriter.WriteLine(string.Join("\n", result));

        textWriter.Flush();
        textWriter.Close();
    }
}
