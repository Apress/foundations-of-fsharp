#light
let anotherObject = ("This is a string" :> obj)

if (anotherObject :? string) then
    print_endline "This object is a string"
else
    print_endline "This object is not a string"
    
if (anotherObject :? string[]) then
    print_endline "This object is a string array"
else
    print_endline "This object is not a string array"