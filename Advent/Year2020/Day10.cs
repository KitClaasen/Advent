using System;
using System.Collections.Generic;
using System.Text;

namespace Advent.Year2020
{
    public class Day10 : IDay
    {
        public void Solve()
        { 
            var stringInput = Properties.Resources.Year2020Day10
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            var intInput = Array.ConvertAll(stringInput, int.Parse);

            int[] tempAddInputLow = { 0 };
            var inputTemp = new int[intInput.Length + 1];
            tempAddInputLow.CopyTo(inputTemp, 0);
            intInput.CopyTo(inputTemp, 1);
            Array.Sort(inputTemp);

            int[] tempAddInputHigh = { inputTemp[inputTemp.Length - 1] + 3 };
            var input = new int[inputTemp.Length + 1];
            inputTemp.CopyTo(input, 0);
            tempAddInputHigh.CopyTo(input, inputTemp.Length);
            Array.Sort(input);

            var indexDict = new Dictionary<int, long>();

            Console.WriteLine(CountDifferences(input));
            Console.WriteLine(DistinctSolutionsRecurcive(input, indexDict));
        }

        private int CountDifferences(int[] input)
        {
            int[] outputArray = { 0, 0, 0 };
            for (int i = 0; i < input.Length - 1; i++)
            {
                int differnece = input[i + 1] - input[i];
                switch (differnece)
                {
                    case (1):
                        outputArray[0]++;
                        break;
                    case (2):
                        outputArray[1]++;
                        break;
                    case (3):
                        outputArray[2]++;
                        break;
                }
            }
            return outputArray[0] * outputArray[2];
        }
        private long DistinctSolutionsRecurcive(int[] input, Dictionary<int, long> indexDict, int index = 0)
        {
            if (indexDict.ContainsKey(index)) return indexDict[index];
            long count = 0;
            if (index == input.Length - 1) return 1;
            if (input[index + 1 % input.Length] - input[index] <= 3 && input[index + 1 % input.Length] - input[index] > 0)
            {
                count += DistinctSolutionsRecurcive(input, indexDict, index + 1);
            }
            if (input[(index + 2) % input.Length] - input[index] <= 3 && input[(index + 2) % input.Length] - input[index] > 0)
            {
                count += DistinctSolutionsRecurcive(input, indexDict, index + 2);
            }
            if (input[(index + 3) % input.Length] - input[index] <= 3 && input[(index + 3) % input.Length] - input[index] > 0)
            {
                count += DistinctSolutionsRecurcive(input, indexDict, index + 3);
            }
            indexDict.Add(index, count);
            return count;
        }
    }
}