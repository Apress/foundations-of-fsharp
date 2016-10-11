#light
open System.Windows.Forms

let myControls =
    [| (new Button() :> Control);
       (new TextBox() :> Control);
       (new Label() :> Control) |]
       
let uc (c : #Control) = c :> Control

let myConciseControls =
    [| uc (new Button()); uc (new TextBox()); uc (new Label()) |]