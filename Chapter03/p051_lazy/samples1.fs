#light
let lazyValue = lazy ( 2 + 2 )
let actualValue = Lazy.force lazyValue

print_int actualValue
print_newline()
