#light
open System
open System.Text
open System.IO

// test.txt: the,cat,sat,on,the,mat
let opener() = File.OpenText("test.txt")
let generator (stream : StreamReader) =
    let endStream = ref false
    let rec generatorInner chars =
        match stream.Read() with
        | -1 ->
        endStream := true
        chars
        | x ->
            match Convert.ToChar(x) with
            | ',' -> chars
            | c -> generatorInner (c :: chars)
    let chars = generatorInner []
    if List.length chars = 0 && !endStream then
        None
    else
        Some(new string(List.to_array (List.rev chars)))
        
let closer (stream : StreamReader) =
    stream.Dispose()
    
let wordList =
    Seq.generate
        opener
        generator
        closer
        
wordList |> Seq.iter (fun s -> print_endline s)