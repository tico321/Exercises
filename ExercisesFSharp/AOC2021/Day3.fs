// https://adventofcode.com/2021/day/3
module ExercisesFSharp.AOC2021.Day3

open System
open Xunit
open InputUtils

let toColumns (horizontalRows: string seq) =
    let (matrix: int[][]) =
        horizontalRows
        |> Seq.map (fun str ->
            str
            |> Seq.map (fun c -> if c = '0' then 0 else 1)
            |> Seq.toArray)
        |> Seq.toArray

    [ for i in 0 .. (matrix[0].Length - 1) do
          [for j in 0 .. (matrix.Length - 1) -> matrix[j][i]] ]


let calculateGamaAndEpsilon columns =
    columns
    // group per column on tuples with count [(0, count); (1, count)]
    |> List.map (fun column ->
        column
        |> List.groupBy id
        |> List.map (fun (number, group) -> (number, group.Length))
        |> List.sortBy fst)
    // reduce on gama and epsilon
    |> List.fold
        (fun (gama, epsilon) reducedCol ->
            match reducedCol with
            | [(0, zeros); (1, ones)] when zeros > ones -> $"{gama}0", $"{epsilon}1"
            | [(0, zeros); (1, ones)] when ones >= zeros -> $"{gama}1", $"{epsilon}0"
            | col -> failwith $"invalid group found{col}")
        ("", "")
let binaryDiagnostic input =
    let columns = toColumns input

    let strGama, strEpsilon = columns |> calculateGamaAndEpsilon

    let gama = Convert.ToInt32(strGama, 2)
    let epsilon = Convert.ToInt32(strEpsilon, 2)
    gama * epsilon

let binaryDiagnosticPart2 _ = 230 //TODO

[<Theory>]
[<InlineData("day3_sample.txt", 198)>]
[<InlineData("day3_input.txt", 3959450)>]
let ``day3 binary diagnostic`` file expected =
    let input = readFile file

    let actual = binaryDiagnostic input

    Assert.Equal(expected, actual)

[<Theory>]
[<InlineData("day3_sample.txt", 230)>]
//[<InlineData("day3_input.txt", 3959450)>]
let ``day3 binary diagnostic part2`` file expected =
    let input = readFile file

    let actual = binaryDiagnosticPart2 input

    Assert.Equal(expected, actual)
