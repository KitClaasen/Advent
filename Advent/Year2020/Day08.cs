using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent.Year2020
{
    public class Day08 : IDay
    {
        public void Solve()
        {
            var input = Properties.Resources.Year2020Day08;
            string[] instructionInput = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            Console.WriteLine(AccumulatorCount(instructionInput)[0]);
            Console.WriteLine(AccumulatorAfterTerminate(instructionInput));
        }

        private int[] AccumulatorCount(string[] instructionInput)
        {
            bool loop = true;
            int index = 0,
                accumulator = 0,
                finishedTest = 0;
            var indexList = new List<int>();
            while (loop)
            {
                indexList.Add(index);

                string[] indexInput = instructionInput[index].Split(" ").ToArray();
                string inputName = indexInput[0];
                int inputValue = int.Parse(indexInput[1]);

                switch (inputName)
                {
                    case ("acc"):
                        accumulator += inputValue;
                        index++;
                        break;
                    case ("jmp"):
                        index += inputValue;
                        break;
                    case ("nop"):
                        index++;
                        break;
                }
                if (indexList.Contains(index)) loop = false;
                if (index >= instructionInput.Count())
                {
                    loop = false;
                    finishedTest++;
                }
            }
            var outputList = new List<int>();
            outputList.Add(accumulator);
            outputList.Add(finishedTest);
            int[] outputArray = outputList.ToArray();
            return outputArray;
        }

        private int AccumulatorAfterTerminate(string[] instructionInput)
        {
            int accumulator = 0;
            foreach (int index in Enumerable.Range(0, instructionInput.Count() - 1))
            {
                string[] indexInput = instructionInput[index].Split(" ").ToArray();
                string inputName = indexInput[0];
                string inputValue = indexInput[1];
                switch (inputName)
                {
                    case ("acc"):
                        break;
                    case ("jmp"):
                        instructionInput[index] = "nop "+ inputValue;
                        int[] accumulatorTemp1 = AccumulatorCount(instructionInput);
                        if (accumulatorTemp1[1] == 1) accumulator = accumulatorTemp1[0];
                        instructionInput[index] = "jmp " + inputValue;
                        break;
                    case ("nop"):
                        instructionInput[index] = "jmp " + inputValue;
                        int[] accumulatorTemp2 = AccumulatorCount(instructionInput);
                        if (accumulatorTemp2[1] == 1) accumulator = accumulatorTemp2[0];
                        instructionInput[index] = "nop " + inputValue;
                        break;
                }
                if (accumulator != 0) break;
            }
            return accumulator;
        }
    }
}
        