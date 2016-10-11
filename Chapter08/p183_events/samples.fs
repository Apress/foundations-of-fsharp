#light
open System.Windows.Forms
let form =
    let temp = new Form()
    temp.MouseClick
    |> IEvent.filter (fun e -> e.Button = MouseButtons.Left)
    |> IEvent.listen
        (fun _ ->
            MessageBox.Show("Left button") |> ignore)
    temp
    
Application.Run(form)