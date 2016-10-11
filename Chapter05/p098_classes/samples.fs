#light
open System.IO

type File4 = class
    val path: string
    val mutable innerFile: FileInfo
    new(path) as x =
        { path = path ;
        innerFile = new FileInfo(path) }
        then
        if not x.innerFile.Exists then
            let dir = x.innerFile.Directory in
            let files = dir.GetFiles() in
            if files.Length > 0 then
                x.innerFile <- files.(0)
        else
            failwith "no files exist in that dir"
end

let myFile4 = new File4("whatever2.txt")