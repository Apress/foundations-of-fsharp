#light
open Microsoft.FSharp.Reflection
let printTupleValues x =
    match Value.GetInfo(x) with
    | TupleValue vals ->
    print_string "("
    vals
    |> List.iteri
        (fun i v ->
            if i <> List.length vals - 1 then
                Printf.printf " %s, " (any_to_string v)
            else
                print_any v)
    print_string " )"
    | _ -> print_string "not a tuple"
    
printTupleValues ("hello world", 1)