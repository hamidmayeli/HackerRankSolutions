// Balanced Forest
// score: 0.3
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

class Solution
{
    class Node
    {
        public int cost;
        public bool visited = false;
        public bool v2 = false;
        public List<int> adjacent = new List<int>();

        public override string ToString()
        {
            return "Node{" +
                    "cost=" + cost +
                    ", v1=" + visited +
                    ", v2=" + v2 +
                    ", adjacent=" + adjacent +
                    '}';
        }

        public Node(int cost)
        {
            this.cost = cost;
        }
    }

    static int mini;
    static int sum;
    static HashSet<int> s = new HashSet<int>();
    static HashSet<int> q = new HashSet<int>();

    static int balancedForest(int[] node_values, int[][] edges)
    {
        s = new HashSet<int>();
        q = new HashSet<int>();

        var nodes = new List<Node>();
        for (int i = 0; i < node_values.Length; i++)
            nodes.Add(new Node(node_values[i]));

        for (int i = 0; i < edges.Length; i++)
        {
            var edge = edges[i];
            var a = edge[0] - 1;
            var b = edge[1] - 1;
            nodes[a].adjacent.Add(b);
            nodes[b].adjacent.Add(a);
        }

        mini = sum = dfs(0, nodes);
        solve(0, nodes);

        return mini == sum ? -1 : mini;
    }

    // s contains sums encountered during depth first search excluding those from the root to the current node.
    // q contains sums encountered during depth first search from the root to current node.
    private static void solve(int p, List<Node> nodes)
    {
        var node = nodes[p];
        if (node.v2) return;
        node.v2 = true;

        int[] x = {2*node.cost,
                2*sum - 4*node.cost,
                sum-node.cost};
        int[] y = {3*node.cost - sum,
                (sum-node.cost)/2 - node.cost};

        // If removing the edge above the current node (node variable defined at the top of this method)
        // gives two trees whose total values are the same, then the node we add should have that
        // same value too to get 3 trees (one of which is our single node that we added) with the same value.
        if (sum % 2 == 0 && node.cost == (sum / 2)) mini = Math.Min(mini, sum / 2);

        // case 1a: When two non-overlapping subtrees in the overall tree have the same total value.
        if (s.Contains(node.cost))
        {// ||                      // case 1a
         //                q.Contains(2*node.cost) ) {                  // ?
            if (y[0] >= 0) mini = Math.Min(mini, y[0]);
        }

        // case 1b: (part B): Two non-overlapping subtrees in the overall tree.
        // case 2b: One edge to be removed is below the other edge to be removed in the overall tree.
        if (s.Contains(sum - 2 * node.cost) ||                 // case 1b (part B)
                q.Contains(sum - node.cost))
        {                // case 2b
            if (y[0] >= 0) mini = Math.Min(mini, y[0]);
        }

        // case 1b: (part A): Two non-overlapping subtrees in the overall tree.
        // case 2a: One edge to be removed is below the other edge to be removed in the overall tree.
        if ((sum - node.cost) % 2 == 0)
        {
            if (s.Contains((sum - node.cost) / 2) ||            // case 1b (part A)
                    q.Contains((sum + node.cost) / 2))
            {        // case 2a
                if (y[1] >= 0) mini = Math.Min(mini, y[1]);
            }
        }

        q.Add(node.cost);

        for (int i = 0; i < node.adjacent.Count; i++)
        {  // DFS!!
            solve(node.adjacent[i], nodes);           // recursive call
        }

        q.Remove(node.cost);
        s.Add(node.cost);
    }


    private static int dfs(int p, List<Node> nodes)
    {
        var node = nodes[p];

        if (node.visited) return 0;
        
        node.visited = true;

        for (int i = 0; i < node.adjacent.Count; i++)
            node.cost += dfs(node.adjacent[i], nodes);

        return node.cost;
    }

    static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int q = Convert.ToInt32(Console.ReadLine());

        for (int qItr = 0; qItr < q; qItr++)
        {
            int n = Convert.ToInt32(Console.ReadLine());

            int[] c = Array.ConvertAll(Console.ReadLine().Split(' '), cTemp => Convert.ToInt32(cTemp))
            ;

            int[][] edges = new int[n - 1][];

            for (int i = 0; i < n - 1; i++)
            {
                edges[i] = Array.ConvertAll(Console.ReadLine().Split(' '), edgesTemp => Convert.ToInt32(edgesTemp));
            }

            int result = balancedForest(c, edges);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
