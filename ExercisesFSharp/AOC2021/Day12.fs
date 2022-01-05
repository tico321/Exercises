module ExercisesFSharp.AOC2021.Day12

open FSharpPlus
open Xunit
open System

let isUpper (str: string) = str.ToCharArray() |> Array.forall Char.IsUpper

let toGraph input =
    let addToGraph (o, d) (routeMap: Map<string, string list>) =
        if not <| routeMap.ContainsKey(o) then routeMap.Add(o, [d])
        else routeMap.Change(o, (Option.bind (fun ds -> (d::ds |> List.distinct) |> Some)))

    input
    |> List.map (sscanf "%s-%s")
    |> List.fold (fun (m: Map<string, string list>) (o, d) -> m |> addToGraph (o,d) |> addToGraph (d, o)) Map.empty

let rec explorePaths paths goalTest getSuccessors =
    match goalTest paths, getSuccessors paths with
    | _, None -> paths
    | true, _ -> paths
    | false, Some successors -> explorePaths successors goalTest getSuccessors

let goalTest paths = paths |> List.forall (function x::_ -> x = "end" | _ -> false)

let getSuccessors isValid (graph: Map<string, string list>) paths =
        let successors =
            paths
            |> List.collect (function
                | x::xs when x = "end" -> [x::xs]
                | x::xs when graph.ContainsKey(x) ->
                    graph[x]
                    |> List.map (fun next -> next::x::xs)
                    |> List.filter isValid
                    |> List.distinct
                | _ -> [])
        match successors with
        | [] -> None
        | s -> Some s

let solution1 input =
    let graph = input |> toGraph
    let isValid path =
        path
        |> List.groupBy id
        |> List.forall (fun (node, nodeOccurrences) -> node |> isUpper || nodeOccurrences |> length = 1)

    let getSuccessors' = getSuccessors isValid graph

    explorePaths [["start"]] goalTest getSuccessors'
    |> length

let solution2 input =
    let graph = input |> toGraph
    let isValid paths =
        let nodeVisitCount = paths |> List.groupBy id |> List.map (fun (k, list) -> k, length list)
        let startCount = nodeVisitCount |> List.find (fun (n, _) -> n = "start") |> snd

        if (startCount <> 1) then
            false
        else
            let smallCaves = nodeVisitCount |> List.filter (fun (n, _) -> not <| isUpper n)
            let smallCavesVisitedTwice = smallCaves |> List.filter (fun (_, ns) -> ns = 2)
            let smallCavesVisitedMoreThanTwice = smallCaves |> List.filter (fun (_, ns) -> ns > 2)

            (smallCavesVisitedTwice.Length <= 1) && (List.isEmpty smallCavesVisitedMoreThanTwice)

    let getSuccessors' = getSuccessors isValid graph

    explorePaths [["start"]] goalTest getSuccessors'
    |> length

[<Theory>]
[<InlineData("day12_sample1.txt", 10)>]
[<InlineData("day12_sample2.txt", 19)>]
[<InlineData("day12_input.txt", 5157)>]
let ``day12 solution 1`` file expected =
    let input = InputUtils.readFile file |> Seq.toList

    let actual = solution1 input

    Assert.Equal(expected, actual)

[<Theory>]
[<InlineData("day12_sample1.txt", 36)>]
[<InlineData("day12_sample2.txt", 103)>]
// [<InlineData("day12_input.txt", 144309)>] //slow solution
let ``day12 solution 2`` file expected =
    let input = InputUtils.readFile file |> Seq.toList

    let actual = solution2 input

    Assert.Equal(expected, actual)
