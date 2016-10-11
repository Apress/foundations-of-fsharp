#light
let matsuoBasho = ref [ "An "; "old "; "pond! ";
    "A "; "frog "; "jumps "; "in- ";
    "The "; "sound "; "of "; "water" ]
    
while (List.nonempty !matsuoBasho) do
    print_string (List.hd !matsuoBasho);
    matsuoBasho := List.tl !matsuoBasho