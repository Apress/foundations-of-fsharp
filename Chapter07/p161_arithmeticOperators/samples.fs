#light

type person = { name : string ; favoriteColor : string }

let robert1 = { name = "Robert" ; favoriteColor = "Red" }
let robert2 = { name = "Robert" ; favoriteColor = "Red" }
let robert3 = { name = "Robert" ; favoriteColor = "Green" }

printf "(robert1 = robert2): %b\r\n" (robert1 = robert2)
printf "(robert1 <> robert3): %b\r\n" (robert1 <> robert3)