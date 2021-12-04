module ExercisesFSharp.AOC2021.day1.problem_solution

open System

let day1SubmarineDrops input =
    input
    |> Seq.windowed 2
    |> Seq.fold
        (fun acc window ->
        match window with
        | [| a; b |] -> if b > a then (acc + 1) else acc
        | _ -> acc)
        0

type Board =
    { rows: (int*bool) list list; cols: (int*bool) list list }

    member this.Mark (number: int) =
        let mark list =
            list
            |> List.map (fun row ->
                row
                |> List.map (fun i ->
                    let n = (i |> fst)
                    if n = number then (n, true)
                    else i))

        { this with rows = this.rows |> mark; cols = this.cols |> mark }

    member this.UnmarkedSum () =
        this.rows
        |> List.map (fun r ->
            r
            |> List.filter (snd >> not)
            |> List.sumBy fst)
        |> List.sum

    member this.CheckWinCondition () =
        let any =
            this.rows @ this.cols
            |> List.map (fun r -> r |> List.filter snd)
            |> List.filter (fun r -> r.Length = 5)
        any.Length > 0

    static member FromInput (input: string []) =
        let rows =
            input
            |> Array.map (fun line ->
                line.Split(" ")
                |> Array.filter (Int32.TryParse >> fst)
                |> Array.map int
                |> Array.map (fun i -> (i, false))
                |> Array.toList)
            |> Array.toList

        let c =
            [ for i in 0 .. 4 do
              for j in 0 .. 4 -> rows[j][i] ]
        let cols =
            c
            |> List.chunkBySize 5

        { rows = rows; cols = cols }

let solveBingo (tickets: int list) (boards: Board list) =
    let rec iterate (bs: Board list, res: int) ticket =
        if res > 0 then (bs, res)
        else
        let newBoards = bs |> List.map (fun b -> b.Mark ticket)
        let winningBoards =
            newBoards
            |> List.filter (fun b -> b.CheckWinCondition())

        if winningBoards.Length > 0 then
            newBoards, winningBoards[0].UnmarkedSum() * ticket
        else
            (newBoards, 0)

    tickets
    |> List.fold iterate (boards, 0)

let solveBingo2 (tickets: int list) (boards: Board list) =
    let rec iterate (acc: Board list * Board list, res: int) ticket =
        let boards, _ = acc;
        if res > 0 then (acc, res)
        else
        let updatedBoards = boards |> List.map (fun b -> b.Mark ticket)
        let newBoards =
            updatedBoards
            |> List.filter (fun b -> b.CheckWinCondition() |> not)
        let winningBoards =
            updatedBoards
            |> List.filter (fun b -> b.CheckWinCondition())

        if newBoards.Length = 0 then
            (newBoards, winningBoards),
            winningBoards.[winningBoards.Length-1].UnmarkedSum() * ticket
        else
            ((newBoards, winningBoards), 0)

    tickets
    |> List.fold iterate ((boards, []), 0)
