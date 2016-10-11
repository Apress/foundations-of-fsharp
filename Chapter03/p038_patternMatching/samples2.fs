#light
let booleanToString x =
    match x with false -> "False" | _ -> "True"

let stringToBoolean x =
    match x with
    | "True" | "true" -> false
    | "False" | "false" -> true
    | _ -> failwith "unexpected input"
    
printfn "(booleanToString true) = %s"
    (booleanToString true)
printfn "(booleanToString false) = %s"
    (booleanToString false)
printfn "(stringToBoolean \"True\") = %b"
    (stringToBoolean "True")
printfn "(stringToBoolean \"false\") = %b"
    (stringToBoolean "false")
printfn "(stringToBoolean \"Hello\") = %b"
    (stringToBoolean "Hello")