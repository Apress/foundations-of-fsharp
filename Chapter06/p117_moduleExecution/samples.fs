#light
module ModuleOne
print_endline "This is the first line"
print_endline "This is the second"

let file =
    let temp = new System.IO.FileInfo("test.txt") in
    Printf.printf "File exists: %b\r\n" temp.Exists;
    temp