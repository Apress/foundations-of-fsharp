#light
let mutableY() =
    let mutable y = "One"
    printfn "Mutating:\r\nx = %s" y
    let f() =
        y <- "Two"
        printfn "x = %s" y
    f()
    printfn "x = %s" y