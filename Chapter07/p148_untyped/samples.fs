#light
open System.Collections

let arrayList =
    let temp = new ArrayList()
    temp.AddRange([| 1; 2; 3 |])
    temp
    
let doubledArrayList =
    arrayList |>
    Seq.untyped_map (fun x -> x * 2)
    
doubledArrayList |> Seq.untyped_iter (fun x -> printf "%i ... " x)