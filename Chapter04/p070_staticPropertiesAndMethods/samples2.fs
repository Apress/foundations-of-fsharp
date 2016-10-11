#light
open System.IO
let exists filePath = File.Exists(filePath)

let files = ["test1.txt"; "test2.txt"; "test3.txt"]

let results = List.map exists files

print_any results