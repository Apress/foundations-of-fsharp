#light
namespace Strangelights

type DemoClass = class
    val Z : int
    new(z) = { Z = z}
    member this.CurriedStyle x y = x + y + this.Z
    member this.TupleStyle (x, y) = x + y + this.Z
end

type IDemoInterface = interface
    abstract CurriedStyle : int -> int -> int
    abstract TupleStyle : (int * int) -> int
    abstract CSharpStyle : int * int -> int
    abstract CSharpNamedStyle : x : int * y : int -> int
end
