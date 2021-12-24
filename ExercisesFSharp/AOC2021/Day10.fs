// https://adventofcode.com/2021/day/10
module ExercisesFSharp.AOC2021.Day10

open Xunit

let toSyntaxErrorScore (str: string) =
        str.ToCharArray()
        |> Array.fold
               (fun ((rest, acc): (char list * int)) c ->
                    match rest, acc with
                    | _, a when a > 0 -> rest, acc // first illegal already found
                    | [], _ -> c::[], acc
                    | r::rs, _ ->
                        match r,c with
                        | '[',']' | '(',')' | '<','>' | '{','}' -> rs, acc // take out matching
                        | _,'[' | _,'(' | _,'<' | _,'{' -> c::r::rs, acc // add new open char
                        | _,')' -> rs, 3
                        | _,']' -> rs, 57
                        | _, '}' -> rs, 1197
                        | _, '>' -> rs, 25137
                        | _ -> c::r::rs, acc
               )
               ([], 0)
        |> snd

let solution1 (input: string seq) =
    input
    |> Seq.map toSyntaxErrorScore
    |> Seq.reduce (+)

let solution2 input =
    let complete (str: string) =
        let removeMatching (rest: char list) (c: char) =
            match rest with
            | [] -> c::[]
            | r::rs ->
                match r,c with
                | '[',']' | '(',')' | '<','>' | '{','}' -> rs
                | _  -> c::r::rs

        let completeRemaining (acc: char list) (c: char) =
            match c with
            | '[' -> ']'::acc
            | '(' -> ')'::acc
            | '{' -> '}'::acc
            | '<' -> '>'::acc
            | _ -> failwith $"invalid input {str}"

        str.ToCharArray()
        |> Array.fold removeMatching []
        |> List.fold completeRemaining []
        |> List.rev

    let rec calcScore res (completed: char list) =
        match completed with
        | [] -> res
        | c::cs ->
            match c with
            | ')' -> calcScore (res*5L + 1L) cs
            | '}' -> calcScore (res*5L + 3L) cs
            | ']' -> calcScore (res*5L + 2L) cs
            | '>' -> calcScore (res*5L + 4L) cs
            | _ -> failwith $"invalid input {completed}"

    input
    |> Seq.map (fun str -> str, str |> toSyntaxErrorScore)
    |> Seq.filter (snd >> ((=) 0))
    |> Seq.map (fst >> complete >> (calcScore 0))
    |> Seq.sort
    |> Seq.toArray
    |> (fun arr -> arr[(arr.Length-1)/2])

[<Theory>]
[<InlineData("day10_sample.txt", 26397)>]
[<InlineData("day10_input.txt", 290691)>]
let ``day10 solution 1`` file expected =
    let input = InputUtils.readFile file

    let actual = solution1 input

    Assert.Equal(expected, actual)

[<Theory>]
[<InlineData("day10_sample.txt", 288957L)>]
[<InlineData("day10_input.txt", 2768166558L)>]
let ``day10 solution 2`` file expected =
    let input = InputUtils.readFile file

    let actual = solution2 input

    Assert.Equal(expected, actual)
