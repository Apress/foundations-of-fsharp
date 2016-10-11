#light
open System.Windows.Forms
open Strangelights.Forms

let fibs =
    (1,1) |> Seq.unfold
        (fun (n0, n1) ->
        Some(n0, (n1, n0 + n1)))
        
let getFib n =
    Seq.nth n fibs
    
let form =
    let temp = new FibForm()
    temp.Calculate.Click.Add
        (fun _ ->
            let n = int_of_string temp.Input.Text
            let n = getFib n
            temp.Result.Text <- string_of_int n)
    temp
    
Application.Run(form)