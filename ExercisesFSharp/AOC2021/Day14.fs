module ExercisesFSharp.AOC2021.Day14

open System.Text
open Xunit
open FSharpPlus

let performInsertions (template: string) (insertionMap: Map<string, string>) =
    let builder = StringBuilder(template.Substring(0, 1), template.Length * 2 )
    template.ToCharArray()
    |> Array.pairwise
    |> Array.Parallel.map (fun (a,b) ->
        match insertionMap.TryFind($"{a}{b}") with
        | Some toInsert -> $"{toInsert}{b}"
        | None -> $"{b}")
    |> Array.fold (fun (acc: StringBuilder) -> acc.Append) builder
    |> (fun b -> b.ToString())

let solution1 input iterations =
    let rec loop template insertionMap i =
        if i <= 0 then template
        else
            let newTemplate = performInsertions template insertionMap
            loop newTemplate insertionMap (i - 1)

    let template = input |> Seq.head
    let insertionMap = input |> Seq.skip 2 |> Seq.map (sscanf "%s -> %s") |> Map.ofSeq

    let finalTemplate = loop template insertionMap iterations

    finalTemplate.ToCharArray()
    |> Array.groupBy id
    |> Array.map (fun (c, matches) -> (c, matches.Length))
    |> Array.sortBy snd
    |> (fun charCount ->
        let mostCommonCount = charCount |> Array.last |> snd
        let leastCommonCount = charCount |> Array.head |> snd
        mostCommonCount - leastCommonCount)


[<Theory>]
[<InlineData("day14_sample.txt", 1588)>]
[<InlineData("day14_input.txt", 3143)>]
let ``day 14 solution 1`` file expected =
    let input = InputUtils.readFile file

    let actual = solution1 input 10

    Assert.Equal(expected, actual)
