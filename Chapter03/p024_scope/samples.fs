#light
let printMessages() =
    // define message and print it
    let message = "Important"
    printfn "%s" message;
    // define an inner function that redefines value of message
    let innerFun () =
        let message = "Very Important"
        printfn "%s" message
    // define message and print it
    innerFun ()
    // finally print message again
    printfn "%s" message
    
printMessages()