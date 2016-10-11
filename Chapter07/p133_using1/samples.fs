#light
open System.IO

using (File.AppendText("test.txt"))
    (fun streamWriter ->
        streamWriter.WriteLine("A safe way to write to a file"))