#light
let inc, dec, show =
    let n = ref 0
    let inc () =
        n := !n + 1
    let dec () =
        n := !n - 1
    let show () =
        print_int !n
    inc, dec, show
    
inc()
inc()
dec()
show()