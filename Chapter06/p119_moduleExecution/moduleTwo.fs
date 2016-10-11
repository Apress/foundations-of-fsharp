#light
module ModuleTwo
print_endline "This is the first line"
print_endline "This is the second"

let funct() =
    Printf.printf "%i" ModuleOne.n
    
funct()
