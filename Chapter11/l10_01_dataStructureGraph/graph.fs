#light
open System
open System.Drawing
open System.Drawing.Drawing2D
open System.Windows.Forms
open Microsoft.FSharp.Math

type Point = { X : float; Y : float }

type LineDefinition =
|   Points of Point array
|   Function of (float -> float)
|   Combination of (float * LineDefinition) list

// Derived construction function
let PointList pts = 
    Points(pts |> Array.of_list |> Array.map (fun (x,y) -> {X=x;Y=y}))

module LineFunctions = begin

    // Helper function to take a list of sequences and return a sequence of lists
    // where the sequences are iterated in lockstep.
    let combinel (seqs : list< #seq<'a> >) : seq< list<'a> > = 
        Seq.generate 
           (fun () -> seqs |> List.map (fun s -> s.GetEnumerator()) )
           (fun ies -> 
               let more = ies |> List.for_all (fun ie -> ie.MoveNext()) 
               if more then Some(ies |> List.map (fun ie -> ie.Current))
               else None)
           (fun ies -> ies |> List.iter (fun ie -> ie.Dispose()))

    // Interoplate the given points to find a Y value for the given X
    let interpolate pts x =
         let best p z = Array.fold_right (fun x y -> if p x y then x else y) pts z
         let l = best (fun p1 p2 -> p1.X > p2.X && p1.X <= x) pts.[0] 
         let r = best (fun p1 p2 -> p1.X < p2.X && p1.X >= x) pts.[pts.Length-1] 
         let y = (if l.X = r.X then (l.Y+r.Y)/2.0 
                  else l.Y + (r.Y-l.Y)*(x-l.X)/(r.X-l.X))
         { X=x; Y=y }

    // Sample the line at the given sequence of X values
    let rec sample xs line  = 
        match line with
        | Points(pts) -> 
             { for x in xs -> interpolate pts x }

        | Function(f) -> 
             { for x in xs -> {X=x;Y=f x} }

        | Combination wlines -> 
             let weights = wlines |> List.map fst |> Vector.of_list 
             // Sample each of the lines
             let ptsl    = wlines |> List.map snd |> List.map (sample xs) 
             // Extract the vector for each sample and combine by weight
             let ys = ptsl |> List.map (Seq.map (fun p -> p.Y)) 
                           |> combinel
                           |> Seq.map Vector.of_list
                           |> Seq.map (Vector.dot weights)
             // Make the results
             Seq.map2 (fun x y -> { X=x;Y=y }) xs ys

end

type LineDetails =
    { pen : Pen
      definition : LineDefinition }

let f32 x = Float32.of_float x
let f64 x = Float.of_float32 x

type Graph = class
    inherit Control
    val mutable maxX : float
    val mutable maxY : float
    val mutable minX : float
    val mutable minY : float
    val mutable lines : LineDetails list
    new () as x = 
      { maxX = 1.0;
        maxY = 1.0;
        minX = -1.0;
        minY = -1.0;
        lines = [] } 
      then
        x.Paint.Add(fun e -> x.DrawGraph(e.Graphics))
        x.Resize.Add(fun _ -> x.Invalidate())

    member f.DrawGraph(graphics : Graphics) =
        let height = Convert.ToSingle(f.Height)
        let width = Convert.ToSingle(f.Width) 
        let widthF = f32 f.maxY - f32 f.minY
        let heightF = f32 f.maxX - f32 f.minX
        let stepY = height / heightF 
        let stepX =  width / widthF
        let orginY = (0.0f - f32 f.minY) * stepY
        let orginX = (0.0f - f32 f.minX) * stepX
        let black = new Pen(Color.Black)
        graphics.DrawLine(black, 0.0f, orginY, width, orginY)
        graphics.DrawLine(black, orginX, 0.0f, orginX, height)
        
        let mapPoint pt =
            new PointF(orginX + (f32 pt.X * stepX), 
                       height - (orginY + (f32 pt.Y * stepY)))
                    
        let xs = { f.minX .. (1.0 / f64 stepX) .. f.maxX }
        f.lines
        |> List.iter 
            (fun line -> LineFunctions.sample xs line.definition
                         |> Seq.map mapPoint
                         |> Seq.to_array
                         |> (fun pts -> graphics.DrawLines(line.pen, pts)))
end

module GraphTest = begin

    let wiggle = PointList [ (0.1,0.6); (0.3,-0.3); (0.5,0.8); (0.7,-0.2) ]
    let straight = Function (fun x -> x + 0.1)
    let square = Function (fun x -> x * x)
    let strange = Combination [ (0.2, square); (0.4, wiggle); (0.4, straight) ]

    let lines =
        [{ pen = new Pen(Color.Blue) ;
           definition = wiggle };
         { pen = new Pen(Color.Orange, 
                         DashStyle = DashStyle.Dot, 
                         Width = 2.0f) ;
           definition = straight };
         { pen = new Pen(Color.Red, 
                         DashStyle = DashStyle.Dash, 
                         Width = 2.0f) ;
           definition = square };
         { pen = new Pen(Color.Green, Width = 2.0f) ;
           definition = strange } ]

    let form =
        let temp = new Form(Visible=true,TopMost=true)
        let g = new Graph(Dock = DockStyle.Fill)
        g.lines <- lines
        temp.Controls.Add(g)
        temp

    [<STAThread>]
    do Application.Run(form)
end
