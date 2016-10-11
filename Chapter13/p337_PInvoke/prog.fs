#light
open System.Runtime.InteropServices

[<DllImport("Advapi32.dll")>]
extern bool FileEncryptionStatus(string filename, uint32* status)

let main() =
    let mutable status = 0ul
    FileEncryptionStatus(@"C:\test.txt", && status) |> ignore
    print_any status
    read_line()
    
main()

