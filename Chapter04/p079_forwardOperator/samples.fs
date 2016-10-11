#light
type date = { year : int; month : int; day : int }

let fsDateList =
    [ { year = 1999 ; month = 12; day = 31 };
      { year = 1999 ; month = 12; day = 31 };
      { year = 1999 ; month = 12; day = 31 } ]

List.iter (fun d -> print_int d.year) fsDateList
fsDateList |> List.iter (fun d -> print_int d.year)
