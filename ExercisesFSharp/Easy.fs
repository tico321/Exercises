namespace Easy

open Xunit

module Tests =

    open Solutions

    [<Theory>]
    [<InlineData("3 -7 0", 3)>]
    [<InlineData("1 -3 71 68 17", 3)>]
    [<InlineData("-59 -36 -13 1 -53 -92 -2 -96 -54 75", 1)>]
    let ``Minimum Absolute Difference Scenarios``(strArr: string, expected: int) =
        let arr = strArr.Split ' ' |> Array.map (fun c -> c |> int)
        let actual = MinimumAbsoluteDifference arr
        Assert.Equal(expected, actual)

