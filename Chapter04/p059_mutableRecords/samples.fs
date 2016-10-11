#light
type Couple = { her : string ; mutable him : string }
let theCouple = { her = "Elizabeth Taylor " ; him = "Nicky Hilton" }

let print o = printfn "%A" o

let changeCouple() =
    print theCouple;
    theCouple.him <- "Michael Wilding";
    print theCouple;
    theCouple.him <- "Michael Todd";
    print theCouple;
    theCouple.him <- "Eddie Fisher";
    print theCouple;
    theCouple.him <- "Richard Burton";
    print theCouple;
    theCouple.him <- "Richard Burton";
    print theCouple;
    theCouple.him <- "John Warner";
    print theCouple;
    theCouple.him <- "Larry Fortensky";
    print theCouple
changeCouple()
