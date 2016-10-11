// Copyright (c) Microsoft Corporation 2005-2006.
// This sample code is provided "as is" without warranty of any kind. 
// We disclaim all warranties, either express or implied, including the 
// warranties of merchantability and fitness for a particular purpose. 
//

#light

module Microsoft.FSharp.Bindings.Linq

open System
open System.Query
open System.Collections.Generic


module SequenceOps = begin

  let (|>) x f = f x

  let it x = x

  let orderBy f (coll :> IEnumerable<_>) = 
    System.Query.Sequence.OrderBy(coll,new Func<_,_>(f))

  let select f coll = 
    System.Query.Sequence.Select(coll,new Func<_,_>(f))

  let selecti f (coll :> IEnumerable<_>) = 
    System.Query.Sequence.Select(coll,new Func<_,_,_>(f))

  let selectMany f (coll :> IEnumerable<_>) = 
    System.Query.Sequence.SelectMany(coll,new Func<_,_>(f))

  let selectManyi f (coll :> IEnumerable<_>) = 
    System.Query.Sequence.SelectMany(coll,new Func<_,_,_>(f))

  let where f (coll :> IEnumerable<_>) = 
    System.Query.Sequence.Where(coll,new Func<_,_>(f))

  let fmin<'a,'c,.. > (qf : ('a -> 'c)) (coll :> IEnumerable<'a>)  = 
    System.Query.Sequence.Min(coll,new Func<'a,'c>(qf))

  let wherei f (coll :> IEnumerable<_>) = 
    System.Query.Sequence.Where(coll,new Func<_,int,_>(f))

  let hd (coll :> IEnumerable<_>) = 
    System.Query.Sequence.First(coll)

  let first f (coll :> IEnumerable<_>) = 
    System.Query.Sequence.First(coll,new Func<_,_>(f))

  let to_List (coll :> IEnumerable<_>) = 
    System.Query.Sequence.ToList(coll)

  let to_list coll = List.of_List (to_List coll)

  let to_Array (coll :> IEnumerable<_>) = 
    System.Query.Sequence.ToArray(coll)

  let to_Dictionary keyf elemf (coll :> IEnumerable<_>) = 
    System.Query.Sequence.ToDictionary(coll,new Func<_,_>(keyf),new Func<_,_>(elemf))

  let iter f (coll :> IEnumerable<_>) = 
    Idioms.foreachG coll f

  let square ((coll1 :> IEnumerable<_>), (coll2 :> IEnumerable<_>)) = 
    System.Query.Sequence.SelectMany(coll1, fun x -> coll2 |> select (fun y -> (x,y)))

  let squarei ((coll1 :> IEnumerable<_>), (coll2 :> IEnumerable<_>)) = 
    System.Query.Sequence.SelectMany(coll1, fun x i -> coll2 |> selecti (fun y j -> (x,i,y,j)))

end
