#light

let trigger, event = IEvent.create<string>()
event.Add(fun x -> printfn "%s" x)
trigger "hello"