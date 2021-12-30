module ExercisesFSharp.BowlingKata

open Xunit

module Bowling =
    type Frame =
        | Frame of ball1:int*ball2:int
        | Spare of ball1:int*ball2:int*bonus:int
        | Strike of ball:int*bonus1:int*bonus2:int

    let rec createFrames (frames: Frame list) (rolls: int list) =
        match rolls with
        | ball::bonus1::[ bonus2 ] when ball = 10 ->
            Strike (ball, bonus1, bonus2) :: frames
        | ball::bonus1::bonus2::remaining when ball = 10 ->
            createFrames (Strike (ball, bonus1, bonus2) :: frames) (bonus1::bonus2::remaining)
        | ball1::ball2::bonus::remaining when ball1 + ball2 = 10 ->
            createFrames (Spare (ball1, ball2, bonus) :: frames) (bonus::remaining)
        | ball1::ball2::remaining ->
            createFrames (Frame (ball1, ball2) :: frames) remaining
        | _ -> frames

    let sumFrames frames =
        frames
        |> List.map (fun frame ->
            match frame with
            | Spare (a,b,c) -> a+b+c
            | Frame (a,b) -> a+b
            | Strike (a,b,c) -> a+b+c)
        |> List.sum

    let play rolls =
        rolls
        |> createFrames []
        |> sumFrames

[<Theory>]
[<InlineData("0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0", 0)>]
[<InlineData("1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1", 20)>]
[<InlineData("6,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1", 29)>]
[<InlineData("10,10,10,10,10,10,10,10,10,10,10,10", 300)>]
let ``Bowling`` (input: string) expectedScore =
    let rolls = input.Split(",") |> Array.map int |> Array.toList
    let actual = Bowling.play rolls
    Assert.Equal(expectedScore, actual)
