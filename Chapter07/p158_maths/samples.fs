#light
open System
open System.Drawing
open System.Windows.Forms
open Microsoft.FSharp.Math
open Microsoft.FSharp.Math.Notation

let cMax = complex 1.0 1.0
let cMin = complex -1.0 -1.0

let iterations = 18

let isInMandelbrotSet c0 =
    let rec check n c =
        (n = iterations)
        or (cMin < c) && (c < cMax) && check (n + 1) ((c * c) + c0)
    check 0 c0
    
let scalingFactor = 1.0 / 200.0
let offset = -1.0

let mapPlane (x, y) =
    let fx = ((float x) * scalingFactor) + offset
    let fy = ((float y) * scalingFactor) + offset
    complex fx fy
    
let form =
    let image = new Bitmap(400, 400)
    for x = 0 to image.Width - 1 do
        for y = 0 to image.Height - 1 do
            let isMember = isInMandelbrotSet ( mapPlane (x, y) )
            if isMember then
                image.SetPixel(x,y, Color.Black)
    let temp = new Form() in
    temp.Paint.Add(fun e -> e.Graphics.DrawImage(image, 0, 0))
    temp
    
[<STAThread>]
do Application.Run(form)
