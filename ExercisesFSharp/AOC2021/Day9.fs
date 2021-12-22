module ExercisesFSharp.AOC2021.Day9

open FSharpPlus.Control
open Microsoft.FSharp.Collections
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

let inputToIntMatrix input =
    let strToIntArray str = str |> Seq.map (string >> int) |> Seq.toArray
    input |> Seq.map strToIntArray |> Seq.toArray

let solution1 (input: string seq) =
    let arr = input |> inputToIntMatrix

    [for y in 0 .. (arr.Length - 1) do
         for x in 0 .. (arr[0].Length - 1) do
             if isLowPoint x y arr then yield arr[y][x]]
    |> List.map ((+) 1)
    |> List.sum


let addBasinStartingAtPos (basins, taken: (int*int) Set, arr: int array array) (y,x) =
    let mergeSets (s: (int*int) Set) (acc: (int*int) Set) = s |> Set.fold (fun (acc: (int*int) Set) -> acc.Add) acc
    let rec getNewBasinFromPos (y,x) (acc: (int*int) Set) =
        if y < 0 || y >= arr.Length then acc
        elif x < 0 || x >= arr[0].Length then acc
        elif acc.Contains (y,x) then acc
        else match arr[y][x] with
             | 9 -> acc
             | _ -> // any height
                 acc.Add (y,x)
                 |> getNewBasinFromPos (y, x-1) // left
                 |> getNewBasinFromPos (y, x+1) //right
                 |> getNewBasinFromPos (y + 1, x) // down
                 |> getNewBasinFromPos (y - 1, x) //up

    if taken.Contains (y,x) then (basins, taken, arr)
    else
        let newBasin = getNewBasinFromPos (y,x) Set.empty
        if newBasin = Set.empty then (basins, taken, arr)
        else (newBasin :: basins, taken |> mergeSets newBasin, arr)

let solution2 (input: string seq) =
    let arr = input |> inputToIntMatrix
    let basins: (int*int) Set list = []
    let taken: (int*int) Set = Set.empty

    [for y in 0 .. (arr.Length - 1) do for x in 0 .. (arr[0].Length - 1) -> (y, x)]
    |> List.fold addBasinStartingAtPos (basins, taken, arr)
    |> (fun (basins, _, _) -> basins)
    |> List.map (fun basin -> basin.Count)
    |> List.sortDescending
    |> List.take 3
    |> List.reduce (*)

[<Theory>]
[<InlineData("Day9_sample.txt", 15)>]
[<InlineData("Day9_input.txt", 498)>]
let ``day9 solution 1`` file expected =
    let input = InputUtils.readFile file

    let actual = solution1 input

    Assert.Equal(expected, actual)

[<Theory>]
[<InlineData("Day9_sample.txt", 1134)>]
[<InlineData("Day9_input.txt", 1071000)>]
let ``day9 solution 2`` file expected =
    let input = InputUtils.readFile file

    let actual = solution2 input

    Assert.Equal(expected, actual)
