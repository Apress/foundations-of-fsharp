#light
type AbstractProperties() = class
    abstract MyProp : int
        with get, set
end

type ConcreteProperties() = class
    inherit AbstractProperties()
    let mutable rand = new System.Random()
    override x.MyProp
        with get() = rand.Next()
        and set(y) = rand <- new System.Random(y)
end