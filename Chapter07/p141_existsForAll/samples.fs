#light
let intArray = [|0; 1; 2; 3; 4; 5; 6; 7; 8; 9|]
let existsMultipleOfTwo =
    intArray |>
    Seq.exists (fun x -> x % 2 = 0)
    
let allMultipleOfTwo =
    intArray |>
    Seq.exists (fun x -> x % 2 = 0)
    
printfn "existsMultipleOfTwo: %b" existsMultipleOfTwo
printfn "allMultipleOfTwo: %b" allMultipleOfTwo