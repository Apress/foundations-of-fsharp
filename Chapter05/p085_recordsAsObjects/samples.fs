#light
open System.Drawing
type Shape =
    { reposition: Point -> unit;
      draw : unit -> unit }

let makeShape initPos draw =
    let currPos = ref initPos
    { reposition = (fun newPos -> currPos := newPos);
    draw = (fun () -> draw !currPos); }

let circle initPos =
    makeShape initPos (fun pos ->
        printfn
            "Circle, with x = %i and y = %i"
            pos.X
            pos.Y)
let square initPos =
    makeShape initPos (fun pos ->
        printfn
            "Square, with x = %i and y = %i"
            pos.X
            pos.Y)
let point (x,y) = new Point(x,y)

let shapes =
    [ circle (point (10,10));
    square (point (30,30)) ]
let moveShapes() =
    shapes |> List.iter (fun s -> s.draw())
let main() =
    moveShapes()
    shapes |> List.iter (fun s -> s.reposition (point (40,40)))
    moveShapes()
    
main()
