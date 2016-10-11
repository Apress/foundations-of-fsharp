#light
open System
open System.Windows.Forms

let form =
    let temp = new Form()
    let textBox = new TextBox(Top=8,Left=8, Width=temp.Width - 24,
                              Anchor = (AnchorStyles.Left |||
                                        AnchorStyles.Right |||
                                        AnchorStyles.Top))
    temp.Controls.Add(textBox)
    temp

[<STAThread>]
do Application.Run(form)