// Matrix
// score: 0.875
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

    // Complete the minTime function below.
    static int minTime(int[][] rawRoads, int[] machines)
    {
        var calculator = new Calculator(machines);

        var sum = 0;

        var roads = new Road[rawRoads.Length];

        for (int index = 0; index < rawRoads.Length; index++)
            roads[index] = new Road
            {
                From = rawRoads[index][0],
                To = rawRoads[index][1],
                Weight = rawRoads[index][2]
            };

        Array.Sort(roads);

        foreach (var road in roads)
        {
            var temp = calculator.AddRoadAndCheckIfCausedIssue(road.From, road.To, road.Weight);

            if (temp.CausedIssue)
            {
                sum += temp.MinWeight;
                calculator.Disconnect(temp.Left, temp.Right);
            }
        }

        return sum;
    }

    class Calculator
    {
        readonly Dictionary<int, List<Road>> Connections = new Dictionary<int, List<Road>>();
        readonly int[] Machines;

        public Calculator(int[] machines)
        {
            Machines = machines;
            Array.Sort(Machines);
        }

        public void Disconnect(int left, int right)
        {
            DisconnectOnSide(left, right);
            DisconnectOnSide(right, left);
        }

        private void DisconnectOnSide(int from, int to)
        {
            if (Connections.TryGetValue(from, out var list))
                list.Remove(list.First(x => x.To == to));
            else
                throw new InvalidOperationException("They are already disconnected.");
        }

        public AddRoadResult AddRoadAndCheckIfCausedIssue(
            int left,
            int right,
            int weight)
        {
            AddToDic(left, right, weight);
            AddToDic(right, left, weight);

            var minWeightRoad = new SearchContext(this, left).GetMinWeightRoad();

            var result = new AddRoadResult
            {
                CausedIssue = minWeightRoad.Weight != int.MaxValue,
                MinWeight = minWeightRoad.Weight,
                Left = minWeightRoad.From,
                Right = minWeightRoad.To,
            };

            return result;
        }

        class SearchContext
        {
            readonly Calculator Calculator;
            readonly Queue<int> Queue = new Queue<int>();
            readonly Dictionary<int, bool> AddedToQueueState = new Dictionary<int, bool>();
            readonly Dictionary<int, bool> VisitingState = new Dictionary<int, bool>();

            public SearchContext(Calculator calculator, int root)
            {
                Calculator = calculator;
                Queue.Enqueue(root);
                AddedToQueueState.Add(root, true);
            }

            public Road GetMinWeightRoad()
            {
                var machines = new List<int>();

                while (Queue.Count > 0)
                {
                    var current = Queue.Dequeue();
                    if (Array.BinarySearch(Calculator.Machines, current) >= 0)
                    {
                        machines.Add(current);
                        if (machines.Count == 2)
                            break;
                    }

                    foreach (var road in Calculator.Connections[current])
                    {
                        if (!AddedToQueueState.TryGetValue(road.To, out var value))
                        {
                            Queue.Enqueue(road.To);
                            AddedToQueueState.Add(road.To, true);
                        }
                    }
                }

                if (machines.Count < 2) return new Road { Weight = int.MaxValue };

                return GetShortestRoad(machines[0], machines[1]);
            }

            private Road GetShortestRoad(int from, int to)
            {
                VisitingState.Add(from, true);

                var linkes = Calculator.Connections[from];

                foreach (var link in linkes)
                {
                    if (link.To == to) return link;

                    if(VisitingState.ContainsKey(link.To)) continue;

                    var temp = GetShortestRoad(link.To, to);
                    if (temp != null)
                        return link.Weight < temp.Weight ? link : temp;
                }

                return null;
            }
        }

        private void AddToDic(int from, int to, int weight)
        {
            var item = new Road { From = from, To = to, Weight = weight };

            if (Connections.TryGetValue(from, out var list))
                list.Add(item);
            else
                Connections.Add(from, new List<Road> { item });
        }
    }

    class AddRoadResult
    {
        public bool CausedIssue;
        public int MinWeight;
        public int Left;
        public int Right;

        public override string ToString() => $"{CausedIssue} => {Left} <=> {Right} ({MinWeight})";
    }

    class Road : IComparable<Road>
    {
        public int From;
        public int To;
        public int Weight;

        public int CompareTo(Road other) => Weight.CompareTo(other.Weight);

        public override string ToString() => $"{From} => {To} : {Weight}";
    }



    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] nk = Console.ReadLine().Split(' ');

        int n = Convert.ToInt32(nk[0].Trim());

        int k = Convert.ToInt32(nk[1].Trim());

        int[][] roads = new int[n - 1][];

        for (int i = 0; i < n - 1; i++) {
            roads[i] = Array.ConvertAll(Console.ReadLine().Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries), roadsTemp => Convert.ToInt32(roadsTemp.Trim()));
        }

        int[] machines = new int [k];

        for (int i = 0; i < k; i++) {
            int machinesItem = Convert.ToInt32(Console.ReadLine());
            machines[i] = machinesItem;
        }

        int result = minTime(roads, machines);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
