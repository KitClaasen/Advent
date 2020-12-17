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

            Console.WriteLine(ValidSumPart1(NewLines).Count());
            Console.WriteLine(ValidSumPart2(NewLines));
        }

        private IEnumerable<string[]> ValidSumPart1(string[] entries)
        {
            foreach (var item in entries)
            {
                string[] part = item.Split(' ').Where(s => s.Length > 0).ToArray();
                bool hasCid = part.Where(s => s.StartsWith("cid:")).Any();

                if (part.Length == 8) yield return part;
                if (part.Length == 7 && !hasCid) yield return part;
            }
        }

        private int ValidSumPart2(string[] entries)
        {
            int count = 0;
            const int
                byrleast = 1920,
                byrmost = 2002,
                iyrleast = 2010,
                iyrmost = 2020,
                eyrleast = 2020,
                eyrmost = 2030;

            foreach (string[] item in ValidSumPart1(entries))
            {
                var valid = true;
                foreach(var part in item)
                {
                    var parts = part.Split(':');
                    var value = parts[1].Replace(" ", string.Empty);
                    switch (parts[0])
                    {
                        case "byr":
                            valid = YearCheck(value, byrleast, byrmost);
                            break;
                        case "iyr":
                            valid = YearCheck(value, iyrleast, iyrmost);
                            break;
                        case "eyr":
                            valid = YearCheck(value, eyrleast, eyrmost);
                            break;
                        case "hgt":
                            valid = HeightCheck(value);
                            break;
                        case "hcl":
                            valid = HairCheck(value);
                            break;
                        case "ecl":
                            valid = EyeCheck(value);
                            break;
                        case "pid":
                            valid = PassportCheck(value);
                            break;
                    }
                    if (!valid) break;
                }
                if (valid) count++;
            }

            return count;
        }


        private bool YearCheck(string year, int least, int most)
        {
            Regex rgx = new Regex(@"^\d{4}$");
            bool check = rgx.IsMatch(year) && int.Parse(year) >= least && int.Parse(year) <= most;
            return check;
        }

        private bool HeightCheck(string height)
        {
            const int
                hgtcmleast = 150,
                hgtcmmost = 193,
                hgtinleast = 59,
                hgtinmost = 76;

            if (height.EndsWith("cm"))
            {
                Regex rgx = new Regex(@"\d+(?=cm)");
                var match = rgx.Matches(height);
                if (!match.Any()) return false;
                var num = match[0].Value;
                bool check = int.Parse(num) >= hgtcmleast && int.Parse(num) <= hgtcmmost;
                return check;
            }
            else if (height.EndsWith("in"))
            {
                Regex rgx = new Regex(@"\d+(?=in)");
                var match = rgx.Matches(height);
                if (!match.Any()) return false;
                var num = match[0].Value;
                bool check = int.Parse(num) >= hgtinleast && int.Parse(num) <= hgtinmost;
                return check;
            }
            else return false;
        }

        private bool HairCheck(string hair)
        {
            Regex rgx = new Regex(@"^#([0-9a-f]){6}$");
            bool check = rgx.IsMatch(hair);
            return check;
        }

        private bool EyeCheck(string colour)
        {
            string[] hairtypes = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            return hairtypes.Contains(colour);
        }

        private bool PassportCheck(string passport)
        {
            Regex rgx = new Regex(@"^[0-9]{9}$");
            bool check = rgx.IsMatch(passport);
            return check;
        }
    }
}
