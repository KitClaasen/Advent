using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent.Year2020
{
    public class Day12 : IDay
    {
        public void Solve()
        {
            string[] input = Properties.Resources.Year2020Day12
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            string[][] stringArrayTemp = new string[input.Length][];
            string[] substringArray = new string[2];
            for (int i = 0; i < stringArrayTemp.Length; i++) stringArrayTemp[i] = substringArray;
            string[][] stringArray = stringArrayTemp.Select(a => a.ToArray()).ToArray();

            for (int i = 0; i < input.Length; i++)
            {
                stringArray[i][0] = input[i].Substring(0, 1);
                stringArray[i][1] = input[i].Substring(1);
            }
            Console.WriteLine(ManhattanDistancePart1(stringArray));
            Console.WriteLine(ManhattanDistancePart2(stringArray));
        }

        private int ManhattanDistancePart1(string[][] stringArray)
        {
            int north = 0,
                east = 0,
                facing = 0;
            for (int i = 0; i < stringArray.Length; i++)
            {
                int value = int.Parse(stringArray[i][1]);
                switch (stringArray[i][0])
                {
                    case ("N"):
                        north += value;
                        break;
                    case ("S"):
                        north -= value;
                        break;
                    case ("E"):
                        east += value;
                        break;
                    case ("W"):
                        east -= value;
                        break;
                    case ("L"):
                        facing += value;
                        facing = (facing + 360) % 360;
                        break;
                    case ("R"):
                        facing -= value;
                        facing = (facing + 360) % 360;
                        break;
                    case ("F"):
                        switch(facing)
                        {
                            case (0):
                                east += value;
                                break;
                            case (90):
                                north += value;
                                break;
                            case (180):
                                east -= value;
                                break;
                            case (270):
                                north -= int.Parse(stringArray[i][1]);
                                break;
                        }
                        break;
                }
            }
            int manhattanDistance = Math.Abs(north) + Math.Abs(east);
            return manhattanDistance;
        }

        private int ManhattanDistancePart2(string[][] stringArray)
        {
            int northShip = 0,
                eastShip = 0,
                northShipTemp = 0,
                eastShipTemp = 0,
                northWaypoint = 1,
                eastWaypoint = 10,
                northWaypointTemp = 1,
                eastWaypointTemp = 10;
            for (int i = 0; i < stringArray.Length; i++)
            {
                int value = int.Parse(stringArray[i][1]);
                switch (stringArray[i][0])
                {
                    case ("N"):
                        northWaypointTemp += value;
                        break;
                    case ("S"):
                        northWaypointTemp -= value;
                        break;
                    case ("E"):
                        eastWaypointTemp += value;
                        break;
                    case ("W"):
                        eastWaypointTemp -= value;
                        break;
                    case ("L"):
                        if (value < 0) value = (value + 360) % 360;
                        switch (value)
                        {
                            case (0):
                                break;
                            case (90):
                                northWaypointTemp = (eastWaypoint - eastShip) + northShip;
                                eastWaypointTemp = -(northWaypoint - northShip) + eastShip;
                                break;
                            case (180):
                                northWaypointTemp = - (northWaypoint - northShip) + northShip;
                                eastWaypointTemp = -(eastWaypoint - eastShip) + eastShip;
                                break;
                            case (270):
                                northWaypointTemp = -(eastWaypoint - eastShip) + northShip;
                                eastWaypointTemp = (northWaypoint - northShip) + eastShip;
                                break;
                        }
                        break;
                    case ("R"):
                        if (value < 0) value = (value + 360) % 360;
                        switch (value)
                        {
                            case (0):
                                break;
                            case (90):
                                northWaypointTemp = -(eastWaypoint - eastShip) + northShip;
                                eastWaypointTemp = (northWaypoint - northShip) + eastShip;
                                break;
                            case (180):
                                northWaypointTemp = -(northWaypoint - northShip) + northShip;
                                eastWaypointTemp = -(eastWaypoint - eastShip) + eastShip;
                                break;
                            case (270):
                                northWaypointTemp = (eastWaypoint - eastShip) + northShip;
                                eastWaypointTemp = -(northWaypoint - northShip) + eastShip;
                                break;
                        }
                        break;
                    case ("F"):
                        northShipTemp += value * (northWaypoint - northShip);
                        eastShipTemp += value * (eastWaypoint - eastShip);
                        northWaypointTemp += northShipTemp - northShip;
                        eastWaypointTemp += eastShipTemp - eastShip;
                        break;
                }
                northShip = northShipTemp;
                eastShip = eastShipTemp;
                northWaypoint = northWaypointTemp;
                eastWaypoint = eastWaypointTemp;
            }
            int manhattanDistance = Math.Abs(northShip) + Math.Abs(eastShip);
            return manhattanDistance;
        }
    }
}
