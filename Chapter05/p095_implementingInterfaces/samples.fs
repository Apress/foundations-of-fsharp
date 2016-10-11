#light
type MyInterface = interface
    abstract ChangeState : int -> unit
end

type Implementation = class
    val mutable state : int
    new() = {state = 0}
    interface MyInterface with
        member x.ChangeState y = x.state <- y
    member x.ChangeState y = x.state <- y
end

let imp = new Implementation()

let pintIntNewline i =
    print_int i
    print_newline()

let main() =
    imp.ChangeState 1
    pintIntNewline imp.state
    imp.ChangeState 2
    pintIntNewline imp.state
    
main()
