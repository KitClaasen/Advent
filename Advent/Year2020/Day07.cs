using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent.Year2020
{
    public class Day07 : IDay
    {
        public void Solve()
        {
            var input = Properties.Resources.Year2020Day07;
            string[] lines = input
                .Split(new[] { "\r\n", "\n" },
                StringSplitOptions.None)
                .Where(s => s.Length > 0).ToArray();

            var bagDict = new Dictionary<string, IEnumerable<(string BagName, int Count)>>();

            foreach (string baginfo in lines)
            {
                string[] bagInfoSplit = baginfo.Split(" bags contain ");

                var parentBag = bagInfoSplit[0];
                var childBags = new List<(string, int)>();

                Regex rgx = new Regex(@"(\d+) ((?: |\w)+)(?= bags?)");
                foreach (Match bags in rgx.Matches(bagInfoSplit[1]))
                {
                    string a = bags.Groups[2].ToString();
                    int b = int.Parse(bags.Groups[1].ToString());

                    childBags.Add((a, b));
                }
                bagDict.Add(parentBag, childBags);
            }
            Console.WriteLine(ContainsGoldCount(bagDict));
            Console.WriteLine(BagCountGoldContains("shiny gold",bagDict));
        }

        private int ContainsGoldSplit(string key, IDictionary<string, IEnumerable<(string BagName, int Count)>> bagDict,int subCount)
        {
            subCount = 0;
            if (key == "shiny gold") subCount++;
            foreach ((string BagName, int Count) subBag in bagDict[key])
            {
                if (bagDict[key].Count() == 0) continue;
                else subCount += ContainsGoldSplit(subBag.BagName, bagDict, subCount);
            }
            return subCount;
        }

        private int ContainsGoldCount(IDictionary<string, IEnumerable<(string BagName, int Count)>> bagDict)
        {
            int count = 0;
            foreach(KeyValuePair<string, IEnumerable<(string BagName, int Count)>> bag in bagDict) if (ContainsGoldSplit(bag.Key, bagDict, 0) > 0) count++;
            return count -1;
        }

        private int BagCountGoldContains(string key, IDictionary<string, IEnumerable<(string BagName, int Count)>> bagDict)
        {
            int count = 0;
            if (bagDict[key].Count() == 0) return 0;
            foreach ((string BagName, int Count) subBag in bagDict[key]) count += subBag.Count * (BagCountGoldContains(subBag.BagName, bagDict) + 1);
            return count;
        }
    }
}

