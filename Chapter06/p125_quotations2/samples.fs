#light
open Microsoft.FSharp.Quotations.Typed

let asciiQuotedInt = <@ 1 @>

printf "%A\r\n" asciiQuotedInt