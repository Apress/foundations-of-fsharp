#light
open System.Collections.Generic
let stringList =
    let temp = new ResizeArray<string>() in
    temp.AddRange([| "one" ; "two" ; "three" |]);
    temp
    
let itemOne = stringList.Item(0)
let itemTwo = stringList.[1]

printfn "%s %s" itemOne itemTwo