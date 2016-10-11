#light
type MyInt(state:int) = class
    member x.State = state
    static member ( + ) (x:MyInt, y:MyInt) : MyInt = new MyInt(x.State + y.State)
    override x.ToString() = string_of_int state
end

let x = new MyInt(1)
let y = new MyInt(1)

printfn "(x + y) = %A" (x + y)