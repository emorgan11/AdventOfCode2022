using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class RockPaperScissors
    {
        public RockPaperScissors()
        {
            Console.WriteLine("\r\n\r\nRock Paper Scissors, GO!");
            Console.WriteLine("----------------------------------");
            Battles = strategyGuide.Replace("\r", null).Split('\n');
        }

        public void FindTotalScore()
        {
            //var battles = strategyGuide.Replace("\r", null).Split('\n');
            
            int totalScore = 0;
            foreach(string s in Battles)
            {
                var result = new BattleResults(s);
                totalScore += result.GetScore();
            }

            Console.WriteLine("Total score of the games: {0}", totalScore);
        }

        private readonly string[] Battles;

        public enum Choice
        {
            Rock = 1,
            Paper = 2,
            Scissors = 3
        };

        private class BattleResults
        {
            public BattleResults(string battleRow)
            {
                var battle = battleRow.Split(' ');
                OpponentChoice = OpponentChoiceMap[battle[0]];
                Result = PlayerChoiceMap[battle[1]];

                if(OpponentChoice == Choice.Rock)
                {
                    switch(Result)
                    {
                        case GameOutcome.PlayerWin:
                            PlayerChoice = Choice.Paper;
                            break;
                        case GameOutcome.PlayerLose:
                            PlayerChoice = Choice.Scissors;
                            break;
                        default:
                            PlayerChoice = Choice.Rock;
                            break;
                    }
                }
                else if (OpponentChoice == Choice.Paper)
                {
                    switch (Result)
                    {
                        case GameOutcome.PlayerWin:
                            PlayerChoice = Choice.Scissors;
                            break;
                        case GameOutcome.PlayerLose:
                            PlayerChoice = Choice.Rock;
                            break;
                        default:
                            PlayerChoice = Choice.Paper;
                            break;
                    }
                }
                else if (OpponentChoice == Choice.Scissors)
                {
                    switch (Result)
                    {
                        case GameOutcome.PlayerWin:
                            PlayerChoice = Choice.Rock;
                            break;
                        case GameOutcome.PlayerLose:
                            PlayerChoice = Choice.Paper;
                            break;
                        default:
                            PlayerChoice = Choice.Scissors;
                            break;
                    }
                }

            }

            private static readonly Dictionary<string, Choice> OpponentChoiceMap = new()
            {
                { "A", Choice.Rock },
                { "B", Choice.Paper },
                { "C", Choice.Scissors }
            };

            private static readonly Dictionary<string, GameOutcome> PlayerChoiceMap = new()
            {
                { "X", GameOutcome.PlayerLose },
                { "Y", GameOutcome.Tie },
                { "Z", GameOutcome.PlayerWin }
            };

            private enum GameOutcome
            {
                PlayerWin,
                PlayerLose,
                Tie
            }

            public Choice OpponentChoice;
            public Choice PlayerChoice;

            private GameOutcome Result;

            public int GetScore()
            {
                int score = 0;
                score += (int)PlayerChoice;

                switch(Result)
                {
                    case GameOutcome.PlayerWin:
                        score += 6;
                        break;
                    case GameOutcome.Tie:
                        score += 3;
                        break;
                    default:
                        break;
                }

                return score;
            }
        };

        private static readonly string strategyGuide = @"B Z
B Z
C Z
C Z
B X
C Y
A Y
B Z
A Z
A Y
C Z
C X
C Z
C X
B X
B Z
B Z
A Z
C Z
C Z
C Y
C Z
B Z
B Z
A X
A Y
B Z
B Z
C Z
B Z
B Z
C X
C Z
A Y
A X
A X
C Z
C Z
B X
C X
B Z
A Z
B X
B Z
B Z
A Z
B Z
A Y
C Z
C Z
B X
C Z
A Z
C Z
C Z
C Z
C Z
A Z
B Z
C Z
A Z
C Z
C Z
C Y
A Y
C X
C Z
C Z
C Z
C Z
A Y
C X
A Y
A Y
C X
B Z
A Y
C Y
C Z
B Z
C Z
B Z
B Z
B Z
C Y
B Z
A Y
C Z
C Z
A X
C X
C Z
A X
C Z
C Z
C X
A Z
C Z
C Z
B Z
C Y
A Y
C Y
C Y
A Z
B Z
C Z
C Z
C Z
A Z
C Z
B X
C Z
A Y
C Z
C X
C Y
C Y
B Z
C Z
C Z
A X
C X
C Z
C Z
A Z
B Z
B Z
C X
C Z
B Z
C X
B Z
C Z
B Z
B Z
B Z
B Z
C Y
C Z
B Z
C Z
C Z
C Z
C Z
B Z
C Z
B Z
C Z
B Z
C Z
C Y
C Z
C Z
C X
C Z
C Z
C Y
C Z
C Y
C X
B Z
B Z
C Z
A Z
C Z
C Z
C Z
B Z
C Z
B Z
C Z
B Z
C Z
C Z
B X
B Z
C Z
C Z
A Z
A Z
B Z
C Z
C Z
A Z
B X
C X
A Z
C Z
C Z
B Y
B Z
B Z
A Y
B Z
C Z
C Z
A Y
B Z
C Y
A Z
C X
A Z
A Y
C X
C Y
C Z
B Z
C Z
A Y
B Z
B Z
C Z
C Z
C Z
C Z
C Z
B Z
C Z
A X
C Z
C Z
A Y
C Z
A Y
C Z
B Z
C Z
A Y
C Z
C Y
C Z
B Z
C X
A Y
C X
A Y
B X
C Y
B X
C Z
C Y
C Z
C Y
C Y
B Z
C Z
A Y
C X
C Z
C Z
B Z
A Z
C Z
C Z
C Y
C Z
C Y
B X
C Z
C Z
C Z
C X
A Y
B Z
C Z
C Z
C Z
B Z
A Y
A Z
A Y
B Z
A Z
A Y
B Z
C Z
A Y
A Z
B Z
B Z
B Z
C Z
C Z
C Z
C Z
C Z
C Z
B Z
C Z
C Z
C Z
A Z
A Y
C X
C Z
A Z
C Z
A Y
C Z
A Y
C Z
C Z
A X
C Y
C Z
A X
C X
A X
B Z
C Z
A X
C Z
B Z
A X
C Z
A Y
A Z
C Y
A Y
C Y
A Z
B Z
A Z
B Z
C X
C X
C Z
C Z
B Z
A Y
C Z
A Y
C Z
B Z
B X
B X
B X
A Z
B Z
C X
C Z
A Y
C Z
B X
A Y
C X
B Z
C Z
C Z
C Z
C Z
A Z
B Z
B Z
B X
A Y
C Z
C X
A Y
C Z
C Z
C Z
A Y
A X
B Z
C Z
A Z
C Y
A Z
B Z
A Y
C Z
C Z
B Z
A Y
B Z
A Z
A Y
C Z
A X
C Z
C Z
B Z
C Z
C X
A Y
A Y
B Z
C Z
B X
A Y
C Z
C Z
A X
A Z
C Z
C Z
A Y
C Z
C Y
A Y
A Y
B Z
C Z
C Z
C X
C Z
A Y
B Z
C Z
C Z
A Y
B Z
C Z
C Z
B Z
A Y
B Z
C Z
C Z
C Z
C X
C Z
C Z
C Z
C Y
C X
C Y
C Z
B Z
A Y
C X
B Z
C X
C X
C Z
A Z
C Z
B X
A Y
C Z
B Z
A X
B X
C X
C Z
C Z
B Z
B Z
C Z
C Z
A Y
B Z
A Z
B Z
A Z
A X
C Z
A Z
A Z
B X
A Z
B Z
B Z
A Y
C Y
B Z
B Z
B Z
B Z
C Z
B Z
C Z
B Z
C Y
C Z
C Z
C X
C Z
C Y
C Y
C X
A Y
C Y
B Z
C Z
A Y
A Z
B X
C Z
C Z
A Y
A Y
A Y
C Z
A Z
C Z
B Y
A X
A Y
C Z
A X
B Z
C Z
C Z
B Z
C Z
A Z
C X
C Z
B Z
B Z
C Z
B Z
A Z
C Z
C Z
A Y
B Z
A Y
B Z
C Z
B Z
C Z
B Z
B Z
A Y
C Y
B Z
C Z
A Z
B Z
C Z
C Z
A Z
B Z
C Z
A Y
A Z
C X
B Z
A Y
A Y
C Z
A Z
B Z
B Z
C Z
C Z
C Z
C Z
A Y
A X
C Z
B Z
C Z
C Z
C Y
C Z
A X
A Z
C Z
B X
A Z
B X
B X
A Y
A Y
A Y
C Z
C Z
C Z
A Z
C X
A Y
C Z
B Z
C Z
A X
B Z
A X
A Z
C Z
C Z
C Z
B X
B Z
C X
B Z
C Z
C Z
C Z
A Y
B Z
B Z
C Z
A X
B Z
A Y
C Z
A Y
A Y
C Z
C Z
A Y
C X
C Z
A Y
C Z
B Z
B Z
C Z
C Y
B Z
B Z
C Z
A Y
C Z
B Z
B X
A Y
A Z
C Z
B Z
C Z
C X
A Y
A Z
A Y
B Z
A Z
B Z
A Y
A Y
B Z
C Z
C Z
C Z
A Y
A X
B Z
A Y
C Z
B X
C Z
C Z
A Y
C Z
C Z
C Y
C Z
C Y
B Z
B Z
C Z
C Z
B Z
B Z
B Z
C Z
B Z
B Z
C Z
C Z
C X
C Z
C Z
B Z
B Z
C X
A Y
C Y
C X
B Z
C X
B Z
C Z
A Z
C Y
C Z
C X
B X
A Y
C Y
A X
C Z
C Z
C Z
C Z
A Y
C Y
C Z
A Y
C Z
C Z
B Z
B Z
B Z
C Z
C Z
C Z
C Z
C Z
C Z
B Z
C Z
B Z
C Z
B Z
C Z
C Z
B Z
C Z
C Z
C Z
A Y
C Z
C Z
A X
C Z
C Z
A X
B Z
C Z
A Y
C X
C Z
A Z
C Y
A Y
C Z
B Z
C Z
B Z
C X
C Y
B Z
C Z
C Z
A Y
C X
C Z
C Z
B X
C Z
C Z
C Z
C Z
A Z
A Y
C Z
B Z
C Z
B Z
A Y
B Z
C Z
B Z
B Y
C Z
B Z
A Y
C Z
C Y
C Z
A X
B Z
C Z
A Z
A Z
B Z
A X
B Z
B Z
C Y
C Z
C Z
B Y
B Z
C Z
A Y
B X
B Z
C Z
C Z
C Z
B X
A Y
A Y
B Z
B Z
C Z
B Z
C Z
C Y
B Z
B Z
B X
A Y
A Y
A Y
B X
C Z
C Z
B Z
C Z
C X
B Z
C Z
C Z
B Z
C Z
C Z
A Y
B Z
A Y
C Y
B Z
B Z
B Z
C X
C Z
C Z
C Z
C X
C Z
A X
B Z
C Z
C Z
A Y
B Z
C Z
A Y
C Z
B Z
C Z
C Z
B Z
B Z
C Z
C Z
C Y
C Z
C Y
C X
C Z
C Y
C Z
A Y
B Z
B Z
A Y
B Z
B X
B X
A Z
C Y
C Z
C Z
B Z
C Z
C Z
C Z
C Z
C Y
C Z
B Z
C Z
C Z
C Z
B Z
B Z
C Z
B X
C Y
A X
C Z
A Z
C Y
C Z
C Z
B Z
C Z
C Z
C Z
B Z
A Y
A Z
B X
C X
A Y
A Y
C Z
C Z
C Z
C Z
C Z
C Z
C Y
C Z
C Z
C Z
A Y
A Z
C Z
A Y
C Z
C Z
B Z
B Z
B X
A Y
B Z
C Y
C Z
B Z
A Z
A Y
A X
C Z
B Z
C Z
B Z
B Z
C Z
C Z
C Z
C Z
A Y
A Z
C Z
C Z
A Y
C Y
C Z
A X
B X
B X
C Z
C Z
B Z
A Y
A Y
A Y
A X
C Z
B Z
B Z
B Z
A Y
C Y
B Z
A Z
C Z
B X
C Z
C Z
C X
C X
C Z
A Y
B Z
C Z
A Y
C Z
A X
C Z
B Z
B Z
B Z
C Z
B Z
C Z
A Z
C Z
B Z
C Z
A Y
C Z
A Z
C Z
C Y
B Z
A Z
C Z
B Y
A Z
C Z
A X
C Z
B Z
A X
C Z
B Z
C Z
C X
A Y
C Z
B Z
A X
C Z
B Z
C Z
A Y
C Z
C Z
C Z
B Z
C Y
C Z
C X
A Z
C Z
C Z
B Z
C Z
C Z
C Z
A Z
C Z
A Y
A X
C Z
A Y
C X
C Z
B Z
C Z
B Z
B Z
A Y
B Z
A Y
A Y
C X
B Z
C Z
C Z
C Z
C Z
C Z
A Y
B Z
C Z
A Y
B Z
C Z
C Z
A Y
B Z
C Z
C Y
B Z
C Z
B Z
A Z
C X
B Z
C Y
C Z
C Z
C X
C X
A X
C Z
B Z
A Z
C Z
B Z
B X
C Z
C Z
C Z
C Z
B Z
B X
C Z
C Z
C Z
B Z
C Y
B Z
B X
C Y
C Z
C Z
B Z
A Z
C Y
C X
C Z
B Z
C Z
C Z
C Z
B Z
B X
C Z
C Z
A Y
C Z
C Y
A Y
A Z
B Y
C Z
A Y
C X
C X
C X
C Z
B Z
C Z
C Z
B Z
C Z
A Y
C Z
C Z
B Z
C X
C Z
B Z
C Z
A Y
C Z
C Z
C X
C Z
B Z
C Y
C Z
A Z
C Z
B Z
A Z
B Z
C Z
A Y
B Z
C Z
C Z
B Z
A Z
C Z
C X
C Z
C Z
A Y
A Y
A Z
C Z
A Y
A X
C Z
C Z
C Z
C X
A Z
C Z
A Y
C Z
C Z
C Y
C Z
C Z
B Z
C Z
C Z
B Z
B Z
C Z
C Z
C Z
B Z
C Z
C Z
C Y
B Z
C Y
B Z
B X
C Z
C Z
A Y
B Z
C Y
B Z
B Z
B Z
B Z
C Z
C Y
A X
C Z
C Z
C Z
C Z
C Z
B Z
C Z
A Y
C Z
C Z
A Y
A Y
C Z
C Z
B Z
B Z
C X
C Z
C Z
A X
C Z
C Y
C Z
B Z
C Z
A Y
C Z
C Z
C Z
C X
A Y
C Z
C X
B Z
C Z
C Z
C Y
C Z
A Y
B Z
B Z
A X
C Y
C Z
B X
A X
C Z
C Z
C Z
C Z
C Y
A X
B Z
C Z
C Z
A Y
B X
A Z
A Y
B Z
C Z
C X
A X
C X
C Y
C Z
B X
A X
C Z
C Z
B X
A Y
A Y
A Y
C Z
C Z
A Y
C Z
A Z
A Y
C Z
B X
C Z
C Z
C X
C Z
C Z
A Z
B Z
A Z
C Y
C Y
C Y
C Z
C X
C Z
A Y
B Z
C Z
B Z
C Z
A Y
C X
C Z
C Z
B Z
C Z
C Y
A Y
B Z
B Z
B X
B Z
C Z
A Y
C Z
B Z
A Y
C Z
C Z
B Z
A Y
C Z
C Z
C Z
C Z
A Z
A Y
C Z
C X
A Z
A Z
C Z
C Z
C Z
A Y
B Z
B Z
B Z
B Z
C Z
C X
C Z
C Z
B Z
B Z
C Z
C X
A Z
A Y
C Z
B Z
C X
C X
A Y
A Y
C Z
B X
C Z
C Y
C X
A Y
C Y
C Z
C Z
B Z
A Z
C Z
C Z
C Z
C X
C Z
B Z
A Y
C Z
B Z
C Z
A Y
A Y
B X
B X
B Z
C Z
C Z
C Z
C Z
C X
C Z
B Z
C X
C X
A Z
B Y
C Z
C Z
C X
B Z
C Z
C Z
A Z
C Z
C X
A Y
A Z
C Z
B Z
C Z
B X
A Y
C Z
C Z
B Z
C X
B Z
B Z
C Z
C Z
C Z
B Z
A Z
C Z
A X
C Z
C Z
C Z
C X
C X
B Z
C Y
C Z
C Y
A Z
A Y
A X
B Z
A Y
C Z
C Y
B Z
B Z
B Z
B Y
B Z
A X
C Z
A Y
B Z
A Y
C Z
C Z
C Z
A Y
C Z
B Z
C Z
A Z
C Y
C X
A Y
A Y
A Y
B Z
C Y
C Z
C Z
C X
B Z
A Y
C Z
C Z
B Z
A Z
C Z
B Z
C X
C Z
B Z
C Z
C Z
B Y
B Z
C Y
B Z
A X
C Y
B Z
C Z
A Y
C Z
A Z
C Z
B Z
A X
C Y
C Y
A Y
A Z
B Z
A Z
C Z
C Y
C X
C Z
A Y
C Z
A Y
C Z
C Z
C Z
C Y
C X
C X
B Z
A Y
C Z
A X
C Z
A Z
C X
A X
A Z
A Y
C Z
C Z
C Z
B Z
B Z
C Z
C Z
C Z
A Y
C Z
A Z
C Z
C Z
C X
C X
C Z
C Z
C Z
C Z
C Z
A Y
B Z
B Z
C Z
B X
C Y
C Z
C Z
C Z
A Y
B X
C Z
C X
B Z
A Z
B X
C Z
C Z
B Z
C X
B Z
C Z
A Y
C X
A Y
A Z
C Y
C Z
C Y
C Z
C X
C Z
C Z
C Z
C Z
C Z
C Z
B Z
C Z
B Z
C X
C Z
C X
B Z
A Z
A X
C Z
A Y
C Y
C Z
C Z
C Z
B Z
B Z
A X
C Z
B Z
C Z
C Z
A X
C Z
B Z
C X
C Z
C Z
C Z
C Z
B X
C X
B Z
C Z
B Z
A Y
C Z
A Y
B X
C Z
C Z
C Z
C Z
A Y
C Z
C Z
B Z
C Z
C Z
B Z
A Y
C X
C Z
C Z
A Y
C X
B Z
B Z
A Y
C Z
B Z
C Z
A Y
B Z
A Z
C Z
B Z
C Z
B Z
C X
B Z
C Z
C Z
C Z
A Y
A Y
C Z
B Z
A Y
B X
C Z
B Z
C X
C Z
B Z
B Z
C Y
C Y
A Z
C X
C Z
B Z
A Y
C Z
C Z
B Z
A Y
B Z
A Z
C Z
B Z
B Z
B Z
C Z
C X
B Z
C Z
B Z
C Z
C Z
A Y
A Y
C Z
C X
A Y
B Z
C Z
A Z
C Z
B Z
A Y
C Z
C X
C Z
C Z
C Z
C Z
A Y
C Z
C X
B Z
B X
C Z
A Y
C Y
C Z
C Z
C X
C Z
C X
C X
C Z
B Z
B X
A Z
A Z
C Z
C Y
A Y
C Z
A Y
C Z
C Z
C Z
B Z
B Z
C Z
B Z
C Z
C Z
C Y
B Z
B Z
C Z
C Z
B Z
C X
A Z
C Z
C Z
A Y
A Y
A Y
C Z
C Z
B Z
A Z
A X
A Y
C X
C Z
B Z
C X
C X
B Z
B Z
B X
C Z
C Y
B Y
C Z
C Z
C Z
B Y
A Y
A Y
C Z
C Z
C Z
C Z
C Z
C Z
C Z
C Z
B X
A Z
A X
C Z
A Y
A Z
C X
C Z
C Y
C Z
C Z
C Z
B Z
B Z
A Y
A Z
C X
B X
B X
C Z
C Y
C Z
C Z
C Y
C Z
C Z
A Z
B Z
C Z
A Y
C Z
C Z
C Z
A Y
C Z
B Y
B X
C Z
A Y
B X
C Y
A Y
C Z
C Z
C Y
A Z
B Z
C Z
C Z
B Z
C Y
C X
C Y
C Y
B Z
C Z
B Z
C Z
A Y
A Y
A Y
B Z
C Z
A Z
C Z
C Y
B Z
B Z
C Y
C Z
C Z
A Y
C Z
C Z
B Z
C Z
C X
A X
A Y
A Y
B Z
C Z
B X
A Y
B Z
C Z
C Z
B Z
A Y
C Z
C Z
B X
A Y
C Z
B X
A Y
C Z
C X
A Z
B Z
B Z
C Z
C X
A Y
C Z
C Z
A Y
B Z
C Z
A Y
C Z
A Y
C Z
B Z
C Z
A X
B Z
C Z
C Z
C X
B Y
A Y
A X
C X
B Z
C Z
C X
B Z
C Z
C Z
C Z
C Z
A Z
C Z
A Y
B X
C Z
C Z
B Z
B Z
C X
B Z
B X
C Z
C Z
C Z
A Y
C Z
C Z
C Z
C Z
A Y
A Z
B Z
C Y
A Z
C Z
C Z
C Z
B X
B Z
C Z
B Z
C Z
C X
C Z
A X
C Z
C Z
C Z
C Z
C Z
A Y
A Y
B Z
B Z
B Z
B Z
C Z
C Z
C Z
C Z
A Z
A Y
C Z
A Z
A Y
C Z
C Z
C Y
A Y
B Z
C X
B Z
A Y
C Z
A Y
C Z
B Z
B Z
B Z
C Z
C Z
A Z
A Y
C X
A Y
C Z
A Y
C Z
C Z
A Y
B Z
C Z
C Y
C Z
B Z
B Z
B Z
C Y
C Z
B Z
C X
B Z
A Y
B Z
C Z
A Y
C Z
C Y
C Z
C Z
C Z
C Z
B X
B Z
C Z
C Z
A Y
C Y
C Z
C Z
C Z
C Z
C Z
C X
B Z
C X
A Y
B X
A Y
C Z
B Z
A Y
B Z
C Z
A Y
C Z
C Z
C X
C X
C Z
B Z
C X
C Z
A X
A Z
C Z
C Y
A Y
A X
B Z
C Z
C Z
B Z
A Y
C Z
A X
B Z
B Z
A X
C Y
C Z
A Y
B Z
C Z
C Z
C Z
B Z
C Z
B Z
C Z
B X
B Z
C Z
A X
C Z
A X
C Z
A Y
C Z
A Z
C Z
C Z
C Z
B X
A Y
A Y
A X
B Z
B Z
C Y
C Z
B Z
A X
C Z
A Y
C Z
C Z
B Z
C Z
C X
B X
B Z
B X
C Z
C Z
B Z
A Z
C Z
C Y
C Y
C Z
C Z
C Z
C Z
C Z
C Z
A X
A Z
C Z
B Z
B Z
B Z
C X
A Z
C Z
C Z
B Z
B Z
C Z
B X
C Z
B Z
C Z
C Z
C Z
C Z
A X
A Z
A Y
C Z
A Z
A Y
C Z
A Z
C Z
A Y
C Z
B Z
C Z
A Y
C Y
A Y
B Z
A Y
B Z
C Z
B X
C X
C Z
B Z
A Z
C Z
B X
C Y
C Z
C Y
A X
C X
C X
B Z
A Y
C Z
A Z
B Z
C Z
C X
C Z
C Z
C Z
C Z
C Z
B Z
B Z
C Z
C Z
C Z
C Z
C X
A Z
C Z
A Y
A Y
C Z
C Z
C Y
C Z
A Y
C Z
B Z
B X
A Z
A Y
A Y
C Z
A Y
C Z
A X
A Y
B X
C Z
C Z
A Z
C Z
A Y
B Z
A Y
C Z
C Z
B Z
C X
C Z
C Z
C Z
C Y
C Z
B X
C Z
C Z
B X
A Z
C Z
C X
C X
B Z
C Z
C Z
B Z
C Z
C Z
B Z
B Z
B Z
B Z
B Z
A Z
C X
C Y
C Z
A Y
C Z
B Z
A Y
C Z
C Z
B Z
B Z
C Z
A Z
A X
A Y
C Z
A Z
C Z
C X
B Z
C Z
A X
B Z
B X
C Z
C Z
C Z
B X
C Z
B Z
B Z
A Y
A Y
A Z
C Z
C Z
C Z
B Z
C Z
B Z
C Z
A Z
C Z
C X
C Z
C Z
C Z
B Z
A Y
C Y
C Z
C Z
C Z
C Z
B Z
C Y
C Z
A Z
C Y
C Z
B X
A Y
A Y
A X
B Z
B Z
C Z
A Y
B X
A Y
C X
C Z
A Y
C X
C Z
C Z
A Z
C X
C Z
C X
C Z
A Y
C Z
C Z
B Z
C Z
C Y
A Z
C X
C Y
C Z
C Z
C Y
B Z
B Z
C X
C Z
C Z
C Z
C Z
B Z
C Z
C Z
C Z
B Y
C Y
C Z
B Z
C Z
C Z
C Y
B Z
A Y
A Z
C Y
C Y
C Z
C X
C Z
A Z
C Z
C Z
A Y
A Z
C Z
A Z
C Z
B Z
B X
B Z
B Z
A Z
C Z
A Y
C Z
C Y
C Z
A Z
B Z
B Z
B Z
C Z
B Z
A Z
A Y
C Z
C Z
C Z
C Z
C Z
A Z
C Z
A Z
A X
B Z
A Y
C X
A Z
B X
B Y
A Y
B Z
A Z
B Z
B Z
C Z
A Z
B Z
B Z
A Y
C Y
C Y
A X
C Z
C Z
C Z
C X
C Z
C Z
C Z
C Z
B Z
A Y
C Z
C Z
C Z
A Z
C X
C Z
B Z
C Y
B X
B Y
C Z
C Z
A X
C Y
C X
B Z
B Z
C Z
B Z
C Y
C Y
C Y
C X
C Z
C Z
C Z
C X
A Y
C Z
C Z
B Z
A Z
C Z
A Y
B X
C Z
C X
C Z
A Z
B Z
A Y
C Z
C Z
C Z
A Z
C Z
C Z
C X
C Z
C Z
B Z
C Z
B Z
C X
C Z
A Y
A Y
C Z
C Z
B Z
C Z
C Z
C X
C Z
A Y
C Z
C Z
C Z
B Z
C Z
A Y";
    }
}
