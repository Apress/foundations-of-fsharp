#light
let obsolete = System.AppDomain.CurrentDomain.GetAssemblies()
                |> List.of_array
                |> List.map ( fun assm -> assm.GetTypes() )
                |> Array.concat
                |> List.of_array
                |> List.filter
                ( fun m ->
                    m.IsDefined((type System.ObsoleteAttribute), true))
                    
print_any obsolete