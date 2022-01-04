module ExercisesFSharp.AOC2021.Day12

open FSharpPlus
open Xunit
open System

let findPaths input =
    let addToGraph (o, d) (routeMap: Map<string, string list>) =
        if not <| routeMap.ContainsKey(o) then routeMap.Add(o, [d])
        else routeMap.Change(o, (Option.bind (fun ds -> (d::ds |> List.distinct) |> Some)))

    let isUpper (str: string) = str.ToCharArray() |> Array.forall Char.IsUpper

    let graph =
        input
        |> List.map (sscanf "%s-%s")
        |> List.fold
            (fun (routeMap: Map<string, string list>) (o, d) -> routeMap |> addToGraph (o,d) |> addToGraph (d, o))
            Map.empty

    let rec explorePaths (paths: string list list) (visited: string Set) : string list list =
        paths
        |> List.collect
               (function
                | x::xs when x = "end" -> [x::xs]
                | x::xs ->
                    match graph.TryGetValue(x) with
                    | false, _ -> []
                    | true, destinations ->
                        destinations
                        |> List.filter (fun p -> not <| visited.Contains(p))
                        |> List.collect (fun next ->
                            let nextVisited = if next |> isUpper then visited else visited.Add(next)
                            explorePaths [next::x::xs] nextVisited)
                | xs -> [xs])

    let graphToStringPath paths =
        paths
        |> List.map (fun arr -> String.Join(",", arr |> List.rev))
        |> List.sortByDescending (fun p -> p.Length)

    explorePaths [["start"]] (Set.empty.Add("start")) |> graphToStringPath

[<Theory>]
[<InlineData("start-A;A-end", "start,A,end")>]
[<InlineData("start-A;A-b;b-end", "start,A,b,end")>]
[<InlineData("A-b;b-end;start-A", "start,A,b,end")>]
[<InlineData("start-a;start-b;a-b;b-end", "start,a,b,end;start,b,end")>]
[<InlineData("start-a;start-b;a-b;b-end;a-end", "start,b,a,end;start,a,b,end;start,b,end;start,a,end")>]
[<InlineData("start-A;A-c;A-end", "start,A,c,A,end;start,A,end")>]
let ``day12 solution 1 samples`` (input: string) (expected: string) =
    let parsedInput = input.Split(";") |> Array.toList
    let parsedExpected = expected.Split(";") |> Array.toList

    let actual = findPaths parsedInput

    Assert.Equal<string list>(parsedExpected, actual)

[<Theory>]
[<InlineData("day12_sample1.txt", 10)>]
[<InlineData("day12_sample2.txt", 19)>]
[<InlineData("day12_input.txt", 5157)>]
let ``day12 solution 1`` file expected =
    let input = InputUtils.readFile file |> Seq.toList

    let actual = findPaths input |> List.length

    Assert.Equal(expected, actual)
