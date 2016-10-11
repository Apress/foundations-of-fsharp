#light
type MyClass = class
    static member revString (s : string) =
        let chars = s.ToCharArray() in
        let reved_chars = Array.rev chars in
        new string(reved_chars)
end

let myString = MyClass.revString "dlrow olleH"

print_string myString