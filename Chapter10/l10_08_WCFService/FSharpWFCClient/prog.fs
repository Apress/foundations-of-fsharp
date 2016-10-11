#light
Idioms.using (new GreetingServiceClient())
    (fun client ->
        while true do
            print_endline (client.Greet("Rob"))
            read_line() |> ignore)
            
Idioms.using (new GreetingServiceClient())
    (fun client ->
        print_endline (client.Greet("Rob"))
        read_line() |> ignore)