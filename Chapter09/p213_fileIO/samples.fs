#light
open System.IO
//test.csv:
//Apples,12,25
//Oranges,12,25
//Bananas,12,25
using (File.OpenText("test.csv"))
    (fun f ->
        while not f.EndOfStream do
            let line = f.ReadLine()
            let items = line.Split([|','|])
            printfn "%O %O %O"
                items.[0]
                items.[1]
                items.[2])