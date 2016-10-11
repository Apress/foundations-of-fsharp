#light
type DrinkAmount =
    | Coffee of int
    | Tea of int
    | Water of int
    with
        override x.ToString() =
            match x with
            | Coffee x -> Printf.sprintf "Coffee: %i" x
            | Tea x -> Printf.sprintf "Tea: %i" x
            | Water x -> Printf.sprintf "Water: %i" x
    end
    
let t = Tea 2

print_endline (t.ToString())