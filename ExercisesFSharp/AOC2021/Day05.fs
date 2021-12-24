// https://adventofcode.com/2021/day/5
module ExercisesFSharp.AOC2021.Day5

open System
open Xunit
open InputUtils

type VentLine = { X1: int; Y1: int; X2: int; Y2: int }
module VentLine =
    let strToVertLine (str: string) =
        let points = str.Split(" -> ")
        let left = points[0].Split(",")
        let right = points[1].Split(",")
        { X1 = Convert.ToInt32(left[0]); Y1 = Convert.ToInt32(left[1])
          X2 = Convert.ToInt32(right[0]); Y2 = Convert.ToInt32(right[1]) }

    let create (input: string seq) =
        input
        |> Seq.filter (fun s -> not <| String.IsNullOrEmpty(s))
        |> Seq.map strToVertLine

    let toHorizontalAndVerticalPoints ventLine =
        if ventLine.X1 = ventLine.X2 then
            let y1 = min ventLine.Y1 ventLine.Y2
            let y2 = max ventLine.Y1 ventLine.Y2
            [ for y in y1 .. y2 -> (ventLine.X1, y) ] |> List.toSeq
        elif ventLine.Y1 = ventLine.Y2 then
            let x1 = min ventLine.X1 ventLine.X2
            let x2 = max ventLine.X1 ventLine.X2
            [ for x in x1 .. x2 -> (x, ventLine.Y1) ] |> List.toSeq
        else
            [] |> List.toSeq

    let toPoints ventLine =
        if ventLine.X1 = ventLine.X2 then
            let y1 = min ventLine.Y1 ventLine.Y2
            let y2 = max ventLine.Y1 ventLine.Y2
            [ for y in y1 .. y2 -> (ventLine.X1, y) ] |> List.toSeq
        elif ventLine.Y1 = ventLine.Y2 then
            let x1 = min ventLine.X1 ventLine.X2
            let x2 = max ventLine.X1 ventLine.X2
            [ for x in x1 .. x2 -> (x, ventLine.Y1) ] |> List.toSeq
        else
            let diff = Math.Abs(ventLine.X1 - ventLine.X2)
            let xPath = if ventLine.X1 < ventLine.X2 then 1 else -1 // goes from left to right / right to left
            let yPath = if ventLine.Y1 < ventLine.Y2 then 1 else -1 // goes from top to bottom / bottom to top
            [ for i in 0 .. diff -> (ventLine.X1 + (xPath * i), ventLine.Y1 + (yPath * i)) ]
            |> List.toSeq

    let countLineOverlappingPoints lines =
        lines
        |> Seq.collect id
        |> Seq.countBy id
        |> Seq.map snd
        |> Seq.filter (fun count -> count > 1)
        |> Seq.toList

let hydrothermalVenture input =
    let ventLines = VentLine.create input

    let count =
        ventLines
        |> Seq.map VentLine.toHorizontalAndVerticalPoints
        |> VentLine.countLineOverlappingPoints

    count.Length

let hydrothermalVenturePart2 input =
    let ventLines = VentLine.create input

    let count =
        ventLines
        |> Seq.map VentLine.toPoints
        |> VentLine.countLineOverlappingPoints

    count.Length

[<Theory>]
[<InlineData("day5_sample.txt", 5)>]
[<InlineData("day5_input.txt", 4655)>]
let ``day5 hydrothermal venture`` fileName expected =
    let input = readFile fileName

    let actual = hydrothermalVenture input

    Assert.Equal(expected, actual)

[<Theory>]
[<InlineData("day5_sample.txt", 12)>]
[<InlineData("day5_input.txt", 20500)>]
let ``day5 hydrothermal venture part 2`` fileName expected =
    let input = readFile fileName

    let actual = hydrothermalVenturePart2 input

    Assert.Equal(expected, actual)
