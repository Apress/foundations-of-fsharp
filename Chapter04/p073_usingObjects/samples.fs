#light
open System
let findIndex f arr = Array.FindIndex(arr, new Predicate<_>(f))

let rhyme = [|"The"; "cat"; "sat"; "on"; "the"; "mat" |]

printfn "First word ending in 'at' in the array: %i"
    (rhyme |> findIndex (fun w -> w.EndsWith("at")))
