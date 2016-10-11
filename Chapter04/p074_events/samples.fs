#light
open System.Timers
module WF = System.Windows.Forms
let timer =
    let temp = new Timer()
    temp.Interval <- 3000.0
    temp.Enabled <- true
    let messageNo = ref 0
    temp.Elapsed.Add(fun _ ->
        let messages = ["bet"; "this"; "gets";
        "really"; "annoying"; "very"; "quickly";]
        WF.MessageBox.Show(List.nth messages !messageNo) |> ignore
        messageNo := (!messageNo + 1) % (List.length messages))
    temp
    
print_endline "Whack the return to finish!"
read_line() |> ignore
timer.Enabled <- false
