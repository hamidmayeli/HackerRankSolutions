// Extra Long Factorials
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

    // Complete the extraLongFactorials function below.
    static void extraLongFactorials(int n) {
        var result = System.Numerics.BigInteger.One;

        for(var index = 2; index <= n; index++)
            result *= index;
        
        Console.WriteLine(result);
    }

    static void Main(string[] args) {
        int n = Convert.ToInt32(Console.ReadLine());

        extraLongFactorials(n);
    }
}
