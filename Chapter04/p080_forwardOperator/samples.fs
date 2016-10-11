#light
let methods = System.AppDomain.CurrentDomain.GetAssemblies()
                |> List.of_array
                |> List.map ( fun assm -> assm.GetTypes() )
                |> Array.concat
                |> List.of_array
                |> List.map ( fun t -> t.GetMethods() )
                |> Array.concat

print_any methods