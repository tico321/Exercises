﻿using Exercises.Easy;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var input2 = Console.ReadLine();
            var result = MikeGCDProblemSolution.Solve(input, input2);
            Console.WriteLine(result);
        }
    }
}
