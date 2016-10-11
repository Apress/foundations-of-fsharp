#light
let decayPattern =
    Seq.unfold
        (fun x ->
            let limit = 0.01
            let n = x - (x / 2.0)
            if n > limit then
                Some(x, n)
            else
                None)
        (10.0)

decayPattern |> Seq.iter (fun x -> printf "%f ... " x)