#light
let rec findSequence l =
    match l with
    | [x; y; z] ->
        printfn "Last 3 numbers in the list were %i %i %i"
            x y z
    | 1 :: 2 :: 3 :: tail ->
        printfn "Found sequence 1, 2, 3 within the list"
        findSequence tail
    | head :: tail -> findSequence tail
    | [] -> ()
    
let testSequence = [1; 2; 3; 4; 5; 6; 7; 8; 9; 8; 7; 6; 5; 4; 3; 2; 1]

findSequence testSequence