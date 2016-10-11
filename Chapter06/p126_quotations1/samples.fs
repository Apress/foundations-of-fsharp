#light
open Microsoft.FSharp.Quotations.Typed

let inc x = x + 1
let quotedFun = « inc 1 »

printf "%A\r\n" quotedFun