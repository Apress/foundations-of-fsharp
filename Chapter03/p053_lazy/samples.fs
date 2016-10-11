#light
let fibs =
    Seq.unfold
        (fun (n0, n1) ->
            Some(n0, (n1, n0 + n1)))
        (1I,1I)
        
let first20 = Seq.take 20 fibs
print_any first20