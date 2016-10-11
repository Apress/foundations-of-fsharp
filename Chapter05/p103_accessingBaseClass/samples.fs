#light
open System.Drawing
open System.Windows.Forms
type MySquareForm(color) = class
    inherit Form() as base
    override x.OnPaint(e) =
        e.Graphics.DrawRectangle(color,
                                 10, 10,
                                 x.Width - 30,
                                 x.Height - 50)
        base.OnPaint(e)
    override x.OnResize(e) =
        x.Invalidate()
        base.OnResize(e)
end

let form = new MySquareForm(Pens.Blue)

do Application.Run(form)
