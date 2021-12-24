// https://adventofcode.com/2021/day/2
module ExercisesFSharp.AOC2021.Day2

open System
open Xunit
open InputUtils

type Command =
    | Forward of int
    | Down of int
    | Up of int

let strToCommand (str: string) =
    match str.Split(" ") with
    | [| command; n |] ->
        match command, Int32.TryParse n with
        | "forward", (true, number) -> Forward number |> Some
        | "down", (true, number) -> Down number |> Some
        | "up", (true, number) -> Up number |> Some
        | _ -> None
    | _ -> None

let toCommands input =
    input
        |> Seq.map strToCommand
        |> Seq.choose id

let dive (input: string seq) =
    let commands = input |> toCommands

    let next (currentPosition: int*int) command =
        let (horizontal, depth) = currentPosition;
        match command with
        | Forward n -> (horizontal + n, depth)
        | Down n -> (horizontal, depth + n)
        | Up n -> (horizontal, depth - n)

    let horizontal, depth = commands |> Seq.fold next (0,0)
    horizontal * depth

let divePart2 (input: string seq) =
    let commands = input |> toCommands

    let next (currentPosition: int*int*int) command =
        let (horizontal, depth, aim) = currentPosition;
        match command with
        | Forward n -> (horizontal + n, depth + aim * n, aim)
        | Down n -> (horizontal, depth, aim + n)
        | Up n -> (horizontal, depth, aim - n)

    let horizontal, depth, _ = commands |> Seq.fold next (0,0,0)
    horizontal * depth

[<Theory>]
[<InlineData("day2_sample.txt", 150)>]
[<InlineData("day2_input.txt", 1692075)>]
let ``day2 dive`` file expected =
    let input = readFile file

    let actual = dive input

    Assert.Equal(expected, actual)

[<Theory>]
[<InlineData("day2_sample.txt", 900)>]
[<InlineData("day2_input.txt", 1749524700)>]
let ``day2 dive part 2`` file expected =
    let input = readFile file

    let actual = divePart2 input

    Assert.Equal(expected, actual)
