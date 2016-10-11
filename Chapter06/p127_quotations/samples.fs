#light
open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.Typed

let interpretInt exp =
    let uexp = to_raw exp in
        match uexp with
        | Raw.Int32 x -> printfn "%d" x
        | _ -> printfn "not an int"
        
interpretInt « 1 »
interpretInt « 1 + 1 »