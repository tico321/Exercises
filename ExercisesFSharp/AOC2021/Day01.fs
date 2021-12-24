// https://adventofcode.com/2021/day/1
module ExercisesFSharp.AOC2021.Day1

open Xunit
open InputUtils

let submarineDrops input =
    input
    |> Seq.pairwise
    |> Seq.map (fun (a, b) -> if b > a then 1 else 0)
    |> Seq.sum

let submarineDropsPart2 (input: int seq) =
    input
    |> Seq.windowed 3
    |> Seq.map Seq.sum
    |> submarineDrops

[<Theory>]
[<InlineData("day1_sample.txt", 7)>]
[<InlineData("day1_input.txt", 1121)>]
let ``day1 submarine drops`` file expected =
    let input =
        readFile file
        |> Seq.map strToInt
        |> Seq.choose id

    let actual = submarineDrops input

    Assert.Equal(expected, actual)

[<Theory>]
[<InlineData("day1_sample.txt", 5)>]
[<InlineData("day1_input.txt", 1065)>]
let ``day1 submarine drops part 2`` file expected =
    let input =
        readFile file
        |> Seq.map strToInt
        |> Seq.choose id

    let actual = submarineDropsPart2 input

    Assert.Equal(expected, actual)
