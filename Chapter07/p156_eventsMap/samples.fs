#light

let trigger, event = IEvent.create<string>()
let newEvent = event |> IEvent.map (fun x -> "Mapped data: " + x)
newEvent.Add(fun x -> print_endline x)

trigger "Harry"
trigger "Sally"