#light
open System.Runtime.InteropServices

[<DllImport("User32.dll")>]
extern bool MessageBeep(uint32 beepType)

MessageBeep(0ul) |> ignore
read_line()
