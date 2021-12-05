module ExercisesFSharp.AOC2021.InputUtils

open System
open System.IO
open System.Text

let readFile fileName =
    let path = Path.Combine(Environment.CurrentDirectory, "AOC2021", fileName)
    File.ReadAllLines(path, Encoding.UTF8)
    |> Array.toSeq

let strToInt (str: string) =
    match Int32.TryParse str with
    | true, n -> Some n
    | _ -> None

let splitLineToInts (separator: string) (line: string) =
    line.Split(separator)
    |> Array.toSeq
    |> Seq.map strToInt
    |> Seq.choose id
