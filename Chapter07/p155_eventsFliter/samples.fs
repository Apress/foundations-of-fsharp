#light

let trigger, event = IEvent.create<string>()
let newEvent = event |> IEvent.filter (fun x -> x.StartsWith("H"))

newEvent.Add(fun x -> printfn "new event: %s" x)
trigger "Harry"
trigger "Jane"
trigger "Hillary"
trigger "John"
trigger "Henry"