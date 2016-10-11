#light
open System
open System.Drawing
open System.Windows.Forms

let form =
    let temp = new Form(Text = "Scribble !!")
    
    let pointsMasterList = ref []
    let pointsTempList = ref []
    let mouseDown = ref false
    let pen = ref (new Pen(Color.Black))
    
    temp.MouseDown.Add(fun _ -> mouseDown := true)
    
    let leftMouse, rightMouse =
        temp.MouseDown
        |> IEvent.partition (fun e -> e.Button = MouseButtons.Left)
        
    leftMouse.Add(fun _ -> pen := new Pen(Color.Black))
    rightMouse.Add(fun _ -> pen := new Pen(Color.Red))
    
    temp.MouseUp
    |> IEvent.listen
        (fun _ ->
            mouseDown := false
            if List.length !pointsTempList > 1 then
                let points = List.to_array !pointsTempList
                pointsMasterList :=
                    (!pen, points) :: !pointsMasterList
            pointsTempList := []
            temp.Invalidate())
            
    temp.MouseMove
    |> IEvent.filter(fun _ -> !mouseDown)
    |> IEvent.listen
        (fun e ->
            pointsTempList := e.Location :: !pointsTempList
            temp.Invalidate())
            
    temp.Paint
    |> IEvent.listen
        (fun e ->
            if List.length !pointsTempList > 1 then
                e.Graphics.DrawLines
                    (!pen, List.to_array !pointsTempList)
            !pointsMasterList
            |> List.iter
                (fun (pen, points) ->
                    e.Graphics.DrawLines(pen, points)))
    temp

[<STAThread>]
do Application.Run(form)
