using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class SupplyStacks
    {
        public SupplyStacks()
        {
            Console.WriteLine("\r\n\r\nSupply Stacks, GO!");
            Console.WriteLine("----------------------------------");

            var splitInput = input.Replace("\r", null).Split('\n');

            int rowIndex = 0;
            List<int> rowsWithCrates = new();
            int numStacks = 0;
            foreach(string s in splitInput)
            {
                if(s.StartsWith("move"))
                {
                    // It's a move instruction
                    MoveInstructions.Add(new MoveInstruction(s));
                }
                else
                {
                    if (s.Contains('['))
                    {
                        rowsWithCrates.Add(rowIndex);
                    }
                    else if(s.Length == 0)
                    {
                        //it's a blank line, skip
                    }
                    else
                    {
                        //it's the line indicating number of crate stacks
                        numStacks = int.Parse(s.Substring(s.Length - 2, 1));
                    }
                }
                rowIndex++;
            }

            //Create our stack dictionary
            for(int i=1;i<=numStacks;i++) //cols are 1 thru X, not 0 based
            {
                CrateStacks.Add(i, new Stack<string>());
                CrateStacks2.Add(i, new Stack<string>());
            }

            //Now, we walk backwards through the rows with crates to put them in the col stacks
            //crates are in a static index in the string.
            /* col   string index
                1       1
                2       5
                3       9
                +1      +4
            */
            //TODO: won't handle the case where there are no crates in the last stack...
            //Wait, maybe it will
            for (int i=rowsWithCrates.Count - 1;i>=0;i--)
            {
                var row = splitInput[i];
                int col = 1;
                for(int j=1;j<row.Length;j+=4)
                {
                    var crate = row[j].ToString(); //screw you char
                    //This is either going to be a letter or a space
                    if(!crate.Equals(" "))
                    {
                        CrateStacks[col].Push(crate);
                        CrateStacks2[col].Push(crate);

                    }
                    col++; //always increment in case of a gap in crate cols
                }
            }

            Console.WriteLine("Crates loaded captain!");
        }

        public void MoveStacks_CrateMover9001()
        {
            //make copy so we can do both sims.
            foreach (var moveInstruction in MoveInstructions)
            {
                MoveMultiple(CrateStacks2[moveInstruction.FromStack], CrateStacks2[moveInstruction.ToStack], moveInstruction.NumberOfCrates);
            }


            Console.WriteLine("stacks shuffled! results:");
            foreach (var stack in CrateStacks2)
            {
                Console.Write(stack.Value.Peek());
            }

            Console.WriteLine();
        }

        public void MoveStacks()
        {
            foreach(var moveInstruction in MoveInstructions)
            {
                for(int i=0;i<moveInstruction.NumberOfCrates;i++)
                {
                    Move(CrateStacks[moveInstruction.FromStack], CrateStacks[moveInstruction.ToStack]);
                }
            }

            Console.WriteLine("stacks shuffled! results:");
            foreach(var stack in CrateStacks)
            {
                Console.Write(stack.Value.Peek());
            }

            Console.WriteLine();
        }


        void PrintStack(Stack<string> stack)
        {
            foreach(var a in stack)
            {
                Console.Write(a + " ");
            }
            Console.Write("\r\n");
        }

        private static void Move(Stack<string> From, Stack<string> To)
        {
            To.Push(From.Pop());
        }

        private static void MoveMultiple(Stack<string> From, Stack<string> To, int count)
        {
            Stack<string> buffer = new Stack<string>();
            for(int i=0;i<count;i++)
            {
                buffer.Push(From.Pop());
            }
            for(int i=0;i<count; i++)
            {
                To.Push(buffer.Pop());
            }
        }

        private class MoveInstruction
        {
            public MoveInstruction(string s)
            {
                //  0  1   2  3 4  5
                //move 2 from 2 to 8
                var instruction = s.Split(" ");
                NumberOfCrates = int.Parse(instruction[1]);
                FromStack = int.Parse(instruction[3]);
                ToStack = int.Parse(instruction[5]);
            }

            public int NumberOfCrates;
            public int FromStack;
            public int ToStack;
        }

        List<MoveInstruction> MoveInstructions = new();
        private Dictionary<int, Stack<string>> CrateStacks = new();
        private Dictionary<int, Stack<string>> CrateStacks2 = new();


        private static readonly string input = @"                        [R] [J] [W]
            [R] [N]     [T] [T] [C]
[R]         [P] [G]     [J] [P] [T]
[Q]     [C] [M] [V]     [F] [F] [H]
[G] [P] [M] [S] [Z]     [Z] [C] [Q]
[P] [C] [P] [Q] [J] [J] [P] [H] [Z]
[C] [T] [H] [T] [H] [P] [G] [L] [V]
[F] [W] [B] [L] [P] [D] [L] [N] [G]
 1   2   3   4   5   6   7   8   9 

move 2 from 2 to 8
move 2 from 1 to 6
move 8 from 7 to 1
move 7 from 5 to 4
move 1 from 6 to 4
move 1 from 6 to 3
move 6 from 3 to 5
move 9 from 8 to 1
move 3 from 6 to 7
move 14 from 4 to 1
move 6 from 1 to 7
move 16 from 1 to 9
move 6 from 1 to 4
move 1 from 8 to 6
move 4 from 1 to 5
move 11 from 9 to 7
move 2 from 1 to 8
move 1 from 6 to 7
move 1 from 8 to 7
move 1 from 8 to 3
move 7 from 4 to 3
move 14 from 7 to 6
move 8 from 6 to 9
move 19 from 9 to 2
move 1 from 1 to 2
move 2 from 9 to 7
move 9 from 7 to 8
move 2 from 2 to 8
move 16 from 2 to 9
move 4 from 8 to 2
move 1 from 7 to 9
move 3 from 9 to 6
move 3 from 3 to 6
move 11 from 9 to 2
move 7 from 5 to 3
move 2 from 5 to 9
move 6 from 6 to 4
move 1 from 6 to 4
move 4 from 6 to 8
move 5 from 9 to 1
move 4 from 1 to 7
move 3 from 2 to 6
move 3 from 4 to 1
move 1 from 4 to 1
move 2 from 1 to 3
move 4 from 3 to 7
move 1 from 5 to 2
move 3 from 1 to 6
move 15 from 2 to 5
move 3 from 6 to 3
move 13 from 3 to 8
move 2 from 4 to 2
move 9 from 5 to 4
move 2 from 2 to 5
move 5 from 7 to 5
move 10 from 8 to 6
move 1 from 2 to 5
move 10 from 4 to 6
move 4 from 8 to 6
move 3 from 7 to 1
move 3 from 1 to 9
move 1 from 2 to 1
move 8 from 5 to 2
move 3 from 6 to 9
move 6 from 8 to 5
move 6 from 9 to 2
move 1 from 1 to 9
move 10 from 2 to 1
move 4 from 8 to 5
move 10 from 5 to 9
move 11 from 9 to 7
move 5 from 7 to 9
move 1 from 9 to 2
move 3 from 2 to 9
move 2 from 2 to 8
move 4 from 9 to 5
move 4 from 1 to 9
move 5 from 5 to 2
move 5 from 1 to 4
move 21 from 6 to 9
move 3 from 2 to 9
move 2 from 8 to 1
move 25 from 9 to 6
move 4 from 5 to 7
move 1 from 4 to 6
move 6 from 6 to 4
move 3 from 4 to 6
move 7 from 7 to 3
move 4 from 9 to 1
move 3 from 7 to 8
move 2 from 9 to 8
move 2 from 2 to 8
move 4 from 1 to 3
move 9 from 6 to 2
move 13 from 6 to 4
move 13 from 4 to 5
move 1 from 5 to 8
move 2 from 2 to 3
move 6 from 5 to 3
move 19 from 3 to 6
move 1 from 4 to 9
move 2 from 8 to 1
move 5 from 2 to 3
move 5 from 1 to 9
move 7 from 5 to 4
move 1 from 8 to 3
move 1 from 2 to 6
move 8 from 6 to 3
move 1 from 9 to 8
move 11 from 4 to 2
move 1 from 4 to 6
move 1 from 2 to 8
move 5 from 3 to 4
move 4 from 9 to 6
move 1 from 6 to 8
move 9 from 3 to 1
move 7 from 2 to 9
move 1 from 2 to 6
move 3 from 1 to 8
move 2 from 2 to 3
move 3 from 9 to 7
move 3 from 4 to 7
move 2 from 4 to 3
move 2 from 3 to 5
move 8 from 6 to 4
move 6 from 8 to 6
move 2 from 9 to 4
move 5 from 8 to 6
move 3 from 7 to 5
move 1 from 5 to 8
move 1 from 8 to 2
move 1 from 5 to 1
move 11 from 4 to 9
move 2 from 6 to 3
move 2 from 2 to 4
move 6 from 1 to 2
move 6 from 2 to 1
move 3 from 7 to 3
move 2 from 4 to 7
move 4 from 6 to 5
move 7 from 3 to 7
move 5 from 9 to 6
move 22 from 6 to 8
move 2 from 6 to 5
move 2 from 8 to 4
move 14 from 8 to 7
move 11 from 7 to 4
move 3 from 8 to 1
move 9 from 7 to 8
move 10 from 1 to 4
move 1 from 7 to 4
move 4 from 8 to 7
move 6 from 4 to 9
move 7 from 4 to 1
move 3 from 4 to 8
move 1 from 5 to 8
move 8 from 5 to 3
move 4 from 3 to 9
move 7 from 8 to 9
move 3 from 8 to 3
move 2 from 8 to 2
move 7 from 9 to 1
move 2 from 2 to 8
move 8 from 9 to 1
move 8 from 1 to 7
move 7 from 1 to 5
move 7 from 7 to 1
move 11 from 9 to 8
move 9 from 8 to 5
move 2 from 8 to 5
move 3 from 1 to 8
move 2 from 3 to 7
move 6 from 4 to 1
move 6 from 1 to 6
move 5 from 7 to 1
move 2 from 4 to 6
move 1 from 3 to 5
move 4 from 7 to 4
move 2 from 8 to 7
move 10 from 5 to 6
move 9 from 6 to 1
move 8 from 1 to 6
move 1 from 7 to 2
move 9 from 6 to 4
move 2 from 4 to 3
move 3 from 8 to 1
move 1 from 2 to 4
move 4 from 4 to 1
move 7 from 4 to 3
move 3 from 3 to 2
move 1 from 7 to 6
move 9 from 6 to 7
move 6 from 7 to 4
move 2 from 7 to 2
move 6 from 4 to 7
move 2 from 2 to 9
move 1 from 2 to 4
move 1 from 7 to 4
move 4 from 7 to 6
move 4 from 5 to 4
move 1 from 2 to 5
move 1 from 7 to 5
move 1 from 2 to 6
move 6 from 4 to 3
move 9 from 3 to 9
move 4 from 6 to 2
move 7 from 3 to 8
move 22 from 1 to 7
move 1 from 1 to 7
move 2 from 8 to 3
move 4 from 5 to 6
move 2 from 3 to 2
move 6 from 2 to 8
move 3 from 8 to 6
move 1 from 4 to 8
move 1 from 1 to 8
move 8 from 6 to 7
move 7 from 8 to 9
move 22 from 7 to 4
move 3 from 5 to 6
move 1 from 8 to 1
move 2 from 8 to 2
move 3 from 6 to 4
move 1 from 1 to 3
move 15 from 9 to 1
move 5 from 1 to 5
move 3 from 7 to 6
move 5 from 5 to 6
move 4 from 4 to 3
move 6 from 6 to 9
move 7 from 7 to 6
move 5 from 6 to 7
move 4 from 1 to 9
move 3 from 7 to 4
move 2 from 9 to 7
move 5 from 3 to 5
move 3 from 6 to 3
move 5 from 4 to 6
move 10 from 9 to 5
move 1 from 2 to 9
move 1 from 3 to 5
move 1 from 2 to 9
move 3 from 1 to 6
move 2 from 9 to 2
move 7 from 6 to 5
move 15 from 4 to 9
move 2 from 4 to 5
move 1 from 3 to 4
move 9 from 9 to 1
move 1 from 9 to 2
move 2 from 9 to 4
move 11 from 5 to 4
move 1 from 9 to 3
move 1 from 6 to 8
move 4 from 7 to 8
move 4 from 8 to 9
move 15 from 4 to 7
move 1 from 6 to 7
move 1 from 3 to 7
move 6 from 9 to 6
move 1 from 3 to 7
move 1 from 2 to 1
move 1 from 9 to 5
move 3 from 6 to 1
move 11 from 1 to 4
move 6 from 5 to 1
move 2 from 2 to 5
move 1 from 5 to 7
move 2 from 6 to 1
move 7 from 5 to 7
move 3 from 5 to 6
move 4 from 6 to 1
move 11 from 4 to 3
move 1 from 8 to 5
move 23 from 7 to 6
move 18 from 6 to 9
move 1 from 5 to 9
move 1 from 4 to 2
move 3 from 3 to 7
move 3 from 3 to 8
move 17 from 1 to 8
move 5 from 6 to 5
move 2 from 7 to 1
move 20 from 8 to 2
move 4 from 7 to 2
move 3 from 9 to 5
move 7 from 9 to 7
move 6 from 9 to 2
move 1 from 1 to 8
move 3 from 9 to 4
move 7 from 5 to 2
move 6 from 7 to 1
move 1 from 1 to 8
move 3 from 2 to 6
move 1 from 7 to 6
move 2 from 8 to 9
move 35 from 2 to 4
move 3 from 3 to 2
move 1 from 5 to 7
move 2 from 3 to 9
move 3 from 1 to 6
move 2 from 2 to 1
move 32 from 4 to 7
move 3 from 4 to 8
move 3 from 9 to 5
move 1 from 1 to 2
move 21 from 7 to 5
move 2 from 2 to 1
move 3 from 1 to 2
move 15 from 5 to 1
move 3 from 6 to 7
move 3 from 4 to 6
move 3 from 8 to 5
move 1 from 9 to 3
move 8 from 7 to 2
move 6 from 5 to 2
move 9 from 1 to 6
move 4 from 7 to 1
move 2 from 5 to 4
move 2 from 4 to 3
move 3 from 5 to 4
move 17 from 2 to 7
move 3 from 3 to 5
move 2 from 4 to 8
move 1 from 4 to 3
move 5 from 7 to 9
move 1 from 3 to 6
move 4 from 1 to 7
move 4 from 6 to 7
move 2 from 5 to 2
move 1 from 1 to 3
move 10 from 6 to 4
move 1 from 3 to 7
move 20 from 7 to 8
move 8 from 4 to 8
move 1 from 2 to 8
move 4 from 9 to 1
move 3 from 7 to 4
move 2 from 4 to 9
move 2 from 6 to 3
move 1 from 2 to 8
move 1 from 7 to 6
move 1 from 9 to 5
move 3 from 5 to 9
move 4 from 9 to 2
move 1 from 4 to 5
move 1 from 5 to 3
move 3 from 2 to 4
move 1 from 9 to 7
move 1 from 2 to 1
move 1 from 7 to 1
move 11 from 1 to 2
move 3 from 1 to 7
move 25 from 8 to 5
move 1 from 6 to 3
move 1 from 6 to 2
move 7 from 8 to 2
move 9 from 2 to 8
move 2 from 4 to 7
move 2 from 5 to 7
move 2 from 5 to 2
move 5 from 5 to 1
move 7 from 5 to 1
move 2 from 4 to 9
move 3 from 5 to 6
move 1 from 1 to 8
move 1 from 5 to 6
move 1 from 4 to 7
move 1 from 9 to 2
move 3 from 5 to 2
move 2 from 6 to 9
move 3 from 9 to 8
move 1 from 5 to 4
move 3 from 3 to 9
move 10 from 1 to 5
move 4 from 2 to 8
move 2 from 6 to 1
move 3 from 9 to 7
move 1 from 1 to 9
move 1 from 4 to 3
move 1 from 9 to 2
move 9 from 8 to 2
move 2 from 3 to 7
move 2 from 7 to 6
move 3 from 5 to 6
move 4 from 8 to 6
move 4 from 8 to 3
move 4 from 3 to 2
move 4 from 6 to 8
move 1 from 7 to 9
move 2 from 1 to 8
move 2 from 8 to 3
move 1 from 9 to 2
move 13 from 2 to 4
move 6 from 5 to 7
move 2 from 5 to 7
move 10 from 2 to 4
move 11 from 7 to 8
move 1 from 6 to 4
move 4 from 6 to 7
move 24 from 4 to 9
move 11 from 7 to 4
move 1 from 3 to 8
move 1 from 3 to 5
move 4 from 4 to 2
move 5 from 4 to 2
move 9 from 2 to 5
move 4 from 9 to 5
move 1 from 5 to 1
move 2 from 5 to 7
move 2 from 2 to 5
move 1 from 1 to 7
move 2 from 2 to 3
move 18 from 9 to 6
move 9 from 8 to 1
move 2 from 9 to 5
move 5 from 1 to 8
move 2 from 8 to 7
move 4 from 8 to 4
move 5 from 8 to 7
move 10 from 5 to 1
move 10 from 7 to 4
move 4 from 5 to 8
move 14 from 1 to 9
move 6 from 9 to 8
move 1 from 5 to 1
move 12 from 6 to 9
move 4 from 6 to 8
move 11 from 8 to 5
move 1 from 6 to 1
move 19 from 9 to 7
move 2 from 3 to 5
move 13 from 7 to 5
move 3 from 7 to 1
move 4 from 8 to 9
move 2 from 7 to 6
move 7 from 4 to 8
move 5 from 8 to 1
move 1 from 1 to 3
move 1 from 7 to 2
move 6 from 1 to 6
move 1 from 2 to 5
move 1 from 8 to 1
move 1 from 8 to 2
move 2 from 4 to 8
move 5 from 6 to 1
move 2 from 4 to 7
move 2 from 9 to 6
move 1 from 6 to 5
move 4 from 6 to 2
move 1 from 9 to 5
move 2 from 4 to 5
move 4 from 2 to 4
move 2 from 8 to 3
move 3 from 3 to 2
move 4 from 1 to 2
move 2 from 4 to 7
move 4 from 2 to 3
move 4 from 1 to 2
move 13 from 5 to 1
move 1 from 6 to 2
move 1 from 1 to 8
move 15 from 5 to 2
move 4 from 3 to 1
move 5 from 4 to 3
move 1 from 3 to 6
move 1 from 8 to 7
move 1 from 9 to 8
move 1 from 7 to 8
move 3 from 3 to 2
move 1 from 8 to 2
move 1 from 3 to 7
move 13 from 1 to 4
move 3 from 5 to 3
move 1 from 1 to 2
move 1 from 8 to 5
move 5 from 7 to 2
move 1 from 6 to 5
move 2 from 3 to 4
move 10 from 2 to 5
move 1 from 9 to 5
move 3 from 1 to 8
move 3 from 8 to 3
move 11 from 4 to 5
move 12 from 2 to 8
move 4 from 4 to 7
move 10 from 8 to 5
move 2 from 8 to 1
move 1 from 7 to 3
move 1 from 7 to 9
move 5 from 3 to 7
move 1 from 9 to 4
move 7 from 7 to 6
move 13 from 5 to 8
move 6 from 6 to 7
move 5 from 7 to 4
move 1 from 6 to 4
move 2 from 4 to 9
move 1 from 7 to 9
move 3 from 4 to 3
move 1 from 3 to 6
move 4 from 5 to 7";
    }
}
