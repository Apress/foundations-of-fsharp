#light
namespace Strangelights.HttpHandlers
open System.Web

type SimpleHandler() = class
    interface IHttpHandler with
        member x.IsReusable = false
        member x.ProcessRequest(c : HttpContext) =
            c.Response.Write("<h1>Hello World</h1>")
    end
end