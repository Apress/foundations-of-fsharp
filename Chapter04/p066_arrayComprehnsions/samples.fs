#light
let chars = [| '1' .. '9' |]

let squares =
    [| for x in 1 .. 9
        -> x, x*x |]
        
printfn "%A" chars
printfn "%A" squares