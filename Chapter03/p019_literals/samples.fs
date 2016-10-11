#light
let message = "Hello
World\r\n\t!"
let dir = @"c:\projects"

let bytes = "bytesbytesbytes"B

let xA = 0xFFy
let xB = 0o7777un
let xC = 0b10010UL

let print x = printfn "%A" x

let main() =
    print message;
    print dir;
    print bytes;
    print xA;
    print xB;
    print xC
main()