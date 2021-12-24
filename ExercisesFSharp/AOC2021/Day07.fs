// https://adventofcode.com/2021/day/7
module ExercisesFSharp.AOC2021.Day7

open Xunit
open System

let solution1 input =
    let sorted = input |> List.sort |> Array.ofList
    let median = sorted[sorted.Length / 2]
    sorted |> Seq.map (fun x -> (x - median) |> abs) |> Seq.sum

let solution2 (input: decimal list) =
    let avg = input |> List.average

    // Summation of n Numbers Formula. The sum of “n” numbers formulas for the natural numbers is given as. n(n + 1)/2
    let fuel (d: decimal) = (d * (d + 1m))/2m

    let calc avg =
        input
        |> List.map (fun i -> (i - avg) |> abs |> fuel)
        |> List.sum

    let withFloor = calc (Math.Floor avg)
    let withCeiling = calc (Math.Ceiling avg)
    if withFloor < withCeiling then withFloor
    else withCeiling

[<Fact>]
let ``day 7 input 1 solution 1`` () =
    let input = [16;1;2;0;4;2;7;1;2;14]

    let actual = solution1 input

    Assert.Equal(37, actual)

[<Fact>]
let ``day 7 input 2 solution 1`` () =
    let input =
        InputUtils.readFile "day7_input.txt"
        |> Seq.head
        |> InputUtils.splitLineToInts ","
        |> Seq.toList

    let actual = solution1 input

    Assert.Equal(341534, actual)

[<Fact>]
let ``day 7 input 1 solution 2`` () =
    let input = [16;1;2;0;4;2;7;1;2;14] |> List.map Convert.ToDecimal

    let actual = solution2 input

    Assert.Equal(168m, actual)

[<Fact>]
let ``day 7 input 2 solution 2`` () =
    let input =
        InputUtils.readFile "day7_input.txt"
        |> Seq.head
        |> InputUtils.splitLineToInts ","
        |> Seq.map Convert.ToDecimal
        |> Seq.toList

    let actual = solution2 input

    Assert.Equal(93397632m, actual)
