using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent.Year2020
{
    public class Day13 : IDay
    {
        public void Solve()
        {
            string[] input = Properties.Resources.Year2020Day13
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            string[] IDArrayStrings = input[1]
                .Split(",")
                .Where(i => i != "x")
                .ToArray();
            int earliestTime = int.Parse(input[0]);
            int[] IDArray = Array.ConvertAll(IDArrayStrings, s => int.Parse(s));

            string[] fullIDArray = input[1]
                .Split(",")
                .ToArray();

            Console.WriteLine(EarliestBus(earliestTime, IDArray));
            Console.WriteLine(EarliestTimestamp(fullIDArray, IDArray));
        }

        private int EarliestBus(int earliestTime, int[] IDArray)
        {
            int[] earliestBusTimes = new int[IDArray.Length];
            for (int i = 0; i < IDArray.Length; i++)
            {
                int busID = IDArray[i];
                int busTime = busID;
                while (busTime < earliestTime) busTime += busID;
                earliestBusTimes[i] = busTime;
            }
            int minIndex = Array.IndexOf(earliestBusTimes, earliestBusTimes.Min());

            int output = IDArray[minIndex] * (earliestBusTimes.Min() - earliestTime);
            return output;
        }

        private long EarliestTimestamp(string[] fullIDArray, int[] IDArray)
        {
            int[] timings = new int[IDArray.Length];
            for (int i = 0; i < IDArray.Length; i++) timings[i] = Array.IndexOf(fullIDArray, IDArray[i].ToString());
            long time = 0,
                stepSize = IDArray[0],
                nextDepart = timings[1];
            int currentBusCheck = IDArray[1],
                count = 1;

            while(true)
            {
                if (nextDepart % currentBusCheck == 0)
                { 
                    count++;
                    if (count == IDArray.Length) break;
                    stepSize *= currentBusCheck;
                    currentBusCheck = IDArray[count];
                    nextDepart = time + timings[count];
                }
                else
                {
                    time += stepSize;
                    nextDepart += stepSize;
                }
            }
            return time;
        }
    }
}
