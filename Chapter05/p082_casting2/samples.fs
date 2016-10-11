#light
open System.Windows.Forms

let myControls =
    [| (new Button() :> Control);
       (new TextBox() :> Control);
       (new Label() :> Control) |]