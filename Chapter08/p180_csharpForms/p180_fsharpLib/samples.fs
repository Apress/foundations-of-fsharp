#light
module Strangelights.Fibonacci

let fibs =
    (1,1) |> Seq.unfold
        (fun (n0, n1) ->
            Some(n0, (n1, n0 + n1)))
            
let get n =
    Seq.nth n fibs