#light
namespace Strangelights.WebServices

open System.Web.Services

[<WebService(Namespace = 
    "http://strangelights.com/FSharp/Foundations/WebServices")>]
type Service = class
    inherit WebService
    new() = {}
    [<WebMethod(Description = "Perfoms integer addition")>]
    member x.Addition ((x : int), (y : int)) = x + y
end