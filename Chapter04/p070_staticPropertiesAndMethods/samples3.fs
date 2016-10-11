#light
open System.IO

let file = File.Open(path = "test.txt",
                        mode = FileMode.Append,
                        access = FileAccess.Write,
                        share = FileShare.None)

file.Close()