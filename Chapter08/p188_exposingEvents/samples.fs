#light
open System.Windows.Forms
type LeftClickForm() as x = class
    inherit Form()
    let trigger, event = IEvent.create()
    do x.MouseClick
        |> IEvent.filter (fun e -> e.Button = MouseButtons.Left)
        |> IEvent.listen (fun e -> trigger e)
    member x.LeftMouseClick = event
end