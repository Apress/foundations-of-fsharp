#light
open Microsoft.FSharp.Quotations.Typed

let quotedAnonFun = « fun x -> x + 1 »

printf "%A\r\n" quotedAnonFun