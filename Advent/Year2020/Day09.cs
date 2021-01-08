using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent.Year2020
{
    public class Day09 : IDay
    {
        public void Solve()
        {
            var inputLinesString = Properties.Resources.Year2020Day09.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            long[] inputLines = Array.ConvertAll(inputLinesString, long.Parse);

            long part1Value = NotInSum(inputLines);
            Console.WriteLine(part1Value);
            Console.WriteLine(WhileLoops(part1Value, inputLines));
        }

        private long NotInSum(long[] inputLines)
        {
            var summationList = CreateSummationTable(inputLines);
            int index = 25;
            long outNum = 0;
            while (true)
            {
                var collectiveList = summationList
                    .SelectMany(list => list)
                    .Distinct()
                    .ToList();

                if (!collectiveList.Contains(inputLines[index]))
                {
                    outNum = inputLines[index];
                    break;
                }
                summationList = NewSummationTable(summationList, inputLines, index);
                index++;
            }
            return outNum;
        }

        private List<List<long>> CreateSummationTable(long[] inputLines)
        {
            var summationList = new List<List<long>>();
            for (int i = 0; i < 24; i++)
            {
                var childList = new List<long>();
                for (int n = i + 1; n <= 24; n++) childList.Add(inputLines[i] + inputLines[n]);
                summationList.Add(childList);
            }
            return summationList;
        }

        private List<List<long>> NewSummationTable(List<List<long>> summationList, long[] inputLines, int index)
        {
            summationList.RemoveAt(0);
            for (int i = 0; i < 23; i++) summationList[i].Add(inputLines[index - 24 + i] + inputLines[index]);
            var childList = new List<long>();
            childList.Add(inputLines[index - 1] + inputLines[index]);
            summationList.Add(childList);
            return summationList;
        }

        private long WhileLoops(long goalValue, long[] inputLines)
        {
            var indexRange = new List<int>();
            for (int i = 0; i < inputLines.Length; i++)
            {
                long sum = 0;
                int index = i;
                bool foundGoalCheck = false;
                while (sum <= goalValue)
                {
                    sum += inputLines[index];
                    if (sum == goalValue)
                    {
                        indexRange.Add(i);
                        indexRange.Add(index);
                        foundGoalCheck = true;
                        break;
                    }
                    index++;
                }
                if (foundGoalCheck == true) break;
            }
            var sumList = new List<long>();
            for (int n = indexRange[0]; n <= indexRange[1]; n++) sumList.Add(inputLines[n]);
            sumList.Sort();
            long weakness = sumList[0] + sumList[sumList.Count() - 1];
            return weakness;
        }
    }
}
