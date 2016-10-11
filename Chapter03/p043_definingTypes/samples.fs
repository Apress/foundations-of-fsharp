#light
type Organization1 = { boss : string ; lackeys : string list }
let rainbow =
    { boss = "Jeffrey" ;
    lackeys = ["Zippy"; "George"; "Bungle"] }
type Organization2 = { chief : string ; underlings : string list }
type Organization3 = { chief : string ; indians : string list }

let thePlayers =
    { new Organization2
         with chief = "Peter Quince"
         and underlings = ["Francis Flute"; "Robin Starveling";
            "Tom Snout"; "Snug"; "Nick Bottom"] }
            
let wayneManor =
    { new Organization3
        with chief = "Batman"
        and indians = ["Robin"; "Alfred"] }