#light
open System.IO

let files = Directory.GetFiles(@"c:\")

for filepath in files do
    let file = new FileInfo(filepath)
    printfn "%s\t%d\t%O"
        file.Name
        file.Length
        file.CreationTime
