#light
let evalA i j = 1.0 / float((i+j)*(i+j+1)/2+i+1)

let evalATimesU u v =
    let n = Array.length v - 1
    for i = 0 to  n do
        v.(i) <- 0.0
        for j = 0 to n do 
            v.(i) <- v.(i) + evalA i j * u.(j)

let evalAtTimesU u v =
    let n = Array.length v -1 in
    for i = 0 to n do
        v.(i) <- 0.0
        for j = 0 to n do 
            v.(i) <- v.(i) + evalA j i * u.(j)

let evalAtATimesU u v =
    let w = Array.create (Array.length u) 0.0
    evalATimesU u w
    evalAtTimesU w v


let main() =
    let n = 
        try 
            int_of_string(Array.get Sys.argv 1) 
        with _ ->  2000
    let u = Array.create n 1.0
    let v = Array.create n 0.0
    for i = 0 to 9 do
        evalAtATimesU u v
        evalAtATimesU v u

    let vv = ref 0.0
    let vBv = ref 0.0
    for i=0 to n-1 do
        vv := !vv + v.(i) * v.(i)
        vBv := !vBv + u.(i) * v.(i)

    Printf.printf "%0.9f\n" (sqrt(!vBv / !vv))

main()