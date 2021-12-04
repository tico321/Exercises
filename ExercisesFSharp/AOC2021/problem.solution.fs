module ExercisesFSharp.AOC2021.day1.problem_solution

let day1SubmarineDrops input =
    input
    |> Seq.windowed 2
    |> Seq.fold
        (fun acc window ->
        match window with
        | [| a; b |] -> if b > a then (acc + 1) else acc
        | _ -> acc)
        0
