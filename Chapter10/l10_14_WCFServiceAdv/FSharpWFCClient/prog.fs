#light
open System
open System.IO
open System.Windows.Forms

let form =
    let temp = new Form(Width=272, Height=64)

    let imagePath = new TextBox(Top=8, Left=8, Width=128)

    let browse = new Button(Top=8, Width=32, Left=8+imagePath.Right, Text = "...")
    browse.Click.Add(fun _ ->
        let dialog = new OpenFileDialog()
        if dialog.ShowDialog() = DialogResult.OK then
            imagePath.Text <- dialog.FileName)

    let send = new Button(Top=8, Left=8+browse.Right, Text = "Send")
    send.Click.Add(fun _ ->
        let buffer = 
            using (File.OpenRead(imagePath.Text)) (fun file ->
                let length = Convert.ToInt32(file.Length)
                let buffer = Array.zero_create length 
                file.Read(buffer, 0, length) |> ignore
                buffer)
        let service = new ImageServiceClient()
        service.ReceiveImage(buffer))

    temp.Controls.Add(imagePath)
    temp.Controls.Add(browse)
    temp.Controls.Add(send)
    temp

[<STAThread>]
do Application.Run(form)