#light
let totalArray () =
    let a = [| 1; 2; 3 |]
    let x = ref 0
    for n in a do
        x := !x + n
    print_int !x
    print_newline()
    
totalArray()