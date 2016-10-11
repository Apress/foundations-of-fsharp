#light
open System.Diagnostics

printf "input expression: "
let input = read_line()
printf "Interpret/Compile [i/c]: "
let interpertFlag = 
    match read_line() with
    | "i" -> true
    | "c" -> false
    | _ -> failwith "not an option"        
printf "reps: "
let reps = read_int()

if interpertFlag then
    let lexbuf = Lexing.from_string input
    let e = Pars.Expression Lex.token lexbuf
    let args = Interpret.getVariableValues e
    let clock = new Stopwatch()
    clock.Start()
    for i = 1 to reps do
        Interpret.interpret e args |> ignore
    clock.Stop()
    printf "%Li" clock.ElapsedMilliseconds
else
    let lexbuf = Lexing.from_string input
    let e = Pars.Expression Lex.token lexbuf
    let paramNames = Compile.getParamList e
    let dm = Compile.createDynamicMethod e paramNames
    let args = 
        paramNames
        |> IEnumerable.map 
            (fun n -> 
                printf "%s: " n
                box (read_float()))
        |> Array.of_IEnumerable
    let clock = new Stopwatch()
    clock.Start()
    for i = 1 to reps do
        dm.Invoke(null, args)  |> ignore
    clock.Stop()
    printf "%Li" clock.ElapsedMilliseconds
read_line()
