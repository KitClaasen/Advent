using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent.Year2020
{
    public class Day02 : IDay
    {
        public void Solve()
        {
            var input = Properties.Resources.Year2020Day02;
            var lines = input
                .Split(Environment.NewLine);

            Console.WriteLine(ValSumPart1(lines));
            Console.WriteLine(ValSumPart2(lines));
        }

        private int ValSumPart1(string[] entries)
        {
            int count = 0;

            foreach (var item in entries)
            {
                char[] splitchar = { '-', ' ', ':' };
                string[] part = item.Split(splitchar).Where(s => s.Length > 0).ToArray();

                int lower = int.Parse(part[0]),
                    higher = int.Parse(part[1]);
                char search = char.Parse(part[2]);
                string pass = part[3];

                int charcount = pass.ToCharArray().Where(c => c == search).ToArray().Length;

                if (charcount >= lower && charcount <= higher) count++;
            }

            return count;
        }

        private int ValSumPart2(string[] entries)
        {
            int count = 0;

            foreach (var item in entries)
            {
                char[] splitchar = { '-', ' ', ':' };
                string[] part = item.Split(splitchar).Where(s => s.Length > 0).ToArray();

                int lower = int.Parse(part[0]),
                    higher = int.Parse(part[1]);
                char search = char.Parse(part[2]);
                string pass = part[3];

                if (pass[lower - 1] == search && pass[higher - 1] != search) count++;
                if (pass[lower - 1] != search && pass[higher - 1] == search) count++;
            }

            return count;
        }
    }
}
