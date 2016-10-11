#light
let writeToFile() =
    let file = System.IO.File.CreateText("test.txt") in
    try
        file.WriteLine("Hello F# users")
    finally
        file.Dispose()
        
writeToFile()