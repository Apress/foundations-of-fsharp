#light
open Microsoft.FSharp.Reflection

let printTupleTypes x =
    match Value.GetTypeInfo(x) with
    | TupleType types ->
        print_string "("
        types
        |> List.iteri
            (fun i t ->
            if i <> List.length types - 1 then
                Printf.printf " %s * " t.Name
            else
                print_string t.Name)
        print_string " )"
    | _ -> print_string "not a tuple"
    
printTupleTypes ("hello world", 1)