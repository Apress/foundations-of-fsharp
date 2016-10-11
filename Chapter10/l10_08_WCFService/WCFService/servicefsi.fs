#light
#I @"C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.0";;
#r "System.ServiceModel.dll";;
open System
open System.ServiceModel
open System.Runtime.Serialization

let mutable f = (fun x -> "Hello: " + x)
f <- (fun x -> "Bonjour: " + x)
f <- (fun x -> "Goedendag: " + x)

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
        member x.Greet( name ) = f name
    end
end

let myServiceHost = 
    let baseAddress = new Uri("http://localhost:8080/service")

    let temp = new ServiceHost((type GreetingService), [|baseAddress|])

    let binding = 
        let temp =
            new WSHttpBinding(Name = "binding1", 
                              HostNameComparisonMode = 
                                HostNameComparisonMode.StrongWildcard,
                              TransactionFlow = false)
        temp.Security.Mode <- SecurityMode.Message
        temp.ReliableSession.Enabled <- false
        temp

    temp.AddServiceEndpoint((type IGreetingService), binding, baseAddress) 
    |> ignore
    temp

myServiceHost.Open()
