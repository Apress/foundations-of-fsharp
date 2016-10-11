#light
#I "C:\Program Files\LINQ Preview\Bin";;
#r "System.Query.dll";;
open System.Query
open System.Reflection

// define easier access to LINQ methods
let select f s = Sequence.Select(s, new Func<_,_>(f))
let where f s = Sequence.Where(s, new Func<_,_>(f))
let groupBy f s = Sequence.GroupBy(s, new Func<_,_>(f))
let orderBy f s = Sequence.OrderBy(s, new Func<_,_>(f))
let count s = Sequence.Count(s)

// query string methods using functions
let namesByFunction =
    (type string).GetMethods()
    |> where (fun m -> not m.IsStatic)
    |> groupBy (fun m -> m.Name)
    |> select (fun m -> m.Key, count m)
    |> orderBy (fun (_, m) -> m)
    
namesByFunction
|> Seq.iter (fun (name, count) -> printfn "%s - %i" name count)
