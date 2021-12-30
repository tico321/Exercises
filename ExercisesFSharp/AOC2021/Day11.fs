module ExercisesFSharp.AOC2021.Day11

open Xunit

let neighbors ((x,y): int*int) maxX maxY : (int*int) list =
    [for i in x-1 .. x+1 do
     if i >= 0 && i < maxX then
        for j in y-1 .. y+1 do
        if j >=0 && j < maxY then
            yield (i, j)]
    |> List.filter ((<>) (x,y))

module Array2D =
    let maxX (arr: 'a[,]) = (arr.GetLength 1) - 1
    let maxY (arr: 'a[,]) = (arr.GetLength 0) - 1
    let positions (arr: 'a[,]) =
        [for x in 0 .. (maxX arr) do
            for y in 0 .. (maxY arr) do yield (x, y)]
    let filter (predicate: int*int*'a -> bool) (arr: 'a[,]) =
        arr
        |> positions
        |> List.filter (fun (x,y) -> predicate (x, y, arr[y, x]))

let solution1' (input: int list list) n =
    let arr = array2D input
    let mutable count = 0

    let incrementStep arr = arr |> Array2D.positions |> List.iter (fun (x,y) -> arr[y,x] <- arr[y,x] + 1)

    let rec flushPos (x, y, arr: int[,]) =
        if arr[y,x] < 0 then ()
        else
        arr[y,x] <- -100
        count <- count + 1

        neighbors (x, y) (Array2D.maxX arr + 1) (Array2D.maxY arr + 1)
        |> List.iter (fun (x,y) ->
            arr[y,x] <- arr[y,x] + 1
            if arr[y,x] >= 10 then flushPos (x,y,arr))

    [1..n]
    |> List.iter (fun _ ->
        arr |> incrementStep

        arr
        |> Array2D.filter (fun (_,_,v) -> v >= 10)
        |> List.iter (fun (x,y) -> flushPos (x,y, arr))

        arr
        |> Array2D.filter (fun (_,_,v) -> v < 0)
        |> List.iter (fun (x,y) -> arr[y,x] <- 0)
    )
    count

let solution2 (input: int list list) n =
    let arr = array2D input
    let mutable count = 0

    let incrementStep arr = arr |> Array2D.positions |> List.iter (fun (x,y) -> arr[y,x] <- arr[y,x] + 1)

    let rec flushPos (x, y, arr: int[,]) =
        if arr[y,x] < 0 then ()
        else
        arr[y,x] <- -100
        count <- count + 1

        neighbors (x, y) (Array2D.maxX arr + 1) (Array2D.maxY arr + 1)
        |> List.iter (fun (x,y) ->
            arr[y,x] <- arr[y,x] + 1
            if arr[y,x] >= 10 then flushPos (x,y,arr))

    [1..n]
    |> List.fold (fun (found, step) i ->
        arr |> incrementStep

        arr
        |> Array2D.filter (fun (_,_,v) -> v >= 10)
        |> List.iter (fun (x,y) -> flushPos (x,y, arr))

        arr
        |> Array2D.filter (fun (_,_,v) -> v < 0)
        |> List.iter (fun (x,y) -> arr[y,x] <- 0)

        if found = true then (true, step)
        elif count = 100 then (true, i)
        else
            count <- 0
            (false, i)
        )
        (false, 0)
    |> snd

[<Theory>]
[<InlineData("day11_sample.txt", 2, 9)>]
[<InlineData("day11_sample2.txt", 100, 1656)>]
[<InlineData("day11_input.txt", 100, 1679)>]
let ``day 11 solution1`` file n expected =
    let input =
        InputUtils.readFile file
        |> Seq.map (fun str -> str |> Seq.map (string >> int) |> Seq.toList)
        |> Seq.toList

    let actual = solution1' input n

    Assert.Equal(expected, actual)

[<Theory>]
[<InlineData("day11_input.txt", 1000, 519)>]
let ``day 11 solution2`` file n expected =
    let input =
        InputUtils.readFile file
        |> Seq.map (fun str -> str |> Seq.map (string >> int) |> Seq.toList)
        |> Seq.toList

    let actual = solution2 input n

    Assert.Equal(expected, actual)

[<Fact>]
let ``day 11 solution1 incremental`` () =
    Assert.Equal(0, solution1' [[0]] 1)
    Assert.Equal(0, solution1' [[0]] 9)
    Assert.Equal(1, solution1' [[0]] 10)
    Assert.Equal(2, solution1' [[0]] 20)
    Assert.Equal(1, solution1' [[9]] 1)
    Assert.Equal(2, solution1' [[9]] 11)
    Assert.Equal(0, solution1' [[0;0]] 1)
    Assert.Equal(2, solution1' [[9;9]] 1)
    Assert.Equal(2, solution1' [[9;8]] 1)
    Assert.Equal(2, solution1' [[9;7]] 2)

[<Fact>]
let ``day 11 filter array2D`` () =
    Assert.Equal<List<int*int>>(
        [(1,1)],
        array2D [[0;0;0];[0;1;0];[0;0;0]] |> Array2D.filter (fun (_, _, v) -> v > 0))

[<Fact>]
let ``day 11 neighbors test`` () =
    Assert.Equal<List<int*int>>([], (neighbors (0,0) 1 1))
    Assert.Equal<List<int*int>>([(1, 0)], (neighbors (0,0) 2 1))
    Assert.Equal<List<int*int>>([(0, 0)], (neighbors (1,0) 2 1))
    Assert.Equal<List<int*int>>([(1, 0)], (neighbors (0,0) 3 1))
    Assert.Equal<List<int*int>>([(1, 0)], (neighbors (2,0) 3 1))
    Assert.Equal<List<int*int>>([(0, 0); (2, 0)], (neighbors (1,0) 3 1))
    Assert.Equal<List<int*int>>([(0, 1)], (neighbors (0,0) 1 2))
    Assert.Equal<List<int*int>>([(0, 0)], (neighbors (0,1) 1 2))
    Assert.Equal<List<int*int>>([(0, 1); (1, 0); (1, 1)], (neighbors (0,0) 2 2))
    Assert.Equal<List<int*int>>(
        [(0, 0); (0, 1); (0, 2); (1, 0); (1, 2); (2, 0); (2, 1); (2, 2)],
        (neighbors (1,1) 3 3))
