#light
open System

let add x y = x + y
let div x y = x / y

open NUnit.Framework

[<TestFixture>]
type TestCases = class
    new() = {}
    [<Test>]
    member x.TestAdd01() = 
        Assert.AreEqual(3, add 1 2)
    [<Test>]
    member x.TestAdd02() = 
        Assert.AreEqual(4, add 2 2)
    [<Test>]
    member x.TestDiv01() = 
        Assert.AreEqual(1, div 2 2)
    [<Test; ExpectedException(type DivideByZeroException)>]
    member x.TestDiv02() = 
        div 1 0 |> ignore
end