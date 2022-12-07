using System;
using System.Collections.Generic;
using System.Linq;


namespace AdventOfCode2022
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            CalorieCounting elfCalories = new();
            elfCalories.FindMaxCalorie();
            elfCalories.FindTopThreeCaloriesTotal();

            RockPaperScissors rps = new();

            rps.FindTotalScore();

            RuckSackReorganization rucky = new();
            rucky.FindPrioritiesSum();
            rucky.FindBadgeSum();

            CampCleanup campy = new();
            campy.FindFullyContainedAssignmentPairs();
            campy.FindPartiallyContainedAssignmentPairs();

            SupplyStacks stacky = new();
            stacky.MoveStacks();
            stacky.MoveStacks_CrateMover9001();

            TuningTrouble tuna = new();
            tuna.FindMarker(4); //packet marker
            tuna.FindMarker(14); //message marker

        }

    }
    
}
