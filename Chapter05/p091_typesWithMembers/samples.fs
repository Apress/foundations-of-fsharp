#light
open System
open System.Collections.Generic
let comparer =
    { new IComparer<string>
        with
            Compare(s1, s2) =
                let rev (s : String) =
                    new String(Array.rev (s.ToCharArray()))
                let reversed = rev s1
                reversed.CompareTo(rev s2) }
let winners =
    [| "Sandie Shaw" ;
        "Bucks Fizz" ;
        "Dana International" ;
        "Abba";
        "Lordi" |]
    
print_any winners
print_newline()
Array.Sort(winners, comparer)
print_any winners