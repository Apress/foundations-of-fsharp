#light
module FirstModule =
    let n = 1
    
module SecondModule =
    let n = 2
    module ThirdModule =
        let n = 3

let x = FirstModule.n
let y = SecondModule.n
let z = SecondModule.ThirdModule.n
