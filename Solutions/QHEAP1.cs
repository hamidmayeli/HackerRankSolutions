// https://www.hackerrank.com/challenges/qheap1/problem
using System;
using System.Collections.Generic;
using System.IO;
class Solution {
    static void Main(String[] args) {
        var count = int.Parse(Console.ReadLine());

        var list = new SortedList<int, bool>();

        for (int index = 0; index < count; index++)
        {
            var command = Console.ReadLine();
            var parts = command.Split(' ');

            if (parts[0] == "1")
                list.Add(int.Parse(parts[1]), false);

            else if (parts[0] == "2")
                list.Remove(int.Parse(parts[1]));

            else if (parts[0] == "3")
                Console.WriteLine(list.Keys[0]);
        }
    }
}

