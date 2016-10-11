#light
let ryunosukeAkutagawa = [| "Green "; "frog, ";
    "Is "; "your "; "body "; "also ";
    "freshly "; "painted?" |]
    
for index = 0 to Array.length ryunosukeAkutagawa - 1 do
    print_string ryunosukeAkutagawa.[index]