#light
type Properties() = class
    let mutable rand = new System.Random()
    member x.MyProp
        with get () = rand.Next()
        and set y = rand <- new System.Random(y)
end

let prop = new Properties()

let print i = printfn "%d" i

prop.MyProp <- 12
print prop.MyProp
print prop.MyProp
print prop.MyProp