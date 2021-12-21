module ExercisesFSharp.AOC2021.Day9

open FSharpPlus.Control
open Xunit

let isLowPoint (x: int) (y: int) (arr: int array array) =
    let toLocation (x, y) = arr[y][x]
    let left = (x - 1), y
    let right = (if x < arr[0].Length - 1 then x + 1 else -1), y
    let top = x, (y - 1)
    let bottom = x, (if y < arr.Length - 1 then y + 1 else -1)
    let location = toLocation (x,y)
    [left; right; top; bottom]
    |> List.filter (fun (i,j) -> i >= 0 && j >= 0)
    |> List.map toLocation
    |> List.forall (fun i -> i > location)

let solution1 (input: string seq) =
    let strToIntArray str = str |> Seq.map (string >> int) |> Seq.toArray
    let arr = input |> Seq.map strToIntArray |> Seq.toArray

    [for y in 0 .. (arr.Length - 1) do
         for x in 0 .. (arr[0].Length - 1) do
             if isLowPoint x y arr then yield arr[y][x]]
    |> List.map ((+) 1)
    |> List.sum


[<Theory>]
[<InlineData("Day9_sample.txt", 15)>]
[<InlineData("Day9_input.txt", 498)>]
let ``day9 solution 1`` file expected =
    let input = InputUtils.readFile file

    let actual = solution1 input

    Assert.Equal(expected, actual)
