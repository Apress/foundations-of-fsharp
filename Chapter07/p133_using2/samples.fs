#light
open System.IO

let text =
    using (File.OpenText("test.txt")) (fun streamReader ->
        streamReader.ReadLine()
    )