// Project Euler #244: Sliders
// score: 0.166666666667
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Solution
{
    static void Main(string[] args)
    {
        var size = int.Parse(Console.ReadLine().Trim());

        var data = new string[size];
        for (int index = 0; index < size; index++)
            data[index] = Console.ReadLine().Trim();

        var start = new Puzzle(size);
        start.LoadState(data);

        for (int index = 0; index < size; index++)
            data[index] = Console.ReadLine().Trim();

        var end = new Puzzle(size);
        end.LoadState(data);

        var result = FindThePath(start, end);

        Console.WriteLine(result);
    }

    static long FindThePath(Puzzle start, Puzzle end)
    {
        if(start == end) return 0;

        var dic = new Dictionary<string, long>();
        var queue = new Queue<string>();

        dic.Add(start.ToString(), 0);
        queue.Enqueue(start.ToString());

        while (queue.Count > 0)
        {
            var current = Puzzle.CreateFromSnapshot(queue.Dequeue());
            var checksum = dic[current.ToString()];
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                if(!current.PredictNextState(direction, out var next))
                    continue;

                if(dic.ContainsKey(next)) continue;

                var newCecksum = (checksum * 243 + (byte)direction) % 100_000_007;

                if(next == end.ToString())
                    return newCecksum;

                dic.Add(next, newCecksum);
                queue.Enqueue(next);
            }
        }

        throw new Exception("How is it possible.");
    }
}

enum Direction : byte
{
    Left = 76,
    Right = 82,
    Up = 85,
    Down = 68,
}

static class Extensions
{
    static int[] RowOffset = new[] { 1, 0, -1, 0 };
    static int[] ColOffset = new[] { 0, -1, 0, 1 };
    static int ToIndex(this Direction @this)
    {
        switch (@this)
        {
            case Direction.Up:
                return 0;
            case Direction.Right:
                return 1;
            case Direction.Down:
                return 2;
            case Direction.Left:
                return 3;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public static int ToRowOffset(this Direction @this) => RowOffset[@this.ToIndex()];

    public static int ToColOffset(this Direction @this) => ColOffset[@this.ToIndex()];
}

class Puzzle
{
    readonly int size;
    string snapshot;
    int whiteRow;
    int whiteCol;

    public Puzzle(int size)
    {
        this.size = size;
    }

    public static Puzzle CreateFromSnapshot(string snapshot)
    {
        var parts = snapshot.Split('-');

        var size = (int)Math.Sqrt(parts[1].Length);
        var index = int.Parse(parts[0]);

        var result = new Puzzle(size)
        {
            snapshot = snapshot,
            whiteRow = index / size,
            whiteCol = index - (index / size) * size,
        };

        return result;
    }

    public void LoadState(string[] rawData)
    {
        snapshot = "";
        var index = -1;
        for (int row = 0; row < size; row++)
        {
            snapshot += rawData[row];

            if (index < 0)
                for (int col = 0; col < size; col++)
                    if (rawData[row][col] == 'W')
                    {
                        index = row * size + col;
                        whiteCol = col;
                        whiteRow = row;
                    }
        }

        snapshot = $"{index}-{snapshot}";
    }

    public bool PredictNextState(Direction direction, out string nextState)
    {
        var newRow = whiteRow + direction.ToRowOffset();
        var newCol = whiteCol + direction.ToColOffset();

        nextState = "";

        if (!IsValid(newRow, newCol)) return false;

        var sb = new StringBuilder();

        var first = GetSnapshotIndex(newRow, newCol);
        var second = GetSnapshotIndex(whiteRow, whiteCol);

        var parts = snapshot.Split('-');

        sb.Append(first);
        sb.Append("-");

        if (first > second)
        {
            var temp = second;
            second = first;
            first = temp;
        }

        sb.Append(parts[1].Substring(0, first));
        sb.Append(parts[1][second]);
        sb.Append(parts[1].Substring(first + 1, second - first - 1));
        sb.Append(parts[1][first]);
        sb.Append(parts[1].Substring(second + 1));

        nextState = sb.ToString();
        return true;
    }

    int GetSnapshotIndex(int row, int col) => row * size + col;

    bool IsValid(int row, int col)
    {
        return row >= 0 &&
            col >= 0 &&
            row < size &&
            col < size;
    }

    public override bool Equals(object obj) => this == (Puzzle)obj;

    public override int GetHashCode() => base.GetHashCode();

    public override string ToString() => snapshot;

    public static bool operator ==(Puzzle p1, Puzzle p2) => p1.snapshot == p2.snapshot;

    public static bool operator !=(Puzzle p1, Puzzle p2) => !(p1 == p2);
}
