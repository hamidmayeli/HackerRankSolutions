// Fraudulent Activity Notifications
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

    // Complete the activityNotifications function below.
    static int activityNotifications(int[] expenditure, int d)
    {
        int count = 0;
        // create freq array as exp <= 200 always
        //maintain a queue to find ontgoing and incoming exp
        //get median from freq array

        var freq = new int[201];
        List<int> q = new List<int>();

        for (int i = 0; i < expenditure.Length; i++)
        {
            while (i < d)
            {
                q.Add(expenditure[i]);
                freq[expenditure[i]] = freq[expenditure[i]] + 1;
                i++;
            }


            float median = getMedian(freq, d);

            if (expenditure[i] >= 2 * median)
            {
                count++;
            }


            // removing outing going element freq
            int elem = q[0];
            q.RemoveAt(0);
            freq[elem] = freq[elem] - 1;

            // adding incoming element to freq
            q.Add(expenditure[i]);
            freq[expenditure[i]] = freq[expenditure[i]] + 1;
        }

        return count;
    }

    private static float getMedian(int[] freq, int d)
    {
        if (d % 2 == 1)
        {
            int center = 0;
            for (int i = 0; i < freq.Length; i++)
            {
                center = center + freq[i];

                if (center > d / 2)
                {
                    return i;
                }
            }
        }
        else
        {
            int count = 0;
            int first = -1;
            int second = -1;
            for (int i = 0; i < freq.Length; i++)
            {
                count = count + freq[i];

                if (count == d / 2)
                {
                    first = i;
                }
                else if (count > d / 2)
                {
                    if (first < 0) first = i;
                    second = i;

                    return ((float)first + (float)second) / 2;
                }
            }
        }
        return 0f;
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] nd = Console.ReadLine().Split(' ');

        int n = Convert.ToInt32(nd[0]);

        int d = Convert.ToInt32(nd[1]);

        int[] expenditure = Array.ConvertAll(Console.ReadLine().Split(' '), expenditureTemp => Convert.ToInt32(expenditureTemp))
        ;
        int result = activityNotifications(expenditure, d);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
