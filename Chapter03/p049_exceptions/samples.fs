#light
exception WrongSecond of int

let primes =
    [ 2; 3; 5; 7; 11; 13; 17; 19; 23; 29; 31; 37; 41; 43; 47; 53; 59 ]
let testSecond() =
    try
        let currentSecond = System.DateTime.Now.Second in
        if List.exists (fun x -> x = currentSecond) primes then
            failwith "A prime second"
        else
            raise (WrongSecond currentSecond)
    with
    WrongSecond x ->
        printf "The current was %i, which is not prime" x
        
testSecond()