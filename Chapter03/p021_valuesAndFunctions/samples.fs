#light

let halfWay a b =
    let dif = b - a
    let mid = dif / 2
    mid + a
    
printfn "(halfWay 5 11) = %i" (halfWay 5 11)
printfn "(halfWay 11 5) = %i" (halfWay 11 5)