#light
namespace Strangelights
open System.Collections.Generic

type TheClass = class
    val mutable TheField : int
    new(i) = { TheField = i }
    member x.Increment() = 
        x.TheField <- x.TheField + 1
    member x.Decrement() = 
        x.TheField <- x.TheField - 1
end

module TheModule = begin
    let incList (l : List<TheClass>) = 
        l |> Seq.iter (fun c -> c.Increment())
    let decList (l : List<TheClass>) = 
        l |> Seq.iter (fun c -> c.Decrement())
end