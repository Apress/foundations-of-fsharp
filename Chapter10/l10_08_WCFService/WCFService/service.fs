#light
namespace Strangelights.Services
open System.ServiceModel

[<ServiceContract
    (Namespace = 
        "http://strangelights.com/FSharp/Foundations/WCFServices")>]
type IGreetingService = interface 
    [<OperationContract>]
    abstract Greet : name:string -> string
end

type GreetingService = class
    new() = {}
    interface IGreetingService with
        member x.Greet(name)  = "Hello: " + name
    end
end