#light
let ones = Array.create 1 3
let twos = ones |> Array.map (fun x -> x + 1)

#light
open System.IO
open Microsoft.FSharp.Compatibility
let paths = Directory.GetFiles(@"c:\")
let files = paths |> Array.map (fun path -> new FileInfo(path))

