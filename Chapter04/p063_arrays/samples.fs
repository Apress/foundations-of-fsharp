#light
let rhymeArray =
    [| "Went to market" ;
       "Stayed home" ;
       "Had roast beef" ;
       "Had none" |]
       
let firstPiggy = rhymeArray.[0]
let secondPiggy = rhymeArray.[1]
let thirdPiggy = rhymeArray.[2]
let fourthPiggy = rhymeArray.[3]

rhymeArray.[0] <- "Wee,"
rhymeArray.[1] <- "wee,"
rhymeArray.[2] <- "wee,"
rhymeArray.[3] <- "all the way home"
print_endline firstPiggy
print_endline secondPiggy
print_endline thirdPiggy
print_endline fourthPiggy
print_any rhymeArray