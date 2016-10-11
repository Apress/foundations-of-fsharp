#light
open System.Windows.Forms

let anchor = AnchorStyles.Left ||| AnchorStyles.Left

printfn "test AnchorStyles.Left: %b"
    (anchor &&& AnchorStyles.Left <> Enum.of_int 0)
printfn "test AnchorStyles.Right: %b"
    (anchor &&& AnchorStyles.Right <> Enum.of_int 0)