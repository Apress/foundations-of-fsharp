#light
type Base = class
    val state: int
    new() = { state = 0 }
    end
    
type Sub = class
    inherit Base
    val otherState: int
    new() = { otherState = 0 }
end

let myObject = new Sub()

printfn
    "myObject.state = %i, myObject.otherState = %i"
    myObject.state
    myObject.otherState