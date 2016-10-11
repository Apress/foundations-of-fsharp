#light
#r "C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.0\System.ServiceModel.dll";;
#r "WCFInterface.dll";;
open System
open System.ServiceModel
open System.Runtime.Serialization

let mutable f = (fun x -> "Hello: " + x)
f <- (fun x -> "Good bye: " + x)
f <- (fun x -> "Suck ass: " + x)

type AService = class
    new() = {}
    interface IService with
        member x.Hello( name ) = f name
    end
end

let myServiceHost = 
    let baseAddress = new Uri("http://localhost:8080/service")

    let temp = new ServiceHost((type AService), [|baseAddress|])

    let binding = 
        let temp = new WSHttpBinding()
        temp.Name <- "binding1"
        temp.HostNameComparisonMode <- 
            HostNameComparisonMode.StrongWildcard
        temp.Security.Mode <- SecurityMode.Message
        temp.ReliableSession.Enabled <- false
        temp.TransactionFlow <- false
        temp

    temp.AddServiceEndpoint((type IService), binding, baseAddress) |> ignore;
    temp

myServiceHost.Open()
