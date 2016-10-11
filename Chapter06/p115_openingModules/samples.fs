#light
open System.IO
open Microsoft.FSharp.Core.Operators

using (File.AppendText("text.txt") ) (fun stream ->
    stream.WriteLine("Hello World"))