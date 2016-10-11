#light
open System.IO

type File2 = class
    val path: string
    val innerFile: FileInfo
    new() = new File2("default.txt")
    new(path) =
        { path = path ;
        innerFile = new FileInfo(path) }
end

let myFile2 = new File2("whatever2.txt")
