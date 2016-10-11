#light
type Base1 = class
    val state: int
    new(state) = { state = state }
end
type Sub1 = class
    inherit Base1
    val otherState: int
    new(state) = { inherit Base1(state) ; otherState = 0 }
end

let myOtherObject = new Sub1(1)

printfn
    "myObject.state = %i, myObject.otherState = %i"
    myOtherObject.state
    myOtherObject.otherState
