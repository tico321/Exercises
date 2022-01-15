// https://adventofcode.com/2021/day/13
module ExercisesFSharp.AOC2021.Day13

open System
open Xunit
open FSharpPlus

let readInput file =
    let input = InputUtils.readFile file |> Seq.toList
    let dots =
        input
        |> List.takeWhile (fun (str: string) -> not <| String.IsNullOrEmpty(str))
        |> List.map (sscanf "%i,%i")
    let folds =
        input
        |> List.skipWhile (fun (str: string) -> String.IsNullOrEmpty(str) || str.StartsWith("fold") |> not)
        |> List.map (sscanf "fold along %s=%i")
    (dots, folds)

let foldAt dots (axes, n) =
    dots
    |> List.map (fun (x,y) ->
        match axes with
        | "x" when x > n -> ((2*n)-x, y)
        | "y" when y > n -> (x, (2*n)-y)
        | _ -> (x, y))
    |> List.distinct

let solution1 dots folds =
    [folds |> List.head]
    |> List.fold foldAt dots
    |> List.length

let solution2 dots folds =
    let foldedDots = folds |> List.fold foldAt dots
    let xSize, ySize =
        foldedDots
        |> List.fold (fun (maxX, maxY) (x,y) -> Math.Max(x , maxX), Math.Max(y , maxY)) (Int32.MinValue, Int32.MinValue)
    let yDotsMap =
        foldedDots
        |> List.groupBy snd
        |> List.map (fun (y, xs) -> y, xs |> List.map fst |> Set.ofList)
        |> Map.ofList


    [for y in 0 .. ySize ->
        [for x in 0 .. xSize do
            if yDotsMap.ContainsKey(y) && yDotsMap[y].Contains(x) then yield '#'
            else yield ' ' ] ]
    |> List.map (fun charArr -> String(charArr |> List.toArray))
    |> (fun rows -> String.Join(Environment.NewLine, rows))

[<Theory>]
[<InlineData("day13_sample.txt", 17)>]
[<InlineData("day13_input.txt", 842)>]
let ``day13 solution 1`` file expected =
    let dots, folds = readInput file

    let actual = solution1 dots folds

    Assert.Equal(expected, actual)

[<Theory>]
[<InlineData("day13_sample.txt",
"#####
#   #
#   #
#   #
#####")>]
[<InlineData("day13_input.txt",
"###  #### #  # ###   ##    ## #### #  #
#  # #    # #  #  # #  #    #    # #  #
###  ###  ##   #  # #       #   #  #  #
#  # #    # #  ###  #       #  #   #  #
#  # #    # #  # #  #  # #  # #    #  #
###  #    #  # #  #  ##   ##  ####  ## ")>]
let ``day13 solution 2`` file expected =
    let dots, folds = readInput file

    let actual = solution2 dots folds

    Assert.Equal(expected, actual)
