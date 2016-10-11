#light
// write to a string
let s = Printf.sprintf "Hello %s\r\n" "string"
print_string s
// prints the string to the given channel
Printf.fprintf stdout "Hello %s\r\n" "channel"
// prints the string to a .NET TextWriter
Printf.twprintf System.Console.Out "Hello %s\r\n" "TextWriter"
// create a string that will be placed
// in an exception message
Printf.failwithf "Hello %s" "exception"