#light
let simpleList = [ box 1; box 2.0; box "three" ]
let recognizeType (item : obj) =
    match item with
    | :? System.Int32 -> print_endline "An integer"
    | :? System.Double -> print_endline "A double"
    | :? System.String -> print_endline "A string"
    | _ -> print_endline "Unknown type"

List.iter recognizeType simpleList