module ExercisesFSharp.AOC2021.day1.problem

(*
As the submarine drops below the surface of the ocean, it automatically performs a sonar sweep of the nearby sea floor. On a small screen, the sonar sweep report (your puzzle input) appears: each line is a measurement of the sea floor depth as the sweep looks further and further away from the submarine.
For example, suppose you had the following report:

199
200
208
210
200
207
240
269
260
263

his report indicates that, scanning outward from the submarine, the sonar sweep found depths of 199, 200, 208, 210, and so on.

The first order of business is to figure out how quickly the depth increases, just so you know what you’re dealing with – you never know if the keys will get carried into deeper water by an ocean current or a fish or something.

To do this, count the number of times a depth measurement increases from the previous measurement. (There is no measurement before the first measurement.) In the example above, the changes are as follows:

199 (N/A - no previous measurement)
200 (increased)
208 (increased)
210 (increased)
200 (decreased)
207 (increased)
240 (increased)
269 (increased)
260 (decreased)
263 (increased)
In this example, there are 7 measurements that are larger than the previous measurement.

How many measurements are larger than the previous measurement?

*)

open System
open System.Text
open Xunit
open System.IO

let getInputFromFile fileName =
    let path = Path.Combine(Environment.CurrentDirectory, "AOC2021", fileName)
    use reader = new StreamReader(path, Encoding.UTF8)
    let strContent = reader.ReadToEnd()
    strContent.Split(Environment.NewLine)
    |> Array.toSeq
    |> Seq.filter (Int32.TryParse >> fst)
    |> Seq.map int

[<Theory>]
[<InlineData("day1_sample.txt", 7)>]
[<InlineData("day1_input.txt", 1462)>]
let ``day1 submarine drops`` file expected =
    let input = getInputFromFile file

    let actual = problem_solution.day1SubmarineDrops input

    Assert.Equal(expected, actual)
