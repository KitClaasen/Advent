using Advent.Year2020;
using System;

namespace Advent
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var day01 = new Day01();
            day01.Solve();

            var input = Properties.Resources.Year2020Day01;
            var lines =input.Split(Environment.NewLine);
        }
    }
}
