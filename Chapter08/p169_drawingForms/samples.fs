#light
open System.Drawing
open System.Windows.Forms
let brush = new SolidBrush(Color.Red)
let form =
    let temp = new Form()
    temp.Resize.Add(fun _ -> temp.Invalidate())
    temp.Paint.Add
        (fun e ->
            if temp.Width - 64 > 0 && temp.Height - 96 > 0 then
                e.Graphics.FillPie
                    (brush,
                        32,
                        32,
                        temp.Width - 64,
                        temp.Height - 64,
                        0,
                        290))
    temp
Application.Run(form)
