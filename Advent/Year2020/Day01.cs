using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent.Year2020
{
    public class Day01 : IDay
    {
        public void Solve()
        {
            var input = Properties.Resources.Year2020Day01;
            var lines = input
                .Split(Environment.NewLine)
                .Select(entry => int.Parse(entry))
                .ToArray();

            var val1 = FindProdPart1(lines);
            var val2 = FindProdPart2(lines);
            Console.WriteLine(val1);
            Console.WriteLine(val2);
        }
 

        private int FindProdPart1(int[] entries)
        {
            const int result = 2020;
            int i = 0,
                prod = 0;
            var found = false;

            while (i < entries.Length - 2)
            {
                if (found) break;
                int j = i + 1;

                while (j < entries.Length - 1)
                {
                    if (entries[i] + entries[j] == result)
                    {
                        found = true;
                        prod = entries[i] * entries[j];
                    }

                    if (found) break;
                    j++;
                }
                i++;
            }
            return prod;
        }


        private int FindProdPart2(int[] entries)
        {
            const int result = 2020;
            int i = 0,
                prod = 0;
            var found = false;


            while (i < entries.Length - 2)
            {
                if (found) break;
                int j = i + 1;

                while (j < entries.Length - 1)
                {
                    if (found) break;
                    int k = j + 1;

                    while (k < entries.Length)
                    {
                        if (entries[i] + entries[j] + entries[k] == result)
                        {
                            found = true;
                            prod = entries[i] * entries[j] * entries[k];
                        }

                        if (found) break;
                        k++;
                    }
                    j++;
                }
                i++;
            }
            return prod;
        }
    }
}
