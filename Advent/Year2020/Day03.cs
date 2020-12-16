using System;
using System.Collections.Generic;
using System.Text;

namespace Advent.Year2020
{
    public class Day03 : IDay
    {
        public void Solve()
        {
            var input = Properties.Resources.Year2020Day03;
            var lines = input
                .Split(Environment.NewLine);

            long r1d1 = TreeHit(lines, 1, 1),
                r3d1 = TreeHit(lines, 3, 1),
                r5d1 = TreeHit(lines, 5, 1),
                r7d1 = TreeHit(lines, 7, 1),
                r1d2 = TreeHit(lines, 1, 2);
            long part2prod = r1d1 * r3d1 * r5d1 * r7d1 * r1d2;

            Console.WriteLine(TreeHit(lines, 3, 1));
            Console.WriteLine(part2prod);
        }

        private int TreeHit(string[] entries, int across, int down)
        {
            int height = entries.Length,
                width = entries[0].Length,
                x = 0,
                y = 0,
                count = 0;
            do
            {
                x = (x + across) % width;
                y += down;
                if (entries[y][x] == '#') count++;
            } while (y + down < height);

            return count;
        }
    }
}
