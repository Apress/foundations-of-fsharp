#light
let add (x:int) (y:int) = (# "add" x y : int #)
let sub (x:int) (y:int) = (# "sub" x y : int #)

let x = add 1 1
let y = sub 4 2

printf "x: %i y: %i" x y
read_line()