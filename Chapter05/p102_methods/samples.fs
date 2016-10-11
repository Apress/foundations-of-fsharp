#light
type Base = class
    val mutable state: int
    new() = { state = 0 }
    member x.JiggleState y = x.state <- y
    abstract WiggleState: int -> unit
    default x.WiggleState y = x.state <- y + x.state
end

type Sub = class
    inherit Base
    new() = {}
    default x.WiggleState y = x.state <- y &&& x.state
end

let myBase = new Base()
let mySub = new Sub()

let testBehavior (c : #Base) =
    c.JiggleState 1
    print_int c.state
    print_newline()
    c.WiggleState 3
    print_int c.state
    print_newline()
    
print_endline "base class: "
testBehavior myBase
print_endline "sub class: "
testBehavior mySub