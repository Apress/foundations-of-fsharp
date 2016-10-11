// Copyright (c) Microsoft Corporation 2005-2006.
// This sample code is provided "as is" without warranty of any kind. 
// We disclaim all warranties, either express or implied, including the 
// warranties of merchantability and fitness for a particular purpose. 
//


module Sample.NorthwindDumper

//-------------------------------------------------------------------------
// An object dumper for Northwind

open nwind
open Microsoft.FSharp.Reflection;;
open System
open System.Reflection


let queryTuple (x:obj) =
  match Reflection.Value.GetInfo x with
  | TupleValue(args) -> Some (args)
  | _ -> None

let unpackCons recd =
  match recd with 
  | [(_,h);(_,t)] -> (h,t)
  | _             -> failwith "unpackCons"
let queryCons (x:obj) =
  match x with 
  | null -> None 
  | _ -> 
  match Reflection.Value.GetInfo x with
  | ConstructorValue (ty,_,recd) -> Some (unpackCons recd)
  | _ -> None
let rec queryList (x:obj) =
  match x with 
  | null -> Some([]) 
  | _ -> 
  match queryCons x with
  | Some(x,y) -> begin match queryList y with Some l -> Some(x::l) | None -> None end
  | _ -> 
  None

open Microsoft.FSharp.Bindings.Linq.SequenceOps

  let rec preProcess1 (o : obj) = 
    match queryTuple o with 
    | Some([x1;x2]) -> box (preProcess1 x1, preProcess1 x2)
    | Some([x1;x2;x3]) -> box (preProcess1 x1, preProcess1 x2, preProcess1 x3)
    | Some([x1;x2;x3;x4]) -> box (preProcess1 x1, preProcess1 x2, preProcess1 x3, preProcess1 x4)
    | _ -> 
    match queryList o with 
    | Some(l) -> box (List.map preProcess1 l)
    | _ -> 
    match o with 
    | (:? System.Collections.IList as l) -> 
      preProcess1 (box (IEnumerable.untyped_to_list l : obj list ))
    | (:? Customer) | (:? Product) |(:? Order) |(:? Employee) |(:? Supplier) |(:? Category)  -> 
      let ty = o.GetType() in 
      let members = ty.GetMembers(Enum.combine [BindingFlags.Public; BindingFlags.Instance]) in 
      let res = 
        Array.to_list members 
        |> List.filter (fun m -> (m :? FieldInfo) or (m:? PropertyInfo))
        |> List.map (fun m -> m.Name,
                              (match m with 
                              | :? FieldInfo as f -> f.GetValue(o)
                              | :? PropertyInfo as p -> p.GetValue(o,null)
                              | _ -> null)) in 
      box res                            
    | _ -> o 

let dump q = 
  q 
  |> select (fun x -> preProcess1 (box x))
  |> to_list
  |> (fun x -> Console.WriteLine("{0}", any_to_string x))

let dump1 x = x |> box |> preProcess1 |> (fun x -> Console.WriteLine("{0}", any_to_string x))
