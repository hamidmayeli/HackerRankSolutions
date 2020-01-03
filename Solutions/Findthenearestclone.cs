// Find the nearest clone
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

    // Complete the findShortest function below.

    /*
     * For the unweighted graph, <name>:
     *
     * 1. The number of nodes is <name>Nodes.
     * 2. The number of edges is <name>Edges.
     * 3. An edge exists between <name>From[i] to <name>To[i].
     *
     */
    static int findShortest(int graphNodes, int[] graphFrom, int[] graphTo, long[] ids, int val) {
        var visited = new bool[graphNodes];
        var stepsToReach = new int[graphNodes];
        var path = new Dictionary<int, List<int>>();
        var colors = new Dictionary<int, long>();

        for(var index = 0; index < graphFrom.Length; index++)
            AddPath(path, graphFrom[index], graphTo[index]);

        for(var index = 0; index < ids.Length; index++)
            colors.Add(index + 1, ids[index]);
        
        var queue = new Queue<int>();

        queue.Enqueue(val);

        while(TryDequeue(queue, out var current))
        {
            if(colors[current] == colors[val] && current != val) return stepsToReach[current - 1];

             if (visited[current - 1]) continue;
            else visited[current - 1] = true;

            if (path.TryGetValue(current, out var list))
            {
                foreach (var next in list)
                {
                    stepsToReach[next - 1] = stepsToReach[current - 1] + 1;
                    queue.Enqueue(next);
                }
            }
        }

        return -1;
    }

    static bool TryDequeue<T>(Queue<T> queue, out T value)
    {
        value = default(T);
        if(queue.Count == 0) return false;

        value = queue.Dequeue();
        return true;
    }

    static void AddPath(Dictionary<int, List<int>> path, int from, int to, bool bothWays = true)
    {
        if (path.TryGetValue(from, out var list))
        {
            if (!list.Contains(to))
                list.Add(to);
        }
        else
            path.Add(from, new List<int> { to });

        if(bothWays)
            AddPath(path, to, from, false);
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] graphNodesEdges = Console.ReadLine().Split(' ');
        int graphNodes = Convert.ToInt32(graphNodesEdges[0]);
        int graphEdges = Convert.ToInt32(graphNodesEdges[1]);

        int[] graphFrom = new int[graphEdges];
        int[] graphTo = new int[graphEdges];

        for (int i = 0; i < graphEdges; i++) {
            string[] graphFromTo = Console.ReadLine().Split(' ');
            graphFrom[i] = Convert.ToInt32(graphFromTo[0]);
            graphTo[i] = Convert.ToInt32(graphFromTo[1]);
        }

        long[] ids = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), idsTemp => Convert.ToInt64(idsTemp))
        ;
        int val = Convert.ToInt32(Console.ReadLine());

        int ans = findShortest(graphNodes, graphFrom, graphTo, ids, val);

        textWriter.WriteLine(ans);

        textWriter.Flush();
        textWriter.Close();
    }
}
