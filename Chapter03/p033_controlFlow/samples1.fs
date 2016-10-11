#light
let result =
    if System.DateTime.Now.Second % 2 = 0 then
        "heads"
    else
        "tails"
        
print_string result