using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent.Year2020
{
    public class Day06 : IDay
    {
        public void Solve()
        {
            string input = Properties.Resources.Year2020Day06;
            string[][] groups = input
                .Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                .ToArray();

            Console.WriteLine(QuestionsSum(groups));
            Console.WriteLine(CommonQuestionsSum(groups));
        }

        private int QuestionsSum(string[][] groups)
        {
            int sum = 0;
            foreach (var group in groups)
            {
                var line = group.SelectMany(g => g.ToCharArray()).Distinct();
                sum += line.Count();
            }
            return sum;
        }

        private int CommonQuestionsSum(string[][] groups)
        {
            int groupSum = 0;
            foreach (string[] group in groups)
            {
                IDictionary<char, int> questionDict = new Dictionary<char, int>();

                foreach (string person in group)
                {
                    foreach (char questionChar in person)
                    {
                        if (!questionDict.ContainsKey(questionChar)) questionDict.Add(questionChar, 0);
                        questionDict[questionChar]++;
                    }
                }
                foreach (KeyValuePair<char, int> question in questionDict)
                {
                    if (question.Value == group.Length) groupSum++;
                }
            }
            return groupSum;
        }
    }
}
