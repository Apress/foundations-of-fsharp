#light
open System
open System.Drawing
open System.Windows.Forms
type Shape =
    { reposition: Point -> unit;
      draw : Graphics -> unit }
let movingShape initPos draw =
    let currPos = ref initPos in
    { reposition = (fun newPos -> currPos := newPos);
    draw = (fun g -> draw !currPos g); }
    
let movingCircle initPos diam =
    movingShape initPos (fun pos g ->
        g.DrawEllipse(Pens.Blue,pos.X,pos.Y,diam,diam))
        
let movingSquare initPos size =
    movingShape initPos (fun pos g ->
    g.DrawRectangle(Pens.Blue,pos.X,pos.Y,size,size) )
    
let fixedShape draw =
    { reposition = (fun newPos -> ());
    draw = (fun g -> draw g); }
    
let fixedCircle (pos:Point) (diam:int) =
    fixedShape (fun g -> g.DrawEllipse(Pens.Blue,pos.X,pos.Y,diam,diam))
    
let fixedSquare (pos:Point) (size:int) =
    fixedShape (fun g -> g.DrawRectangle(Pens.Blue,pos.X,pos.Y,size,size))
    
let point (x,y) = new Point(x,y)

let shapes =
    [ movingCircle (point (10,10)) 20;
    movingSquare (point (30,30)) 20;
    fixedCircle (point (20,20)) 20;
    fixedSquare (point (40,40)) 20; ]

let mainForm =
    let form = new Form()
    let rand = new Random()
    form.Paint.Add(fun e ->
        shapes |> List.iter (fun s ->
        s.draw e.Graphics)
        )
    form.Click.Add(fun e ->
        shapes |> List.iter (fun s ->
        s.reposition(new Point(rand.Next(form.Width),
                               rand.Next(form.Height)));
        form.Invalidate())
        )
    form

[<STAThread>]
do Application.Run(mainForm)
