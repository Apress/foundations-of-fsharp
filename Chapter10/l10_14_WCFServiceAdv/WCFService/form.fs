#light
open System
open System.IO
open System.Drawing 
open System.ServiceModel
open System.Windows.Forms

let myServiceHost = 
    let baseAddress = new Uri("http://localhost:8080/service")

    let temp = new ServiceHost((type Service.ImageService), [|baseAddress|])

    let binding = 
        let temp = new WSHttpBinding()
        temp.Name <- "binding1"
        temp.HostNameComparisonMode <- 
            HostNameComparisonMode.StrongWildcard
        temp.Security.Mode <- SecurityMode.Message
        temp.ReliableSession.Enabled <- false
        temp.TransactionFlow <- false
        temp

    temp.AddServiceEndpoint((type Service.IImageService), binding, baseAddress) 
    |> ignore
    temp

myServiceHost.Open()

let form = new Form()

Service.newImgEvent.Add(fun img -> form.BackgroundImage <- img)
    
[<STAThread>]
do Application.Run(form)