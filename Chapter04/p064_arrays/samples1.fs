#light
let jagged = [| [| "one" |] ; [| "two" ; "three" |] |]

let singleDim = jagged.[0]
let itemOne = singleDim.[0]
let itemTwo = jagged.[1].[0]

printfn "%s %s" itemOne itemTwo