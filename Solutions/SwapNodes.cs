// https://www.hackerrank.com/challenges/swap-nodes-algo/problem
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Solution
{

    /*
     * Complete the swapNodes function below.
     */
    static int[][] swapNodes(int[][] indexes, int[] queries)
    {
        var queue = new Queue<Node>();
        var dic = new Dictionary<int, List<Node>>();

        var root = new Node
        {
            Data = 1,
            Level = 1,
        };

        dic.Add(1, new List<Node> { root });
        queue.Enqueue(root);

        var counter = 0;

        Func<int, Node, Node> get = (int data, Node current) =>
        {
            var res = data != -1 ? new Node
            {
                Data = data,
                Level = current.Level + 1,
            } : null;

            if (res != null)
            {
                if (dic.TryGetValue(current.Level + 1, out var list))
                    list.Add(res);
                else
                    dic.Add(current.Level + 1, new List<Node> { res });

                queue.Enqueue(res);
            }

            return res;
        };

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var values = indexes[counter++];

            current.Left = get(values[0], current);
            current.Right = get(values[1], current);
        }

        var result = new List<int[]>();

        foreach (var query in queries)
            result.Add(Swap(dic, query, root));

        return result.ToArray();
    }

    static int[] Swap(Dictionary<int, List<Node>> dic, int query, Node root)
    {
        var mul = 1;

        while (query * mul <= dic.Count)
            if (dic.TryGetValue(query * mul++, out var list))
                list.ForEach(x => x.Toggled = !x.Toggled);

        var result = new List<int>();

        result.AddRange(GetForPrint(root.Left));
        result.Add(root.Data);
        result.AddRange(GetForPrint(root.Right));

        return result.ToArray();
    }

    private static IEnumerable<int> GetForPrint(Node node)
    {
        if (node == null) yield break;

        foreach (var item in GetForPrint(node.Left))
            yield return item;

        yield return node.Data;

        foreach (var item in GetForPrint(node.Right))
            yield return item;
    }

    class Node
    {
        Node left;
        Node right;

        public bool Toggled { get; set; }
        public int Level { get; set; }
        public int Data { get; set; }
        public Node Left
        {
            get => Toggled ? right : left;
            set
            {
                if (Toggled) right = value;
                else left = value;
            }
        }
        public Node Right
        {
            get => Toggled ? left : right;
            set
            {
                if (Toggled) left = value;
                else right = value;
            }
        }
    }

    static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine());

        int[][] indexes = new int[n][];

        for (int indexesRowItr = 0; indexesRowItr < n; indexesRowItr++)
        {
            indexes[indexesRowItr] = Array.ConvertAll(Console.ReadLine().Split(' '), indexesTemp => Convert.ToInt32(indexesTemp));
        }

        int queriesCount = Convert.ToInt32(Console.ReadLine());

        int[] queries = new int[queriesCount];

        for (int queriesItr = 0; queriesItr < queriesCount; queriesItr++)
        {
            int queriesItem = Convert.ToInt32(Console.ReadLine());
            queries[queriesItr] = queriesItem;
        }

        int[][] result = swapNodes(indexes, queries);

        textWriter.WriteLine(String.Join("\n", result.Select(x => String.Join(" ", x))));

        textWriter.Flush();
        textWriter.Close();
    }
}
