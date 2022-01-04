namespace Easy

module Solutions =

    let MinimumAbsoluteDifference arr =
        arr
        |> Array.sort
        |> Array.windowed 2
        |> Array.map (fun window -> abs <| window.[0] - window.[1])
        |> Array.min

    let rec primeFactors (number: int64) =
        let rec loop i n =
            match n with
            | 1L -> []
            | n when n % i = 0L -> i :: (loop i (n/i))
            | _ -> loop (i + 1L) n
        loop 2L number

    let fibonacciPos pos =
        Seq.unfold (fun (a,b) -> Some (a, (b, a+b))) (0, 1)
        |> Seq.take pos
        |> Seq.toList
        |> (fun s -> s |> List.last)
