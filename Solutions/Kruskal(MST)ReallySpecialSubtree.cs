// Kruskal (MST): Really Special Subtree
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

class Result
{

    /*
     * Complete the 'kruskals' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts WEIGHTED_INTEGER_GRAPH g as parameter.
     */

    /*
     * For the weighted graph, <name>:
     *
     * 1. The number of nodes is <name>Nodes.
     * 2. The number of edges is <name>Edges.
     * 3. An edge exists between <name>From[i] and <name>To[i]. The weight of the edge is <name>Weight[i].
     *
     */

    public static int kruskals(int gNodes, List<int> gFrom, List<int> gTo, List<int> gWeight)
    {
        var ds = new DisjointSets(gNodes);

        var edges = new Edge[gFrom.Count];
        for(var index = 0; index < gFrom.Count; index++)
        {
            edges[index] = new Edge
            {
                From = gFrom[index],
                To = gTo[index],
                Weight = gWeight[index]
            };
        }

        Array.Sort(edges);

        var edgesUsed = 0;
        var sum = 0;

        foreach(var edge in edges)
        {
            if(edgesUsed == gNodes - 1) break;

            if(ds.Find(edge.From) == ds.Find(edge.To)) continue;

            edgesUsed++;
            sum += edge.Weight;

            ds.Union(edge.From, edge.To);
        }

        return sum;
    }

    class DisjointSets
    {
        int[] Parents;
        int[] Ranks;

        public DisjointSets(int nodes)
        {
            Parents = new int[nodes];
            Ranks = new int[nodes];

            for(var index = 0; index < nodes; index++)
                Parents[index] = index;
        }

        public int Find(int value) => FindImpl(value - 1) + 1;

        private int FindImpl(int value)
        {
            if(Parents[value] == value)
                return value;
            else
                return FindImpl(Parents[value]);
        }

        public void Union(int left, int right)
        {
            left = Find(left) - 1;
            right = Find(right) - 1;

            if(Ranks[left] > Ranks[right])
            {
                Parents[right] = left;
            }
            else if(Ranks[left] < Ranks[right])
            {
                Parents[left] = right;
            }
            else
            {
                Parents[right] = left;
                Ranks[left]++;
            }                
        }
    }

    class Edge : IComparable<Edge>
    {
        public int From;
        public int To;
        public int Weight;

        public int CompareTo(Edge other)
        {
            var result = Weight.CompareTo(other.Weight);

            if(result != 0) return result;

            return (From + Weight + To).CompareTo(other.From + other.Weight + other.To);
        }
    }
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] gNodesEdges = Console.ReadLine().TrimEnd().Split(' ');

        int gNodes = Convert.ToInt32(gNodesEdges[0]);
        int gEdges = Convert.ToInt32(gNodesEdges[1]);

        List<int> gFrom = new List<int>();
        List<int> gTo = new List<int>();
        List<int> gWeight = new List<int>();

        for (int i = 0; i < gEdges; i++)
        {
            string[] gFromToWeight = Console.ReadLine().TrimEnd().Split(' ');

            gFrom.Add(Convert.ToInt32(gFromToWeight[0]));
            gTo.Add(Convert.ToInt32(gFromToWeight[1]));
            gWeight.Add(Convert.ToInt32(gFromToWeight[2]));
        }

        int res = Result.kruskals(gNodes, gFrom, gTo, gWeight);

        textWriter.Write(res);

        textWriter.Flush();
        textWriter.Close();
    }
}
