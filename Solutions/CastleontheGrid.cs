// Castle on the Grid
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

    // Complete the minimumMoves function below.
    class Point
    {
        public readonly int X;
        public readonly int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    // Complete the minimumMoves function below.
    static int minimumMoves(string[] grid, int startX, int startY, int goalX, int goalY)
    {
        var xMoves = new[] { 1, 0, -1, 0 };
        var yMoves = new[] { 0, 1, 0, -1 };

        var visited = new bool[grid.Length, grid.Length];
        var stepsToReach = new int[grid.Length, grid.Length];
        var queue = new Queue<Point>();

        queue.Enqueue(new Point(startX, startY));
        stepsToReach[startX, startY] = 0;
        visited[startX, startY] = true;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            for (var directionIndex = 0; directionIndex < xMoves.Length; directionIndex++)
            {
                var counter = 1;

                while (IsValid(grid,
                    current.X + xMoves[directionIndex] * counter,
                    current.Y + yMoves[directionIndex] * counter))
                {
                    var x = current.X + xMoves[directionIndex] * counter;
                    var y = current.Y + yMoves[directionIndex] * counter;
                    counter++;

                    if (x == goalX && y == goalY)
                        return stepsToReach[current.X, current.Y] + 1;

                    if (visited[x, y]) continue;

                    stepsToReach[x, y] = stepsToReach[current.X, current.Y] + 1;
                    visited[x, y] = true;
                    queue.Enqueue(new Point(x, y));
                }
            }
        }

        throw new Exception("No path found.");
    }

    static bool IsValid(string[] grid, int x, int y)
    {
        return x >= 0 &&
            y >= 0 &&
            x < grid.Length &&
            y < grid.Length &&
            grid[x][y] != 'X';
    }


    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine());

        string[] grid = new string [n];

        for (int i = 0; i < n; i++) {
            string gridItem = Console.ReadLine();
            grid[i] = gridItem;
        }

        string[] startXStartY = Console.ReadLine().Split(' ');

        int startX = Convert.ToInt32(startXStartY[0]);

        int startY = Convert.ToInt32(startXStartY[1]);

        int goalX = Convert.ToInt32(startXStartY[2]);

        int goalY = Convert.ToInt32(startXStartY[3]);

        int result = minimumMoves(grid, startX, startY, goalX, goalY);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
