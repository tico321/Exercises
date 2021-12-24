// https://adventofcode.com/2021/day/8
module ExercisesFSharp.AOC2021.Day8

open Xunit

let readInput file =
    InputUtils.readFile file
    |> Seq.map(fun line ->
        let arr = line.Split(" | ") |> Array.map(fun str -> str.Split(" "))
        (arr[0], arr[1])
    )

let countUniqueSegments displays =
    let isUniqueSegment (s: string) =
        match s with
        | s when s.Length = 2 -> true
        | s when s.Length = 4 -> true
        | s when s.Length = 3 -> true
        | s when s.Length = 7 -> true
        | _ -> false

    displays
    |> Seq.map snd
    |> Seq.collect (fun segment -> segment |> Array.filter isUniqueSegment |> Array.map (fun _ -> 1))
    |> Seq.sum

open System

let sol2 (input: (string array * string array) seq) =
    let findMappedDigits (displays: string seq) =
        let whereLength n (str: string) = str.Length = n
        let whereDistinctCharsOfFirstHaveLength (first: string) n (second: string) =
            (Seq.except second first |> Seq.length) = n

        let one = displays |> Seq.find (whereLength 2)
        let four = displays |> Seq.find (whereLength 4)
        let seven = displays |> Seq.find (whereLength 3)
        let eight = displays |> Seq.find (whereLength 7)

        let elementsWithLengthSix = displays |> Seq.filter (whereLength 6)
        let six = elementsWithLengthSix |> Seq.find (whereDistinctCharsOfFirstHaveLength one 1)
        let sixRemains = elementsWithLengthSix |> Seq.filter ((<>) six)
        let nine = sixRemains |> Seq.find (whereDistinctCharsOfFirstHaveLength four 0)
        let zero = sixRemains |> Seq.find ((<>) nine)

        let elementsWithLengthFive = displays |> Seq.filter (whereLength 5)
        let three = elementsWithLengthFive|> Seq.find (fun e -> whereDistinctCharsOfFirstHaveLength e 3 one)
        let remaining = elementsWithLengthFive |> Seq.filter ((<>) three)
        let two =  remaining |> Seq.find (fun e -> whereDistinctCharsOfFirstHaveLength e 1 six)
        let five = remaining |> Seq.find ((<>) two)

        Map.ofList [ (zero, 0); (one, 1); (two, 2); (three, 3); (four, 4); (five, 5)
                     (six, 6);  (seven, 7); (eight, 8); (nine, 9)]

    let sortedString (str: string) = new string(str.ToCharArray() |> Array.sort)

    input
    |> Seq.mapi (fun ii (displays, outputs) ->
        let digitsMap = displays |> Seq.map sortedString |> findMappedDigits
        outputs
        |> Seq.map sortedString
        |> Seq.map (fun e -> if digitsMap.ContainsKey e then digitsMap[e] else failwith $"digit not found for key {ii} - {e} in {digitsMap}")
        |> Seq.rev
        |> Seq.mapi (fun i e -> (Math.Pow(10, i) |> int) * e )
        |> Seq.sum)
    |> Seq.sum

[<Theory>]
[<InlineData("day8_sample.txt", 26)>]
[<InlineData("day8_input.txt", 534)>]
let ``day8 solution 1`` text expected =
    let input = readInput text

    let actual = input |> countUniqueSegments

    Assert.Equal(expected, actual)

[<Theory>]
[<InlineData("day8_sample.txt", 61229)>]
[<InlineData("day8_input.txt", 1070188)>]
let ``day8 solution 2`` text expected =
    let input = readInput text

    let actual = input |> sol2

    Assert.Equal(expected, actual)
