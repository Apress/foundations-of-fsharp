#light
let x = (fun x y -> x + y) 1 2

let x1 = (function x -> function y -> x + y) 1 2
let x2 = (function (x, y) -> x + y) (1, 2)
