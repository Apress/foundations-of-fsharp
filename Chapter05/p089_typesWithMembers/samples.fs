#light
type Point =
    { mutable top : int ;
      mutable left : int }
    with
        member x.Swap() =
            let temp = x.top
            x.top <- x.left
            x.left <- temp
    end

let printAnyNewline x =
    print_any x
    print_newline()
    
let myPoint = 
    {top = 3;
     left = 7;}
     
let main() =
    printAnyNewline myPoint
    myPoint.Swap()
    printAnyNewline myPoint
main()