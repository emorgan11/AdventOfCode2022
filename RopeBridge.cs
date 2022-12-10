using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class RopeBridge
    {
        public RopeBridge()
        {
            Console.WriteLine("\r\n\r\nRope Bridge, GO!\r\n");
            Console.WriteLine("----------------------------------");
            var splitInput = input.Replace("\r", null).Split('\n');

            Position tail = new();
            Position head = new();
            Position start = new();

            start.numTailVisits = 1;
            grid.Add(start, 1);
            PrintHeadTail(5);
            foreach(string s in splitInput)
            {
                var moveInput = s.Split(" ");
                Move(moveInput[0], int.Parse(moveInput[1]), head, tail);
                //Console.WriteLine("({0},{1})--({2},{3}) {4}", head.x, head.y, tail.x, tail.y, s);// + "--" + tail);
                var dist = Math.Sqrt(Math.Pow(head.x - tail.x, 2) + Math.Pow(head.y - tail.y, 2));
                if (dist >= 2)
                {
                    throw new Exception("doh");
                }
            }
        }

        public void FindPositionsVisited(int atleast)
        {
            int visits = 0;
            foreach(var pos in grid)
            {
                if(pos.Value >= 1)
                {
                    visits++;
                }
            }
            Console.WriteLine("tail visited {0} positions at least {1} times", visits, atleast);
        }

        private void Move(string dir, int count, Position head, Position tail)
        {
            Console.WriteLine("{0} {1}", dir, count);
            for (int i=0;i<count;i++)
            {
                var relative = FindHeadRelativeToTail(head, tail);
                var movement = DetermineTailMovementRelativeToHead(relative, dir);
                
                
                MoveHead(dir, head);

                // move tail. Note: even if tail doesn't move, it will still add a "visit". maybe bad later?
                tail.x = tail.x + movement.Item1;
                tail.y = tail.y + movement.Item2;

                //Don't add tail object, it was getting updated in the grid.
                Position newTailPosition = new();
                newTailPosition.x = tail.x;
                newTailPosition.y = tail.y;
                if (grid.ContainsKey(newTailPosition))
                {
                    grid[newTailPosition]++;//.numTailVisits++;
                }
                else
                {
                    grid.Add(newTailPosition, 1);
                }

                //Console.WriteLine("({0},{1})--({2},{3})", head.x, head.y, tail.x, tail.y);
                //PrintHeadTail(FindHeadRelativeToTail(head, tail));
                
            }
            //Console.Read();
        }

        private void PrintHeadTail(int cardinalPosition)
        {
            for (int i=0;i<3;i++)
            {
                for(int j=0;j<3;j++)
                {
                    if(i==0 && j==0 && cardinalPosition==7 ||
                        i == 0 && j == 1 && cardinalPosition == 8 ||
                        i == 0 && j == 2 && cardinalPosition == 9 ||
                        i == 1 && j == 0 && cardinalPosition == 4 ||
                        i == 1 && j == 2 && cardinalPosition == 6 ||
                        i == 2 && j == 0 && cardinalPosition == 1 ||
                        i == 2 && j == 1 && cardinalPosition == 2 ||
                        i == 2 && j == 2 && cardinalPosition == 3)
                    {
                        Console.Write("H");
                    }
                    else if(i == 1 && j == 1 && cardinalPosition != 5)
                    {
                        Console.Write("T");
                    }
                    else if(i == 1 && j == 1 && cardinalPosition == 5)
                    {
                        Console.Write("H");
                    }
                    else
                    {
                        Console.Write("*");
                    }
                }
                Console.Write("\r\n");
            }
            Console.WriteLine();
        }

        private void MoveHead(string dir, Position head)
        {
            //Position newHeadPos = new();
            if (dir.Equals("U"))
            {
                head.y++;
            }
            else if (dir.Equals("D"))
            {
                head.y--;
            }
            else if (dir.Equals("L"))
            {
                head.x--;
            }
            else if (dir.Equals("R"))
            {
                head.x++;
            }
            //return newHeadPos;
        }

        private Tuple<int,int> DetermineTailMovementRelativeToHead(int headPos, string dir)
        {
            switch(headPos)
            {
                case 1:
                    switch(dir)
                    {
                        case "U": return new Tuple<int, int>(0, 0);
                        case "D": return new Tuple<int, int>(-1, -1);
                        case "L": return new Tuple<int, int>(-1, -1);
                        case "R":return new Tuple<int, int>(0, 0);
                    }
                    break;
                case 2:
                    switch (dir)
                    {
                        case "U": return new Tuple<int, int>(0, 0); 
                        case "D": return new Tuple<int, int>(0, -1); 
                        case "L": return new Tuple<int, int>(0, 0); 
                        case "R": return new Tuple<int, int>(0, 0); 
                    }
                    break;
                case 3:
                    switch (dir)
                    {
                        case "U": return new Tuple<int, int>(0, 0); 
                        case "D": return new Tuple<int, int>(1, -1); 
                        case "L": return new Tuple<int, int>(0, 0); 
                        case "R": return new Tuple<int, int>(1, -1); 
                    }
                    break;
                case 4:
                    switch (dir)
                    {
                        case "U": return new Tuple<int, int>(0, 0); 
                        case "D": return new Tuple<int, int>(0, 0); 
                        case "L": return new Tuple<int, int>(-1, 0);
                        case "R": return new Tuple<int, int>(0, 0); 
                    }
                    break;
                case 5:
                    return new Tuple<int, int>(0, 0); break;
                case 6:
                    switch (dir)
                    {
                        case "U": return new Tuple<int, int>(0, 0); 
                        case "D": return new Tuple<int, int>(0, 0); 
                        case "L": return new Tuple<int, int>(0, 0); 
                        case "R": return new Tuple<int, int>(1, 0); 
                    }
                    break;
                case 7:
                    switch (dir)
                    {
                        case "U": return new Tuple<int, int>(-1,1); 
                        case "D": return new Tuple<int, int>(0, 0); 
                        case "L": return new Tuple<int, int>(-1,1); 
                        case "R": return new Tuple<int, int>(0, 0); 
                    }
                    break;
                case 8:
                    switch (dir)
                    {
                        case "U": return new Tuple<int, int>(0, 1); 
                        case "D": return new Tuple<int, int>(0, 0); 
                        case "L": return new Tuple<int, int>(0, 0); 
                        case "R": return new Tuple<int, int>(0, 0); 
                    }
                    break;
                case 9:
                    switch (dir)
                    {
                        case "U": return new Tuple<int, int>(1, 1); 
                        case "D": return new Tuple<int, int>(0, 0); 
                        case "L": return new Tuple<int, int>(0, 0); 
                        case "R": return new Tuple<int, int>(1, 1); 
                    }
                    break;
                default:
                    return new Tuple<int, int>(0, 0);
            }
            return new Tuple<int, int>(0, 0);
        }

        /// <summary>
        /// relative position of tail to head, using numpad as cardinal directions
        /// </summary>
        /// <param name="head"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        private int FindHeadRelativeToTail(Position head, Position tail)
        {
            if(head.x == tail.x)
            {
                if(head.y == tail.y)
                {
                    return 5;
                }
                if(head.y > tail.y)
                {
                    return 8;
                }
                if(head.y < tail.y)
                {
                    return 2;
                }
            }
            else if(head.y == tail.y)
            {
                if (head.x > tail.x)
                {
                    return 6;
                }
                if (head.x < tail.x)
                {
                    return 4;
                }
            }
            else if(head.x > tail.x)
            {
                if(head.y > tail.y)
                {
                    return 9;
                }
                if(head.y < tail.y)
                {
                    return 3;
                }
            }
            else if(head.x < tail.x)
            {
                if(head.y > tail.y)
                {
                    return 7;
                }
                if(head.y < tail.y)
                {
                    return 1;
                }
            }

            return 0;
        }

        private class Position
        {
            public int x = 0;
            public int y = 0;
            public int numTailVisits = 0;

            public override bool Equals(object obj)
            {
                Position other;
                other = (Position)obj;
                return (x == other.x && y == other.y);
            }

            public override int GetHashCode()
            {
                string positionalHash = x.ToString() + "|" + y.ToString();
                return positionalHash.GetHashCode();
            }
        }

        //sloppy meh
        private Dictionary<Position, int> grid = new();
        private static readonly string input = @"U 2
L 1
U 1
D 2
L 2
D 1
L 2
R 2
D 2
U 2
L 2
D 1
L 2
U 1
R 2
D 2
R 1
U 1
R 1
L 1
D 2
R 1
D 2
R 2
D 2
L 2
U 1
R 1
L 1
D 2
L 1
R 1
U 1
D 1
R 1
D 2
R 2
L 1
R 2
L 2
U 1
L 1
D 2
U 2
D 1
R 2
L 2
D 1
R 1
D 2
U 1
D 2
R 2
U 2
L 2
D 2
R 2
L 1
D 2
L 1
U 2
R 1
L 2
U 1
R 2
U 1
D 2
R 2
U 1
D 2
L 2
D 2
L 1
R 1
D 1
L 2
D 2
U 2
R 1
U 1
R 2
U 2
L 2
R 2
L 2
R 1
U 2
L 2
R 1
D 1
L 1
D 1
U 2
R 1
U 2
R 2
U 2
L 2
U 2
L 2
U 1
R 1
U 2
R 2
L 2
R 2
U 1
R 1
D 1
U 1
L 2
R 3
L 2
U 3
L 1
R 1
U 1
D 1
U 1
D 1
R 3
U 3
L 3
U 1
R 2
D 1
U 2
D 2
U 3
L 2
U 3
R 1
L 2
U 1
L 2
D 2
U 3
D 1
R 3
U 3
R 3
L 1
D 2
U 2
D 3
U 1
R 2
L 1
D 1
L 1
D 3
R 1
U 2
L 2
D 2
U 3
D 2
U 1
D 3
U 2
L 3
R 2
D 2
R 3
L 3
R 1
D 1
L 2
R 1
D 2
L 3
R 2
U 1
L 3
D 2
R 3
D 3
U 1
L 1
D 3
U 3
D 3
R 3
U 1
D 1
L 1
R 3
L 3
U 1
D 3
R 1
L 3
R 3
D 2
L 3
D 3
U 3
L 1
U 1
L 2
D 3
L 2
U 3
L 3
U 1
R 2
D 3
U 2
R 2
U 1
L 2
D 2
L 2
D 1
U 3
D 1
L 1
U 2
D 1
R 2
L 2
R 3
L 2
R 1
D 1
R 1
D 2
L 4
D 3
R 3
D 4
L 3
D 1
U 2
D 1
L 4
R 4
L 3
R 2
L 1
R 2
D 1
R 1
D 4
U 1
D 2
R 3
D 3
R 2
U 1
D 2
U 2
L 1
D 3
U 4
L 3
U 2
L 4
D 4
L 1
U 3
D 2
R 3
D 2
L 2
U 3
L 3
R 2
D 2
R 2
U 3
L 2
R 4
L 4
U 1
D 3
L 2
R 3
L 2
R 3
D 4
R 1
L 1
U 1
L 2
D 3
L 4
R 2
D 2
U 3
D 4
L 3
R 1
U 4
L 3
D 4
L 1
D 3
L 4
R 2
L 1
R 4
L 4
U 3
D 4
U 1
R 2
D 3
U 3
R 2
L 2
R 2
U 4
L 4
D 4
L 3
D 3
U 1
D 1
U 4
R 1
L 2
U 2
L 3
U 3
L 3
D 1
L 2
U 3
L 3
U 2
L 2
U 2
L 4
R 1
L 2
D 2
R 3
L 5
U 1
R 5
D 3
L 5
U 3
D 1
L 3
R 2
D 2
U 3
R 5
D 5
R 1
D 4
U 1
R 1
L 4
D 1
U 3
D 4
L 1
D 1
L 2
U 1
L 3
R 1
D 1
U 1
D 2
L 5
U 2
R 3
D 4
L 4
R 1
L 4
R 5
D 4
R 4
L 4
U 3
L 3
R 1
D 1
R 4
U 1
R 3
U 5
R 5
D 2
R 4
D 5
L 2
D 4
U 4
R 3
D 2
L 5
R 4
D 4
L 1
D 4
L 4
D 4
R 2
D 5
L 4
U 3
D 5
U 5
L 1
U 3
L 2
U 2
R 2
U 3
D 3
R 1
L 3
R 1
U 4
D 1
R 1
L 1
R 4
L 1
R 3
D 4
U 5
L 4
U 5
L 5
U 4
L 3
D 2
L 1
U 1
R 4
L 5
D 3
U 3
L 2
D 1
L 1
U 4
R 6
U 5
L 4
R 3
U 4
L 5
U 3
D 1
U 3
R 4
L 3
D 6
R 1
L 2
U 6
D 6
R 2
L 4
D 1
R 5
L 3
D 1
R 2
D 3
L 6
U 5
D 1
U 2
R 2
L 5
U 1
D 3
R 5
D 2
L 1
D 6
R 4
D 5
L 1
R 2
L 3
R 4
L 6
U 5
L 3
D 4
R 4
U 1
D 3
R 4
L 4
R 1
U 2
R 6
D 1
L 4
R 2
D 5
R 6
D 3
R 5
L 1
R 2
U 2
D 2
L 4
D 6
L 1
R 2
U 3
D 6
L 1
R 3
L 1
D 1
R 6
D 6
L 1
U 4
L 4
U 6
R 6
L 6
D 1
U 2
R 6
L 6
U 6
D 1
R 5
U 4
R 2
L 2
R 5
U 5
R 6
U 2
D 3
R 6
D 2
R 3
L 6
U 5
R 2
D 5
L 3
D 5
U 2
D 4
L 1
D 3
U 1
L 4
D 2
L 3
U 5
R 5
L 3
D 7
R 5
L 5
U 5
L 7
U 3
L 2
R 5
D 1
R 4
U 1
R 5
U 6
R 1
L 3
U 7
L 6
R 2
L 3
U 6
L 4
D 1
L 6
U 2
L 6
U 2
L 4
U 2
L 4
U 4
D 3
U 1
R 1
U 5
L 2
R 3
D 7
R 4
U 4
R 1
D 1
L 7
R 2
U 3
D 1
U 1
D 1
L 2
R 5
U 2
R 5
U 7
L 2
R 6
U 2
R 1
L 7
U 2
R 3
U 2
R 7
D 7
R 6
U 4
L 1
R 1
U 6
R 6
U 1
L 7
R 6
L 2
U 5
L 3
R 4
D 4
R 6
L 5
D 1
U 4
D 6
L 4
R 4
D 1
L 3
U 2
L 7
R 2
U 7
R 3
L 6
D 6
U 3
D 7
U 7
D 7
R 7
U 5
L 2
U 5
D 4
L 4
U 2
L 6
U 4
D 3
L 6
U 7
R 3
D 2
U 2
L 7
D 5
L 2
U 6
R 8
D 7
R 5
U 5
R 3
L 1
U 6
R 3
D 7
U 3
R 4
U 5
D 4
R 1
D 6
L 7
D 2
R 5
U 4
L 1
R 1
L 7
U 6
D 1
L 1
D 8
U 1
L 7
R 1
U 6
D 6
L 4
U 1
L 3
D 6
R 6
D 3
R 7
U 3
L 7
R 6
L 3
D 2
L 4
U 1
R 1
D 7
L 8
R 1
L 7
D 1
L 1
R 3
U 1
R 8
L 6
D 3
U 3
R 2
D 7
R 6
L 2
R 1
U 4
R 6
L 6
D 4
L 4
U 6
L 2
U 7
D 3
R 3
L 5
R 7
D 7
U 8
L 6
U 6
D 7
L 8
U 8
L 6
U 1
D 1
U 8
D 5
R 5
U 7
D 3
U 3
D 2
U 4
L 6
D 6
L 2
D 4
R 2
L 7
R 4
U 4
D 6
R 7
D 6
U 1
D 2
U 1
D 4
L 2
R 4
L 7
U 5
L 3
D 6
U 5
L 6
U 3
L 3
U 1
D 1
L 6
U 3
L 8
D 3
R 7
L 2
U 8
R 5
U 8
R 9
U 9
L 8
U 5
D 3
U 4
R 8
U 4
L 6
R 6
L 8
R 8
U 2
L 2
R 5
D 7
L 3
D 4
L 5
D 1
R 7
D 6
R 6
D 2
R 8
U 8
R 2
D 4
U 3
R 4
U 2
L 3
U 4
D 5
R 4
U 9
D 4
R 7
L 1
D 7
U 6
L 1
U 3
D 8
R 3
U 4
D 2
L 6
R 8
D 5
R 5
D 1
U 2
L 7
D 7
U 6
D 3
U 6
R 9
U 2
L 7
U 4
L 7
U 4
D 3
U 9
R 5
D 2
U 1
L 6
D 2
L 8
U 5
R 3
U 9
R 2
U 9
L 7
D 6
L 4
R 1
L 4
D 5
R 5
L 1
R 8
L 7
U 2
R 9
L 1
R 2
D 4
U 5
R 5
L 10
D 7
U 7
R 9
L 1
R 9
D 7
U 4
L 8
U 9
R 10
D 3
U 2
D 2
L 10
R 7
L 2
R 4
U 10
R 7
D 5
L 7
R 8
D 6
R 6
U 1
R 1
L 4
D 7
R 10
L 3
U 4
D 10
L 5
R 10
D 7
U 2
D 8
L 5
R 2
D 5
U 9
R 6
L 6
U 5
L 3
R 3
D 6
L 3
U 4
D 10
L 6
U 7
L 4
D 5
L 4
D 8
R 5
U 8
R 8
L 1
D 4
R 7
U 5
D 3
L 5
D 3
L 7
U 7
R 4
U 1
R 7
L 3
R 3
D 2
U 6
D 6
U 1
R 6
D 1
U 4
R 5
D 3
R 8
L 2
D 10
L 7
D 10
U 9
D 6
U 3
L 8
U 9
R 10
D 10
R 5
L 10
D 6
L 6
R 11
D 6
L 1
R 8
U 10
R 8
L 8
R 10
U 8
R 4
L 8
D 7
U 4
L 1
R 10
U 10
L 6
R 10
L 6
U 8
R 11
D 9
L 1
R 2
D 10
L 4
U 8
D 9
L 11
R 7
U 7
L 2
D 1
R 8
U 11
D 5
R 4
U 7
L 8
D 7
R 10
D 1
R 5
U 4
D 3
U 4
D 11
L 8
R 10
D 7
U 1
D 11
U 1
D 10
R 5
L 2
R 10
U 9
D 8
L 3
U 6
R 11
L 4
U 4
R 3
L 3
U 2
R 8
U 6
R 7
D 6
U 1
R 3
L 6
D 4
L 2
D 8
U 2
D 3
R 10
D 10
R 1
L 2
R 4
D 10
L 1
U 3
D 1
R 7
D 9
R 1
L 10
R 9
U 11
R 1
L 5
U 7
R 8
L 3
D 5
R 11
D 4
R 2
U 6
D 11
L 5
D 5
L 3
U 7
R 5
L 11
U 11
L 10
R 9
D 2
U 2
R 2
D 11
L 3
U 4
R 3
U 5
D 5
L 5
U 5
D 3
U 5
D 7
R 9
L 1
D 11
U 11
L 5
R 10
L 3
U 12
L 8
U 4
R 12
U 12
D 6
U 1
L 11
R 8
U 8
R 12
U 2
D 12
R 4
L 1
U 12
R 1
L 8
R 5
U 11
L 8
R 2
L 10
R 3
D 11
L 2
U 11
D 7
L 5
R 8
L 5
U 7
R 10
U 3
L 6
U 1
R 9
D 12
R 10
D 3
R 2
D 8
R 2
D 3
U 2
L 12
D 8
L 4
U 8
D 10
U 11
R 11
U 6
L 10
U 3
L 5
U 12
L 9
R 8
L 12
R 3
D 11
U 11
L 9
R 8
L 1
U 2
L 6
R 6
D 5
R 1
D 7
L 3
U 8
R 1
U 12
R 6
L 9
U 6
R 2
D 8
L 11
U 1
R 2
L 3
U 4
L 12
U 1
D 7
U 3
D 10
L 6
D 1
R 2
U 7
L 3
U 2
D 6
R 5
L 11
R 9
D 5
R 8
L 6
D 3
U 7
L 6
D 10
U 4
L 9
R 2
D 8
U 2
L 7
D 13
R 6
L 6
U 3
D 9
L 5
R 11
D 13
U 6
D 8
L 7
U 13
L 3
D 2
U 7
D 12
L 3
R 10
L 8
D 8
U 5
R 4
U 12
R 2
D 11
L 12
D 12
L 11
D 1
R 10
D 10
U 10
D 2
L 2
R 7
L 13
U 5
L 10
U 5
R 5
L 13
D 7
R 9
U 2
D 11
U 4
R 1
L 5
D 7
L 5
D 6
U 7
R 8
U 10
D 12
R 2
U 6
L 12
D 12
L 8
U 1
D 8
U 9
D 11
U 8
D 1
L 9
R 10
U 4
D 4
U 12
L 9
D 13
L 12
R 12
L 11
D 12
U 9
D 1
R 4
U 5
R 8
U 6
R 10
U 1
L 7
D 12
R 13
D 10
L 1
U 9
L 9
R 2
L 14
R 8
U 6
D 7
R 4
L 9
D 3
L 11
R 13
U 6
D 6
L 2
R 6
U 1
L 13
R 9
D 9
L 5
R 9
D 4
U 13
L 9
D 7
U 8
D 1
U 11
L 12
U 4
D 7
R 6
D 13
L 12
R 8
L 6
U 1
R 13
U 9
D 2
U 2
D 1
U 6
R 8
U 2
R 6
D 8
L 10
D 14
U 6
D 8
L 3
R 2
U 4
D 10
U 13
R 1
L 11
D 2
L 6
D 14
U 8
D 11
R 1
L 8
R 12
U 1
D 4
U 10
L 9
D 11
L 10
U 13
D 7
U 4
L 11
U 8
R 12
D 4
L 1
U 7
D 5
U 6
R 7
L 8
U 13
L 1
D 8
U 14
D 5
U 10
D 14
L 6
U 4
L 9
D 6
U 7
L 14
R 13
D 11
R 6
D 10
U 6
R 8
D 14
U 14
L 4
U 2
L 4
R 8
U 14
D 2
L 15
U 4
D 6
R 3
U 3
L 12
U 4
D 8
R 14
U 15
R 3
D 10
R 14
L 15
R 9
D 9
L 10
U 7
L 13
R 4
U 4
D 13
L 14
U 7
L 4
R 5
D 7
R 4
D 15
R 15
U 1
L 2
R 14
L 7
U 13
R 12
L 5
U 8
L 15
U 6
R 11
L 12
U 13
R 4
U 15
D 5
R 14
L 6
U 1
D 5
L 7
U 13
D 8
U 2
D 11
L 6
R 2
L 10
R 6
L 14
D 7
R 6
L 10
R 4
U 1
L 3
R 6
L 8
D 14
R 3
D 15
L 9
D 10
U 11
R 5
D 11
U 4
R 2
D 2
U 6
L 7
D 5
L 12
U 3
R 8
D 13
R 9
D 1
L 3
R 14
L 3
D 13
L 15
U 2
L 2
U 12
D 1
L 9
U 2
D 8
R 12
L 5
R 9
U 15
D 4
R 7
U 9
R 13
U 14
L 1
U 1
R 15
D 1
R 2
U 11
R 8
D 9
R 1
U 13
D 3
L 4
U 6
L 12
U 5
L 2
R 11
L 5
R 4
D 11
L 9
U 15
L 12
U 16
L 12
D 6
L 3
D 12
U 2
L 4
R 15
U 16
D 13
L 14
U 13
D 11
R 14
L 13
U 9
L 9
D 15
R 1
D 4
L 8
R 1
D 12
U 1
L 13
D 8
U 7
D 7
L 12
D 11
L 7
U 5
R 14
L 6
R 8
L 15
U 12
R 13
U 7
L 1
D 12
U 16
L 8
U 1
L 1
U 6
D 14
U 2
R 9
L 15
R 4
D 5
U 10
R 7
L 15
R 5
D 1
L 1
D 4
L 1
R 4
D 10
U 13
L 1
R 13
U 16
R 15
D 5
U 6
D 1
R 5
D 4
U 6
R 8
L 1
D 3
U 13
D 14
U 11
D 7
U 12
L 1
D 3
U 1
R 2
U 9
R 2
D 5
L 8
U 4
D 1
L 9
R 5
D 12
R 12
L 12
R 16
L 10
R 15
U 7
D 13
R 14
D 12
R 2
L 7
U 5
L 15
D 5
R 12
U 14
D 2
U 11
R 8
U 2
L 5
D 10
U 7
L 3
U 15
R 2
U 11
L 10
D 4
U 6
D 15
L 9
D 8
U 11
L 10
R 17
D 2
U 1
R 7
U 9
L 4
R 5
L 11
U 11
R 13
L 7
D 17
U 10
D 5
U 17
L 1
U 3
D 4
U 16
D 17
U 10
D 12
U 1
R 4
D 2
U 1
D 1
L 9
D 6
L 1
U 8
L 2
D 1
U 10
L 14
U 17
D 12
U 1
R 1
D 8
L 7
D 10
L 15
R 15
D 14
R 12
D 5
L 1
R 14
L 2
D 8
R 15
U 6
L 4
R 5
D 11
L 16
R 11
D 14
L 14
R 16
U 12
R 2
U 16
L 3
R 4
U 15
R 15
D 3
U 17
R 5
L 2
R 15
D 13
L 2
U 4
L 6
R 5
L 5
R 4
L 9
U 3
L 9
R 15
L 4
R 1
U 3
L 1
D 3
R 9
D 9
L 10
R 14
L 18
U 7
R 11
L 6
R 16
U 9
R 5
U 5
R 9
D 9
L 12
D 18
L 11
D 18
L 2
D 8
R 6
L 1
U 11
L 6
U 18
L 8
D 12
L 8
R 15
L 15
R 13
L 10
D 15
R 10
L 11
R 14
D 6
L 9
U 3
R 10
U 18
R 9
L 9
D 4
L 17
D 16
U 10
R 14
L 15
R 5
U 2
L 4
R 15
U 2
D 2
R 9
U 14
D 17
U 5
R 1
U 15
R 17
D 4
R 7
L 4
R 16
L 11
U 10
R 3
D 2
L 5
R 14
L 4
D 11
L 11
U 6
R 9
U 3
R 17
D 16
R 18
L 10
R 15
L 14
R 16
L 12
U 3
R 9
D 17
R 6
L 1
R 16
L 8
U 16
R 6
L 1
U 4
L 17
R 4
U 15
R 19
D 7
R 18
L 16
R 18
U 2
D 11
L 17
R 2
L 10
U 17
D 11
R 17
U 1
D 15
U 6
D 19
R 16
D 12
L 7
R 13
D 14
L 12
U 14
L 5
R 5
D 3
L 10
U 16
R 14
D 16
L 9
D 16
L 10
R 11
D 11
U 19
L 4
D 16
R 8
D 1
U 4
L 15
R 5
D 1
L 19
D 19
R 12
L 18
D 13
L 4
U 6
R 5
D 12
R 15
D 9
L 9
U 16
D 1
R 16
L 7
R 6
D 12
L 7
R 8
U 17
L 12
D 9
L 8
D 16
R 8
D 9
L 17
R 3
U 6
D 1
R 7
L 4
D 10
U 7
R 13
L 10
D 19
U 7
D 10
L 17
R 18
D 15
R 6
L 7
R 8
U 16
R 6
D 3
L 19
D 10
R 12
U 13
L 11";
    }
}
