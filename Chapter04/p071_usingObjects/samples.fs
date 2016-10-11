#light
open System.IO
let file = new FileInfo("test.txt")

if not file.Exists then
    using (file.CreateText()) (fun stream ->
    stream.WriteLine("hello world"))
    file.Attributes <- FileAttributes.ReadOnly
    
print_endline file.FullName