namespace Medium

module Solutions =

    let FraudulentActivityNotifications expenditure d =
        let n = Array.length expenditure
        let mid = d/2
        let isDEven = d % 2 = 0
        let getTwiceMedian i =
            let ordered = expenditure
                          |> Array.skip i
                          |> Array.take d
                          |> Array.sort
            match isDEven with
                | true -> ordered.[mid - 1] + ordered.[mid]
                | false -> ordered.[mid] * 2
        let rec countExpenditures i count =
            match i with
            | i when i = n -> count
            | _ ->
                let median = getTwiceMedian (i-d)
                let isSend = expenditure.[i] >= median
                match isSend with
                    | true -> countExpenditures (i+1) (count+1)
                    | false -> countExpenditures (i+1) count
        countExpenditures d 0



