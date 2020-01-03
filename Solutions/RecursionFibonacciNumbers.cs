// Recursion: Fibonacci Numbers
using System;
using System.Collections.Generic;
using System.IO;

class Solution {
    
    public static int Fibonacci(int n) {
        if(n < 2) return n;
        return Fibonacci(n - 2) + Fibonacci(n - 1);
    }

    static void Main(String[] args) {
        int n = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine(Fibonacci(n));
    }
}

