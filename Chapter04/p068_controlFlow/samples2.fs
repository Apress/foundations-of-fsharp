#light
let shusonKato = [| "watching."; "been "; "have ";
    "children "; "three "; "my "; "realize "; "and ";
    "ant "; "an "; "kill "; "I ";
    |]
    
for index = Array.length shusonKato - 1 downto 0 do
    print_string shusonKato.[index]