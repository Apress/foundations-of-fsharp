#light
open System.IO

type File1(path) = class
    let innerFile = new FileInfo(path)
    member x.InnerFile = innerFile
end

let myFile1 = new File1("whatever.txt")