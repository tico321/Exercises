// https://adventofcode.com/2021/day/6
module ExercisesFSharp.AOC2021.Day6

open Xunit

let solution1 lanternFish days =
    let rec next lanternFish acc =
        match lanternFish with
        | [] -> acc
        | head :: tail when head = 0 -> next tail (6 :: 8 :: acc)
        | head :: tail -> next tail (head - 1 :: acc)

    [1..days]
    |> List.fold
        (fun accLanternFish _ -> next accLanternFish [])
        lanternFish
    |> (fun lanternFish -> lanternFish.Length)

let solution1' lanternFish days =
    [1..days]
    |> List.fold
        (fun acc _ -> acc |> List.collect (fun fish -> if fish = 0 then [6;8] else [fish - 1]))
        lanternFish
    |> (fun acc -> acc.Length)

let solution2 lanternFish days =
    let lanternFishIndexed =
        lanternFish
        |> List.fold
               (fun indexList currentFish -> indexList |> List.updateAt currentFish (indexList[currentFish] + 1L))
               ([0..9] |> List.map (fun _ -> 0L))
    let next (indexedLanternFish: int64 list) =
        [0..9]
        |> List.map (fun i ->
            match i with
            | 6 -> indexedLanternFish[0] + indexedLanternFish[7]
            | 8 -> indexedLanternFish[0] + indexedLanternFish[9]
            | 9 -> 0
            | _ -> indexedLanternFish[i+1])

    [1..days]
    |> List.fold
        (fun accLanternFish _ -> next accLanternFish)
        lanternFishIndexed
    |> List.sum

[<Fact>]
let ``day 6 part 1 input 1`` () =
    let input = [3;4;3;1;2]

    let actual = solution1 input 80

    Assert.Equal(5934, actual)

open System.Linq
[<Fact>]
let ``dat 6 part 1 input 2`` () =
    let input =
        InputUtils.readFile "day6_input.txt"
        |> Seq.head
        |> InputUtils.splitLineToInts ","
        |> Seq.toList

    let actual = solution1 input 80

    Assert.Equal(391671, actual)

[<Fact>]
let ``day 6 part 2 input 1`` () =
    let input = [3;4;3;1;2]

    let actual = solution2 input 256

    Assert.Equal(26984457539L, actual)

[<Fact>]
let ``day 6 part 2 input 2`` () =
    let input =
        InputUtils.readFile "day6_input.txt"
        |> Seq.head
        |> InputUtils.splitLineToInts ","
        |> Seq.toList

    let actual = solution2 input 256

    Assert.Equal(1754000560399L, actual)
