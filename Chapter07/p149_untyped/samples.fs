#light
open System.Collections
open System.Collections.Generic

let floatArrayList =
    let temp = new ArrayList()
    temp.AddRange([| 1.0; 2.0; 3.0 |])
    temp
    
let (typedIntList : seq<float>) =
    Seq.untyped_to_typed floatArrayList