// Sorting: Bubble Sort
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

    // Complete the countSwaps function below.
    static void countSwaps(int[] a) {
        if(a.Length == 0)
            throw new ArgumentException();

        var counter = 0;

        for (int i = 0; i < a.Length; i++) {
            for (int j = 0; j < a.Length - 1 - i; j++) {
                if (a[j] > a[j + 1]) {
                    var temp = a[j + 1];
                    a[j + 1] = a[j];
                    a[j] = temp;

                    counter++;
                }
            }   
        }

        Console.WriteLine($"Array is sorted in {counter} swaps.");
        Console.WriteLine($"First Element: {a[0]}");
        Console.WriteLine($"Last Element: {a[a.Length - 1]}");
    }

    static void Main(string[] args) {
        int n = Convert.ToInt32(Console.ReadLine());

        int[] a = Array.ConvertAll(Console.ReadLine().Split(' '), aTemp => Convert.ToInt32(aTemp))
        ;
        countSwaps(a);
    }
}
