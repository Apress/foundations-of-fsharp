#light
let lexbuf = Lexing.from_string "1"
let token = Lex.token lexbuf
print_any token
read_line()

let lexbuf2 = Lexing.from_string "(1 * 1) + 2"
while not lexbuf2.IsPastEndOfStream do
    let token = Lex.token lexbuf2
    printf "%s\r\n" (any_to_string token)
read_line()

#light
let lexbuf3 = Lexing.from_string "(1 * 1) + 2"
let e = Pars.Expression Lex.token lexbuf3
print_any e