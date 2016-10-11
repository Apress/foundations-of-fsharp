#light
#I "C:\Program Files\LINQ Preview\Bin";;
#r "System.Query.dll";;
#r "System.Xml.XLinq.dll";;
open System.Query
open System.Reflection
open System.Xml.XLinq

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
    |> select (fun m -> new XElement(XName.Get(m.Key), count m))
    |> orderBy (fun e -> int_of_string e.Value)
    
let overloadsXml =
    new XElement(XName.Get("MethodOverloads"), namesByFunction)
    
print_endline (overloadsXml.ToString())