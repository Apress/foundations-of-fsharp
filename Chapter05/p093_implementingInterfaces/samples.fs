#light
type MyInterface = interface
    abstract ChangeState : myInt : int -> unit
end
type Implementation = class
    val mutable state : int
    new() = {state = 0}
    interface MyInterface with
        member x.ChangeState y = x.state <- y
    end
end

let imp = new Implementation()
let inter = imp :> MyInterface
let pintIntNewline i =
    print_int i
    print_newline()
    
let main() =
    inter.ChangeState 1
    pintIntNewline imp.state
    inter.ChangeState 2
    pintIntNewline imp.state
    inter.ChangeState 3
    pintIntNewline imp.state
    
main()
