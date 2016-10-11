#light
let rec conactStringList =
    function head :: tail -> head + conactStringList tail
           | [] -> ""

let jabber = ["'Twas "; "brillig, "; "and "; "the "; "slithy "; "toves "; "..."]
let completJabber = conactStringList jabber

print_endline completJabber