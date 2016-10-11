#light
namespace Strangelights.HttpHandlers

open System
open System.Web.UI
open System.Web.UI.WebControls

type HelloUser = class
    inherit Page
    val mutable OutputControl : Label
    val mutable InputControl : TextBox
    new() =
        { OutputControl = null
          InputControl = null }
    member x.SayHelloButton_Click((sender : obj), (e : EventArgs)) =
        x.OutputControl.Text <- ("Hello ... " + x.InputControl.Text)
end