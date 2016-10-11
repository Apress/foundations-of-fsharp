#light
let tenOnes = Seq.init_finite 10 (fun _ -> 1)
let allIntegers = Seq.init_infinite (fun x -> System.Int32.MinValue + x)
let firstTenInts = Seq.take 10 allIntegers

tenOnes |> Seq.iter (fun x -> printf "%i ... " x)
print_newline()
print_any firstTenInts