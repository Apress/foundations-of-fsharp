#light
let myFlag = ref true
let myString = ref ""
let myInt = ref 0
let myFloat = ref 0.0
let (myStringList : string list ref) = ref []

let argList =
    [ ("-set", Arg.Set myFlag, "Sets the value myFlag");
      ("-clear", Arg.Clear myFlag, "Clears the value myFlag");
      ("-str_val", Arg.String(fun x -> myString := x), "Sets the value myString");
      ("-int_val", Arg.Int(fun x -> myInt := x), "Sets the value myInt");
      ("-float_val", Arg.Float(fun x -> myFloat := x), "Sets the value myFloat"); ]
      
if Sys.argv.Length <> 1 then
    Arg.parse
        argList
        (fun x -> myStringList := x :: !myStringList)
        "Arg module demo"
else
    Arg.usage
        argList
        "Arg module demo"
    exit 1

printfn "myFlag: %b" !myFlag
printfn "myString: %s" !myString
printfn "myInt: %i" !myInt
printfn "myFloat: %f" !myFloat
printfn "myStringList: "
print_any !myStringList