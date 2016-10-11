#light
let lazyList =
    LazyList.unfold
        (fun x ->
            if x < 13 then
                Some(x, x + 1)
            else
                None)
        10
        
let rec display l =
    match l with
    | LazyList.Cons(h,t) ->
        print_int h
        print_newline ()
        display t
    | LazyList.Nil ->
        ()
        
display lazyList