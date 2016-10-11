#light
type person = { name : string ; favoriteColor : string }

let robert2 = { name = "Robert" ; favoriteColor = "Red" }
let robert3 = { name = "Robert" ; favoriteColor = "Green" }

printf "(robert2 > robert3): %b\r\n" (robert2 > robert3)