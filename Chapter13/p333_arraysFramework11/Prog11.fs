#light
open Microsoft.FSharp.Compatibility
let ones = CompatArray.create 1 3
let twos = ones |> CompatArray.map (fun x -> x + 1)


#light
open System.IO
open Microsoft.FSharp.Compatibility
let paths = Directory.GetFiles(@"c:\")
let files = paths |> CompatArray.map (fun path -> new FileInfo(path))
