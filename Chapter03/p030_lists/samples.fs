#light
let listOfList = [[2; 3; 5]; [7; 11; 13]; [17; 19; 23; 29]]

let rec concatList l =
    if List.nonempty l then
        let head = List.hd l in
        let tail = List.tl l in
        head @ (concatList tail)
    else
        []

let primes = concatList listOfList

print_any primes