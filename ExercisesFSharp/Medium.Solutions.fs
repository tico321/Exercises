namespace Medium

module Solutions =

    let FraudulentActivityNotifications expenditures d =
        Array.windowed (d + 1) expenditures
        |> Array.filter (fun a -> float a.[d] >= (Array.sumBy float a.[0..d-1]) * 2.0 / float d)
        |> Array.length
