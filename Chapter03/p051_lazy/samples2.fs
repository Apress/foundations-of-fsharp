#light
let lazySideEffect =
    lazy
        ( let temp = 2 + 2
          print_int temp
          print_newline()
          temp )
          
print_endline "Force value the first time: "
let actualValue1 = Lazy.force lazySideEffect
print_endline "Force value the second time: "
let actualValue2 = Lazy.force lazySideEffect