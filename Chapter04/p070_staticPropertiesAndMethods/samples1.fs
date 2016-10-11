#light
open System.IO
if File.Exists("test.txt") then
    print_endline "Text file \"test.txt\" is present"
else
    print_endline "Text file \"test.txt\" does not exist"