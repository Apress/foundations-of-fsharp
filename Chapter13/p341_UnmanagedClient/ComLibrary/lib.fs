namespace Strangelights
open System
open System.Runtime.InteropServices


[<Guid("6180B9DF-2BA7-4a9f-8B67-AD43D4EE0563")>]
type IMath = interface 
    abstract Add : x: int * y: int -> int
    abstract Sub : x: int * y: int -> int
end


[<Guid("B040B134-734B-4a57-8B46-9090B41F0D62");
ClassInterface(ClassInterfaceType.None)>]
type Math = class
    new () = {}
    interface IMath with
        member this.Add(x, y) = x + y
        member this.Sub(x, y) = x - y
    end
end    