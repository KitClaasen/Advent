using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace Advent.Year2020
{
    public class Day05 : IDay
    {
        public void Solve()
        {
            var input = Properties.Resources.Year2020Day05;
            var lines = input
                .Split(Environment.NewLine);

            var part1 = SeatIdArray(lines);
            Console.WriteLine(part1[0]);
            Array.Reverse(part1);
            Console.WriteLine(MySeat(part1));
        }

        private int[] SeatIdArray(string[] entries)
        {
            List<int> seatidlist = new List<int>();
            foreach (var item in entries)
            {
                string row = item.Substring(0, 7);
                string column = item.Substring(7, 3);

                int rowseat = SeatFinder(row, 'B', 128);
                int columnseat = SeatFinder(column, 'R', 8);

                int seatid = rowseat * 8 + columnseat;
                seatidlist.Add(seatid);
            }
            int[] seatidarray = seatidlist.ToArray();
            Array.Sort(seatidarray);
            Array.Reverse(seatidarray);
            return seatidarray;
        }

        private int SeatFinder(string mapInput, char back, int maxPosition)
        {
            int n = 0,
                seat = 0;

            var array = mapInput.ToCharArray();
            while (n < mapInput.Length)
            {
                if (array[n] == back) seat += maxPosition / (int)Math.Pow(2, n + 1);
                n++;
            }
            return seat;
        }

        private int MySeat(int[] seatidarray)
        {
            int step = 1;
            var prev = seatidarray[0];
            while (step < seatidarray.Length)
            {
                var id = seatidarray[step];
                if (prev + 2 == id)
                {
                    // yay
                    return prev + 1;
                }
                prev = seatidarray[step];
                step++;
            }
            return 0;
        }
    }
}
        
