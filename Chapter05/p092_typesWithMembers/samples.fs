#light
open System
open System.Drawing
open System.Windows.Forms

let makeNumberControl (n : int) =
    { new Control(Tag = n, Width = 32, Height = 16) with
        override x.OnPaint(e) =
            let font = new Font(FontFamily.Families.[2], 12.0F)
            e.Graphics.DrawString(n.ToString(),
                                    font,
                                    Brushes.Black,
                                    new PointF(0.0F, 0.0F))
                                    
      interface IComparable with
            CompareTo(other) =
                let otherControl = other :?> Control in
                let n1 = otherControl.Tag :?> int in
                n.CompareTo(n1) }
                
let numbers =
    let temp = new ResizeArray<Control>()
    let rand = new Random()
    for index = 1 to 10 do
        temp.Add(makeNumberControl (rand.Next(100)))
    temp.Sort()
    let height = ref 0
    temp |> IEnumerable.iter
        (fun c ->
            c.Top <- !height
            height := c.Height + !height)
    temp.ToArray()
    
let numbersForm =
    let temp = new Form() in
    temp.Controls.AddRange(numbers);
    temp
    
[<STAThread>]
do Application.Run(numbersForm)
