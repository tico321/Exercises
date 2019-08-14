namespace Easy

module Solutions =

    let MinimumAbsoluteDifference arr =
        arr
        |> Array.sort
        |> Array.windowed 2
        |> Array.map (fun window -> abs <| window.[0] - window.[1])
        |> Array.min