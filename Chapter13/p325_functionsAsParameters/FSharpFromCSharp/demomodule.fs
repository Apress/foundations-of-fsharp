#light
module Strangelights.DemoModule

open System
open System.Collections.Generic

let filterStringList f (l : List<string>) = l |> Seq.filter f

let filterStringListDelegate 
    (del : Predicate<string>) 
    (l : List<string>) = 
        let f x = del.Invoke(x)
        new List<string>(l |> Seq.filter f)

