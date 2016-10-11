#light
type person = { name : string ; favoriteColor : string }

let robert1 = { name = "Robert" ; favoriteColor = "Red" }
let robert2 = { name = "Robert" ; favoriteColor = "Red" }

printfn "(Obj.eq robert1 robert2): %b" (Obj.eq robert1 robert2)