#light
open System.IO
type File3 = class
    val path: string
    val innerFile: FileInfo
    new(path) as x =
        { path = path ;
        innerFile = new FileInfo(path) }
        then
        if not x.innerFile.Exists then
            let textFile = x.innerFile.CreateText()
            textFile.Dispose()
end

let myFile3 = new File3("whatever.txt")