// https://adventofcode.com/2021/day/4
module ExercisesFSharp.AOC2021.Day4

open System
open Xunit
open InputUtils

type Board =
    { rows: (int*bool) list list; cols: (int*bool) list list }

module Board =
    let markNumber (number: int) board =
        let mark list =
             [ for row in list do [
                for col in row do
                    if (col |> fst) = number then (number, true)
                      else col
                  ]
             ]

        { board with rows = board.rows |> mark; cols = board.cols |> mark }

    let unmarkedSum board =
        board.rows
        |> List.map (fun r ->
            r
            |> List.filter (snd >> not)
            |> List.sumBy fst)
        |> List.sum

    let checkWinCondition board =
        let winConditions =
            board.rows @ board.cols
            |> List.map (fun r -> r |> List.filter snd)
            |> List.filter (fun r -> r.Length = 5)
        winConditions.Length > 0

    let create (input: string []) =
        let rows =
            input
            |> Array.map (fun line ->
                line
                |> InputUtils.splitLineToInts " "
                |> Seq.map (fun i -> (i, false))
                |> Seq.toList)
            |> Array.toList

        let cols =
            [ for i in 0 .. 4 do
              [ for j in 0 .. 4 -> rows[j][i]] ]

        { rows = rows; cols = cols }

let bingo (tickets: int seq) (boards: Board seq) =
    let rec iterate (bs: Board seq) tickets =
        match tickets with
        | [] -> failwith "Invalid input data"
        | ticket::ts ->
            let newBoards = bs |> Seq.map (Board.markNumber ticket)

            match Seq.tryFind Board.checkWinCondition newBoards with
            | Some winner -> Board.unmarkedSum winner * ticket
            | None -> iterate newBoards ts

    iterate boards (tickets |> Seq.toList)

let bingoPart2 (tickets: int seq) (boards: Board seq) =
    let iterate (acc: Board seq * Board seq, res: int) ticket =
        let boards, _ = acc;
        if res > 0 then (acc, res)
        else
        let updatedBoards = boards |> Seq.map (Board.markNumber ticket)
        let newBoards =
            updatedBoards
            |> Seq.filter (fun b -> Board.checkWinCondition b |> not)
            |> Seq.toList
        let winningBoards =
            updatedBoards
            |> Seq.filter Board.checkWinCondition
            |> Seq.toList

        if newBoards.Length = 0 then
            let unmarked = Board.unmarkedSum winningBoards.[winningBoards.Length-1]
            (newBoards, winningBoards), unmarked * ticket
        else
            ((newBoards, winningBoards), 0)

    tickets
    |> Seq.fold iterate ((boards, []), 0)

open System.Linq

let readBingoData fileName =
    let str =
        readFile fileName
        |> Seq.filter (not << String.IsNullOrEmpty)

    let tickets =
        str.First()
        |> splitLineToInts ","

    let boards =
        str
        |> Seq.skip 1
        |> Seq.chunkBySize 5
        |> Seq.map Board.create

    (tickets, boards)

[<Theory>]
[<InlineData("day4_sample.txt", 4512)>]
[<InlineData("day4_input.txt", 58412)>]
let ``day4 bingo`` fileName expected =
    let tickets, boards = readBingoData fileName

    let actual = bingo tickets boards

    Assert.Equal(expected, actual)


[<Theory>]
[<InlineData("day4_sample.txt", 1924)>]
[<InlineData("day4_input.txt", 10030)>]
let ``day4 bingo 2`` fileName expected =
    let tickets, boards = readBingoData fileName

    let actual = bingoPart2 tickets boards

    Assert.Equal(expected, actual |> snd)
