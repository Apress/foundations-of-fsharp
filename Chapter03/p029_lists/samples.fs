#light
let one = ["one "]
let two = "two " :: one
let three = "three " :: two

let rightWayRound = List.rev three

let printList l =
    List.iter print_string l
    print_newline()

let main() =
    printList one
    printList two
    printList three
    printList rightWayRound

main()