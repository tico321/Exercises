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

    /// A prime number is only evenly divisible by itself and 1.
    /// Ex: prime factors of 60 = 2 * 2 * 3 * 5
    [<Theory>]
    [<InlineData(60L, "2 2 3 5")>]
    [<InlineData(901255L, "5 17 23 461")>]
    let ``Primer factors`` (n: int64, factors: string) =
        let expected = factors.Split ' ' |> Array.map int64

        let actual = primeFactors n |> List.toArray

        Assert.Equal<int64[]>(expected, actual)
