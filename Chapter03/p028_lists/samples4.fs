#light
let emptyList = []

let oneItem = "one " :: []

let twoItem = "one " :: "two " :: []

let shortHand = ["apples "; "pairs "]

let twoLists = ["one, "; "two, "] @ ["buckle "; "my "; "shoe "]

let objList = [box 1; box 2.0; box "three"]

let printList l =
    List.iter print_string l
    print_newline()
    
let main() =
    printList emptyList
    printList oneItem
    printList twoItem
    printList shortHand
    printList twoLists
    
    for x in objList do
        print_any x
        print_char ' '
    print_newline()

main()
