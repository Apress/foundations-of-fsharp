#light
type MyDelegate = delegate of int -> unit

let inst = new MyDelegate (fun i -> print_int i)
let ints = [1 ; 2 ; 3 ]

ints
|> List.iter (fun i -> inst.Invoke(i))