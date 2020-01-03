// DFS: Connected Cell in a Grid
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

    // Complete the maxRegion function below.
    static int maxRegion(int[][] matrix)
    {
        var xOffset = new[] { -1, 0, 1, -1 };
        var yOffset = new[] { -1, -1, -1, 0 };

        var ds = new DisjointSets();

        var rows = matrix.Length;
        var cols = matrix[0].Length;

        var max = 0;

        Func<int, int, bool> isValid = (int x, int y) =>
        {
            return x >= 0 && x < cols && y >= 0 && y < rows;
        };

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                if(matrix[y][x] == 0) continue;
                var current = new CellKey(x, y);
                ds.AddNew(current);
                max = Math.Max(max, 1);

                for (int index = 0; index < xOffset.Length; index++)
                {
                    var ox = x + xOffset[index];
                    var oy = y + yOffset[index];

                    if (!isValid(ox, oy)) continue;

                    if(matrix[oy][ox] == 0) continue;

                    var offset = new CellKey(ox, oy);

                    if (ds.Find(current) != ds.Find(offset))
                        max = Math.Max(max, ds.Union(current, offset));
                }
            }
        }

        return max;
    }

    struct CellKey
    {
        public int X;
        public int Y;

        public CellKey(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator !=(CellKey lhs, CellKey rhs) => !(lhs == rhs);

        public static bool operator ==(CellKey lhs, CellKey rhs) => lhs.X == rhs.X && lhs.Y == rhs.Y;

        public override bool Equals(object obj) => this == (CellKey) obj;

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => $"{Y} - {X}";
    }

    class DisjointSets
    {
        Dictionary<CellKey, CellKey> Parents = new Dictionary<CellKey, CellKey>();
        Dictionary<CellKey, int> Counts = new Dictionary<CellKey, int>();

        public void AddNew(CellKey key)
        {
            Parents.Add(key, key);
            Counts.Add(key, 1);
        }

        public CellKey Find(CellKey value) => Parents[value] == value ? value : Find(Parents[value]);

        public int Union(CellKey left, CellKey right)
        {
            var pLeft = Find(left);
            var pRight = Find(right);

            Parents[pRight] = pLeft;

            Counts[pLeft] = Counts[pLeft] + Counts[pRight];

            return Counts[pLeft];
        }
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine());

        int m = Convert.ToInt32(Console.ReadLine());

        int[][] grid = new int[n][];

        for (int i = 0; i < n; i++) {
            grid[i] = Array.ConvertAll(Console.ReadLine().Split(' '), gridTemp => Convert.ToInt32(gridTemp));
        }

        int res = maxRegion(grid);

        textWriter.WriteLine(res);

        textWriter.Flush();
        textWriter.Close();
    }
}
