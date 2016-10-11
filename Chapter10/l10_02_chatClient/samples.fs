#light
open System
open System.ComponentModel
open System.IO
open System.Net.Sockets
open System.Threading
open System.Windows.Forms

let form =
    let temp = new Form()
    temp.Text <- "F# Talk Client"

    temp.Closing.Add(fun e -> 
        Application.Exit()
        Environment.Exit(0)) 

    let output = 
        new TextBox(Dock = DockStyle.Fill, 
                     ReadOnly = true, 
                     Multiline = true)
    temp.Controls.Add(output) 

    let input = new TextBox(Dock = DockStyle.Bottom, Multiline = true)
    temp.Controls.Add(input)

    let tc = new TcpClient() 
    tc.Connect("localhost", 4242) 

    let load() =
        let run() = 
            let sr = new StreamReader(tc.GetStream())
            while(true) do 
                let text = sr.ReadLine()
                if text <> null && text <> "" then
                    temp.Invoke(new MethodInvoker(fun () ->
                        output.AppendText(text + Environment.NewLine) 
                        output.SelectionStart <- output.Text.Length))
                    |> ignore
        let t = new Thread(new ThreadStart(run)) 
        t.Start()

    temp.Load.Add(fun _ -> load())

    let sw = new StreamWriter(tc.GetStream())
    let keyUp _ = 
        if(input.Lines.Length > 1) then 
            let text = input.Text
            if (text <> null && text <> "") then
                begin 
                    try 
                        sw.WriteLine(text)
                        sw.Flush()
                    with err -> 
                        MessageBox.Show(sprintf "Server error\n\n%O" err) 
                        |> ignore
                end;
                input.Text <- ""

    input.KeyUp.Add(fun e -> keyUp e) 
    temp

[<STAThread>]
do Application.Run(form)
