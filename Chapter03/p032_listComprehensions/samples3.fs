#light
let squarePoints n =
    { for x in 1 .. n
        for y in 1 .. n -> x,y }
print_any (squarePoints 3)