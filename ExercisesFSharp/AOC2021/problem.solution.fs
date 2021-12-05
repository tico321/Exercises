module ExercisesFSharp.AOC2021.day1.problem_solution

open System
open System.Linq
open ExercisesFSharp.AOC2021

module Day1 =

    let submarineDrops input =
        input
        |> Seq.windowed 2
        |> Seq.map (fun ab ->
            match ab with
            | [| a; b |] when b > a -> 1
            | _ -> 0)
        |> Seq.sum

    let submarineDropsPart2 (input: int seq) =
        input
        |> Seq.windowed 3
        |> Seq.map Seq.sum
        |> submarineDrops

module Day2 =
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

module Day4 =

    type Board =
        { rows: (int*bool) list list; cols: (int*bool) list list }

    module Board =
        let markNumber (number: int) board =
            let mark list =
                list
                |> List.map (fun row ->
                    row
                    |> List.map (fun i ->
                        let n = (i |> fst)
                        if n = number then (n, true)
                        else i))

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
        let iterate (bs: Board seq, res: int) ticket =
            if res > 0 then (bs, res)
            else
            let newBoards = bs |> Seq.map (Board.markNumber ticket)
            let winningBoards =
                newBoards
                |> Seq.filter Board.checkWinCondition
                |> Seq.toList

            if winningBoards.Length > 0 then
                let unmarked = Board.unmarkedSum winningBoards[0]
                newBoards, unmarked * ticket
            else
                (newBoards, 0)

        tickets
        |> Seq.fold iterate (boards, 0)

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
