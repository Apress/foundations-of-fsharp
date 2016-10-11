#light
open System.Diagnostics

printf "input expression: "
let input = read_line()
printf "Interpret/Compile/Compile Through Delegate [i/c/cd]: "
let interpertFlag = read_line()
printf "reps: "
let reps = read_int()

type Df0 = delegate of unit -> float
type Df1 = delegate of float -> float
type Df2 = delegate of float * float -> float
type Df3 = delegate of float * float * float -> float
type Df4 = delegate of float * float * float * float -> float

match interpertFlag with
| "i" ->
    let lexbuf = Lexing.from_string input
    let e = Pars.Expression Lex.token lexbuf
    let args = Interpret.getVariableValues e
    let clock = new Stopwatch()
    clock.Start()
    for i = 1 to reps do
        Interpret.interpret e args |> ignore
    clock.Stop()
    printf "%Li" clock.ElapsedTicks
| "c" ->
    let lexbuf = Lexing.from_string input
    let e = Pars.Expression Lex.token lexbuf
    let paramNames = Compile.getParamList e
    let dm = Compile.createDynamicMethod e paramNames
    let args = Compile.collectArgs paramNames
    let clock = new Stopwatch()
    clock.Start()
    for i = 1 to reps do
        dm.Invoke(null, args) |> ignore
    clock.Stop()
    printf "%Li" clock.ElapsedTicks
| "cd" ->
    let lexbuf = Lexing.from_string input
    let e = Pars.Expression Lex.token lexbuf
    let paramNames = Compile.getParamList e
    let dm = Compile.createDynamicMethod e paramNames
    let args = Compile.collectArgs paramNames
    let args = args |> Array.map (fun f -> f :?> float)
    let d =
        match args.Length with
        | 0 -> dm.CreateDelegate(type Df0)
        | 1 -> dm.CreateDelegate(type Df1)
        | 2 -> dm.CreateDelegate(type Df2)
        | 3 -> dm.CreateDelegate(type Df3)
        | 4 -> dm.CreateDelegate(type Df4)
        | _ -> failwith "too many parameters"
    let clock = new Stopwatch()
    clock.Start()
    for i = 1 to reps do
        match d with
        | :? Df0 as d -> d.Invoke() |> ignore
        | :? Df1 as d -> d.Invoke(args.(0)) |> ignore
        | :? Df2 as d -> d.Invoke(args.(0), args.(1)) |> ignore
        | :? Df3 as d -> d.Invoke(args.(0), args.(1), args.(2)) |> ignore
        | :? Df4 as d -> d.Invoke(args.(0), args.(1), args.(2), args.(4)) |> ignore
        | _ -> failwith "too many parameters"
    clock.Stop()
    printf "%Li" clock.ElapsedTicks
| _ -> failwith "not an option"        

//let x  = Microsoft.C
