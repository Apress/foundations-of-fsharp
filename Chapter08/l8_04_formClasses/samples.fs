#light
open System
open System.Windows.Forms

type MyForm() as x = class
    inherit Form(Width=174, Height=64)
    let label = new Label(Top=8, Left=8, Width=40, Text="Input:")
    let textbox = new TextBox(Top=8, Left=48, Width=40)
    let button = new Button(Top=8, Left=96, Width=60, Text="Push Me!")
    do button.Click.Add
        (fun _ ->
        let form = new MyForm(Text=textbox.Text)
        form.Show())
    do x.Controls.Add(label)
    do x.Controls.Add(textbox)
    do x.Controls.Add(button)
    member x.Textbox = textbox
end

let form =
    let temp = new MyForm(Text="My Form")
    temp.Textbox.Text <- "Next!"
    temp
    
[<STAThread>]
do Application.Run(form)
