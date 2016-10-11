#light

let trigger, event = IEvent.create<string>()
let hData, nonHData = event |> IEvent.partition (fun x -> x.StartsWith("H"))

hData.Add(fun x -> printfn "H data: %s" x)
nonHData.Add(fun x -> printfn "None H data: %s" x)

trigger "Harry"
trigger "Jane"
trigger "Hillary"
trigger "John"
trigger "Henry"