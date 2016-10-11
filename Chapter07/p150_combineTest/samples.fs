#light
open System.Windows.Forms

let anchor = Enum.combine [AnchorStyles.Left ; AnchorStyles.Left]

printfn "(Enum.test anchor AnchorStyles.Left): %b"
    (Enum.test anchor AnchorStyles.Left)
printfn "(Enum.test anchor AnchorStyles.Right): %b"
    (Enum.test anchor AnchorStyles.Right)