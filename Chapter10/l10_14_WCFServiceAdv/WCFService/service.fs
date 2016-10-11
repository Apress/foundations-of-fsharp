#light
open System
open System.IO
open System.Drawing 
open System.ServiceModel
open System.Windows.Forms

[<ServiceContract
    (Namespace = 
        "http://strangelights.com/FSharp/Foundations/WCFImageService")>]
type IImageService = interface 
    [<OperationContract>]
    abstract ReceiveImage : image:array<Byte> -> unit
end

let newImgTrigger, (newImgEvent: Idioms.IEvent<Bitmap>) =
        IEvent.create()

type ImageService = class
    new() = {}
    
    interface IImageService with
        member x.ReceiveImage( image ) =
            let memStream = new MemoryStream(image) 
            let bitmap = new Bitmap(memStream)
            newImgTrigger bitmap
    end
end

let myServiceHost = 
    let baseAddress = new Uri("http://localhost:8080/service")

    let temp = new ServiceHost((type ImageService), [|baseAddress|])

    let binding = 
        let temp = new WSHttpBinding()
        temp.Name <- "binding1"
        temp.HostNameComparisonMode <- 
            HostNameComparisonMode.StrongWildcard
        temp.Security.Mode <- SecurityMode.Message
        temp.ReliableSession.Enabled <- false
        temp.TransactionFlow <- false
        temp

    temp.AddServiceEndpoint((type IImageService), binding, baseAddress) 
    |> ignore
    temp

myServiceHost.Open()

let form = new Form()

newImgEvent.Add(fun img -> form.BackgroundImage <- img)
    
[<STAThread>]
do Application.Run(form)