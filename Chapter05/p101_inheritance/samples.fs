#light
type Base1(state) = class
    member x.State = state
end

type Sub1(state) = class
    inherit Base1(state)
    member x.OtherState = state
end

let myOtherObject = new Sub1(1)

printfn
    "myObject.state = %i, myObject.otherState = %i"
    myOtherObject.State
    myOtherObject.OtherState