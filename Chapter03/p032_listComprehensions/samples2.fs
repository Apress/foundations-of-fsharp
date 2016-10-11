#light
let evens n =
    { for x in 1 .. n when x % 2 = 0 -> x }
    
print_any (evens 10)