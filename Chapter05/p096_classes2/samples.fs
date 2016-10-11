#light
open System.IO
type File1 = class
    val path: string
    val innerFile: FileInfo
    new(path) =
        { path = path ;
        innerFile = new FileInfo(path) }
end

let myFile1 = new File1("whatever.txt")