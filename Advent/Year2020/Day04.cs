using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace Advent.Year2020
{
    public class Day04 : IDay
    {
        public void Solve()
        {
            var input = Properties.Resources.Year2020Day04;
            var lines = input
                .Split(Environment.NewLine);

            int point = 0;
            string hold = null;
            List<string> NewLinesList = new List<string>();
            
            while (point < lines.Length)
            {
                if (string.IsNullOrEmpty(lines[point]))
                {
                    NewLinesList.Add(hold);
                    hold = null;
                }
                else
                    hold += $" {lines[point]}";
                point++;
            }
            string[] NewLines = NewLinesList.ToArray();

            Console.WriteLine(ValidSum(NewLines).Count());
        }

        private IEnumerable<string[]> ValidSum(string[] entries)
        {
            foreach (var item in entries)
            {
                string[] part = item.Split(' ').Where(s => s.Length > 0).ToArray();
                bool hasCid = part.Where(s => s.StartsWith("cid:")).Any();

                if (part.Length == 8) yield return part;
                if (part.Length == 7 && !hasCid) yield return part;
            }
        }

        public bool YearCheck(int year, int least, int most)
        {
            bool check = year >= least && year <= most;
            return check;
        }
    }
}
