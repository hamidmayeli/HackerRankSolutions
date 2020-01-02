// https://www.hackerrank.com/challenges/ctci-queue-using-two-stacks/problem
// Not efficient enough yet
using System;
using System.Collections.Generic;
using System.IO;
class Solution
{
    static void Main(String[] args)
    {
        var count = int.Parse(Console.ReadLine());

        var queue = new MyQueue();

        for (int index = 0; index < count; index++)
        {
            var command = Console.ReadLine();
            var parts = command.Split(' ');

            if (parts[0] == "1")
                queue.Enqueue(int.Parse(parts[1]));

            else if (parts[0] == "2")
                queue.Dequeue();

            else if (parts[0] == "3")
                Console.WriteLine(queue.Peek());
        }
    }
}

class MyQueue
{
    Stack<int> first = new Stack<int>();
    Stack<int> second = new Stack<int>();
    bool Toggled;

    Stack<int> Primary => Toggled ? second : first;
    Stack<int> Secondary => Toggled ? first : second;

    public int Dequeue()
    {
        if (!Toggled) Revert();
        return Primary.Pop();
    }

    public void Enqueue(int value)
    {
        if(Toggled) Revert();

        Primary.Push(value);
    }

    public int Peek()
    {
        if (!Toggled) Revert();
        return Primary.Peek();
    }

    void Revert()
    {
        Secondary.Clear();

        while (Primary.Count > 0)
            Secondary.Push(Primary.Pop());

        Toggled = !Toggled;
    }
}
