using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent.Year2020
{
    public class Day11 : IDay
    {
        public void Solve()
        {
            List<List<char>> charGridList = Properties.Resources.Year2020Day11
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.ToList())
                .ToList();
            foreach (List<char> row in charGridList)
            {
                row.Reverse();
                row.Add('.');
                row.Reverse();
                row.Add('.');
            }
            List<List<char>> floorRowArray = new List<List<char>>() { Enumerable.Repeat('.', charGridList[0].Count()).ToList() };
            charGridList.Reverse();
            charGridList.AddRange(floorRowArray);
            charGridList.Reverse();
            charGridList.AddRange(floorRowArray);

            char[][] charGrid = charGridList.Select(a => a.ToArray()).ToArray();

            char[][] outputArrayPart1 = IterateArrayPart1(charGrid);
            char[][] outputArrayPart2 = IterateArrayPart2(charGrid);

            Console.WriteLine(IterateArrayCount(outputArrayPart1));
            Console.WriteLine(IterateArrayCount(outputArrayPart2));
        }

        private char[][] IterateArrayPart1(char[][] charGrid)
        {
            char[][] outputArrayTemp = new char[charGrid.Length][];
            char[] subOutputArray = new char[charGrid[0].Length];
            for (int i = 0; i < charGrid.Length; i++) outputArrayTemp[i] = subOutputArray;
            char[][] outputArray = outputArrayTemp.Select(a => a.ToArray()).ToArray();

            bool areNotEqual = true;
            while (areNotEqual)
            {
                char[][] newArray = NewArrayPart1(charGrid);

                for (int i = 1; i < charGrid.Length - 1; i++)
                {
                    for (int j = 1; j < charGrid[0].Length - 1; j++)
                    {
                        if (newArray[i][j] != charGrid[i][j])
                        {
                            areNotEqual = true;
                            break;
                        }
                        else areNotEqual = false;
                    }
                    if (areNotEqual) break;
                }
                outputArray = newArray;
                charGrid = newArray;
            }
            return outputArray;
        }

        private char[][] IterateArrayPart2(char[][] charGrid)
        {
            char[][] outputArrayTemp = new char[charGrid.Length][];
            char[] subOutputArray = new char[charGrid[0].Length];
            for (int i = 0; i < charGrid.Length; i++) outputArrayTemp[i] = subOutputArray;
            char[][] outputArray = outputArrayTemp.Select(a => a.ToArray()).ToArray();

            bool areNotEqual = true;
            while (areNotEqual)
            {
                char[][] newArray = NewArrayPart2(charGrid);

                for (int i = 1; i < charGrid.Length - 1; i++)
                {
                    for (int j = 1; j < charGrid[0].Length - 1; j++)
                    {
                        if (newArray[i][j] != charGrid[i][j])
                        {
                            areNotEqual = true;
                            break;
                        }
                        else areNotEqual = false;
                    }
                    if (areNotEqual) break;
                }
                outputArray = newArray;
                charGrid = newArray;
            }
            return outputArray;
        }

        private int IterateArrayCount(char[][] outputArray)
        {
            int occupiedCount = 0;
            for (int i = 1; i < outputArray.Length - 1; i++)
            {
                for (int j = 1; j < outputArray[0].Length - 1; j++)
                {
                    if (outputArray[i][j] == '#') occupiedCount++;
                }
            }
            return occupiedCount;
        }

        private char[][] NewArrayPart1(char[][] charGrid)
        {
            char[][] newGridTemp = new char[charGrid.Length][];
            char[] subNewGrid = new char[charGrid[0].Length];
            Array.Fill(subNewGrid, '.');
            for (int i = 0; i < charGrid.Length; i++) newGridTemp[i] = subNewGrid;
            char[][] newGrid = newGridTemp.Select(a => a.ToArray()).ToArray();

            for (int i = 1; i < charGrid.Length - 1; i++)
            {
                for (int j = 1; j < charGrid[0].Length - 1; j++)
                {
                    char center = charGrid[i][j];
                    if (center == 'L') newGrid[i][j] = 'L';
                    if (center == '#') newGrid[i][j] = '#';

                    bool[] occupiedNeighbors =
                    {
                        charGrid[i - 1][j - 1] == '#',
                        charGrid[i - 1][j] == '#',
                        charGrid[i - 1][j + 1] == '#',
                        charGrid[i][j - 1] == '#',
                        charGrid[i][j + 1] == '#',
                        charGrid[i + 1][j - 1] == '#',
                        charGrid[i + 1][j] == '#',
                        charGrid[i + 1][j + 1] == '#'
                    };
                    int count = 0;
                    foreach (bool item in occupiedNeighbors) if (item == true) count++;

                    if (center == 'L' && count == 0) newGrid[i][j] = '#';
                    if (center == '#' && count >= 4) newGrid[i][j] = 'L';
                }
            }
            return newGrid;
        }

        private char[][] NewArrayPart2(char[][] charGrid)
        {
            char[][] newGridTemp = new char[charGrid.Length][];
            char[] subNewGrid = new char[charGrid[0].Length];
            Array.Fill(subNewGrid, '.');
            for (int i = 0; i < charGrid.Length; i++) newGridTemp[i] = subNewGrid;
            char[][] newGrid = newGridTemp.Select(a => a.ToArray()).ToArray();

            for (int i = 1; i < charGrid.Length - 1; i++)
            {
                for (int j = 1; j < charGrid[0].Length - 1; j++)
                {
                    char center = charGrid[i][j];
                    if (center == 'L') newGrid[i][j] = 'L';
                    if (center == '#') newGrid[i][j] = '#';

                    bool[] occupiedNeighbors =
                    {
                        xAxis(i, j, charGrid)[0] == '#',
                        xAxis(i, j, charGrid)[1] == '#',
                        yAxis(i, j, charGrid)[0] == '#',
                        yAxis(i, j, charGrid)[1] == '#',
                        zAxis(i, j, charGrid)[0] == '#',
                        zAxis(i, j, charGrid)[1] == '#',
                        zAxis(i, j, charGrid)[2] == '#',
                        zAxis(i, j, charGrid)[3] == '#',
                    };
                    int count = 0;
                    foreach (bool item in occupiedNeighbors) if (item == true) count++;

                    if (center == 'L' && count == 0) newGrid[i][j] = '#';
                    if (center == '#' && count >= 5) newGrid[i][j] = 'L';
                }
            }
            return newGrid;
        }

        private char[] xAxis(int i, int j, char[][] charGrid)
        {
            char[] output = { '.', '.' };
            char[] leftSplit = charGrid[i].Take(j).Reverse().ToArray();
            char[] rightSplit = charGrid[i].Skip(j + 1).ToArray();

            output[0] = CheckForChar(leftSplit);
            output[1] = CheckForChar(rightSplit);
            return output;
        }

        private char[] yAxis(int i, int j, char[][] charGrid)
        {
            char[] output = { '.', '.' };
            var aboveSplitList = new List<char>();
            for (int I = i - 1; I > 0; I--) aboveSplitList.Add(charGrid[I][j]);
            char[] aboveSplit = aboveSplitList.ToArray();
            var belowSplitList = new List<char>();
            for (int I = i + 1; I < charGrid.Length - 1; I++) belowSplitList.Add(charGrid[I][j]);
            char[] belowSplit = belowSplitList.ToArray();

            output[0] = CheckForChar(aboveSplit);
            output[1] = CheckForChar(belowSplit);
            return output;
        }

        private char[] zAxis(int i, int j, char[][] charGrid)
        {
            char[] output = { '.', '.', '.', '.' };
            var upRightSplitList = new List<char>();
            for (int I = 1; i - I > 0 && j + I < charGrid[0].Length - 1; I++) upRightSplitList.Add(charGrid[i - I][j + I]);
            char[] upRightSplit = upRightSplitList.ToArray();

            var downRightSplitList = new List<char>();
            for (int I = 1; i + I < charGrid.Length - 1 && j + I < charGrid[0].Length - 1; I++) downRightSplitList.Add(charGrid[i + I][j + I]);
            char[] downRightSplit = downRightSplitList.ToArray();

            var upLeftSplitList = new List<char>();
            for (int I = 1; i - I > 0 && j - I > 0; I++) upLeftSplitList.Add(charGrid[i - I][j - I]);
            char[] upLeftSplit = upLeftSplitList.ToArray();

            var downLeftSplitList = new List<char>();
            for (int I = 1; i + I < charGrid.Length - 1 && j - I > 0; I++) downLeftSplitList.Add(charGrid[i + I][j - I]);
            char[] downLeftSplit = downLeftSplitList.ToArray();

            output[0] = CheckForChar(upRightSplit);
            output[1] = CheckForChar(downRightSplit);
            output[2] = CheckForChar(upLeftSplit);
            output[3] = CheckForChar(downLeftSplit);
            return output;
        }

        private char CheckForChar(char[] array)
        {
            char output = '.';
            for (int index = 0; index < array.Length; index++)
            {
                if (array[index] != '.')
                {
                    output = array[index];
                    break;
                }
            }
            return output;
        }
    }
}