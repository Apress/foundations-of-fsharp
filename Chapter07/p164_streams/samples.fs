#light
let getStream() =
    print_string "Write to a console (y/n): "
    let input = read_line ()
    match input with
    | "y" | "Y" -> stdout
    | "n" | "N" ->
    print_string "Enter file name: "
    let filename = read_line ()
    open_out filename
    | _ -> failwith "Not an option"
    
let main() =
    let stream = getStream()
    output_string stream "Hello"
    close_out stream
    read_line() |> ignore
    
main()