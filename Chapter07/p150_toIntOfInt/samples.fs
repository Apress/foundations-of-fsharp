#light
open System

let dayInt = Enum.to_int DateTime.Now.DayOfWeek
let (dayEnum : DayOfWeek) = Enum.of_int dayInt

print_int dayInt
print_newline ()
print_any dayEnum