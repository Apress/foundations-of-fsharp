// Copyright (c) Microsoft Corporation 2005-2006.
// This sample code is provided "as is" without warranty of any kind. 
// We disclaim all warranties, either express or implied, including the 
// warranties of merchantability and fitness for a particular purpose. 
//

#light

module Microsoft.FSharp.Bindings.DLinq

open Quotations
open Quotations.Typed
open System
open System.Query
open System.Data.DLinq
open System.Expressions
open System.Collections.Generic

type NDecimal = Nullable<System.Decimal>

val (« »)    : ('a,'b,'c,'a expr) template -> 'c 
val (<@ @>)  : ('a,'b,'c,'a expr) template -> 'c 

val («| |»)  : ('a,'b,'c,'a expr) template -> 'a expr -> 'b option 
val (<@| |@>): ('a,'b,'c,'a expr) template -> 'a expr -> 'b option 

val lift : 'a -> 'a expr 

val ( <>?! )       : Nullable< 'a> -> 'a -> bool          when 'a :> System.ValueType
val ( =?! )        : Nullable< 'a> -> 'a -> bool          when 'a :> System.ValueType
val ( <=?! )       : Nullable< 'a> -> 'a -> bool          when 'a :> System.ValueType
val ( <?!  )       : Nullable< 'a> -> 'a -> bool          when 'a :> System.ValueType
val ( >?!  )       : Nullable< 'a> -> 'a -> bool          when 'a :> System.ValueType
val ( >=?! )       : Nullable< 'a> -> 'a -> bool          when 'a :> System.ValueType
val inline ( +?! ) : Nullable< ^a> -> ^a -> Nullable< ^a> when ^a : (static member op_Addition   : ^a * ^a -> ^a) and ^a :> System.ValueType
val inline ( -?! ) : Nullable< ^a> -> ^a -> Nullable< ^a> when ^a : (static member op_Subtraction: ^a * ^a -> ^a) and ^a :> System.ValueType
val inline ( *?! ) : Nullable< ^a> -> ^a -> Nullable< ^a> when ^a : (static member op_Multiply   : ^a * ^a -> ^a) and ^a :> System.ValueType
val inline ( /?! ) : Nullable< ^a> -> ^a -> Nullable< ^a> when ^a : (static member op_Division   : ^a * ^a -> ^a) and ^a :> System.ValueType


module Query : begin
  
    val ( |> )     : 'a -> ('a -> 'b) -> 'b 
    
    val orderBy   : ('a -> 'b) expr    -> #IQueryable<'a>       -> IOrderedQueryable<'a>  
    val select    : ('a -> 'b) expr    -> #IQueryable<'a>       -> IQueryable<'b> 
    val first     : ('a -> bool) expr  -> #IQueryable<'a>       -> 'a 
    val take      : int                -> #IQueryable<'a>       -> IQueryable<'a> 
    val min       :                       #IQueryable<'a>       -> 'a 
    val fmin      : ('a -> 'b) expr    -> #IQueryable<'a>       -> 'b                when 'b  :> IComparable<'b>
    val averagem  :                       #IQueryable<Decimal>  -> Decimal
    val averageNm :                       #IQueryable<NDecimal> -> NDecimal  
    val where     : ('a -> bool) expr  -> #IQueryable<'a>       -> IQueryable<'a> 
    val distinct  :                       #IQueryable<'a>       -> IQueryable<'a> 
    val groupBy   : ('a -> 'k) expr    -> #IQueryable<'a>       -> IQueryable<IGrouping<'k,'a> >
    val groupByAndProject 
                  : ('a -> 'k) expr    -> 
                    ('a -> 'v) expr    -> #IQueryable<'a>       -> IQueryable<IGrouping<'k,'v> > 

    val selectMany : ('a -> IEnumerable<'b>) expr 
                                        -> #IQueryable<'a>       -> IQueryable<'b> 


    val iter      : ('a -> unit)       -> #IEnumerable<'a> -> unit 

end


type projEnv = Map.Map<Quotations.Raw.exprVarName,ParameterExpression> 

val projE : projEnv -> Quotations.Raw.expr -> Expression 
val projV : projEnv -> Quotations.Raw.exprVarName -> ParameterExpression 
val projVbind : Quotations.Raw.exprVar -> ParameterExpression 
val projFunc2 : ('a -> 'b) expr -> Expression<Func<'a,'b> > 
val projFunc3 : ('a -> 'b -> 'c) expr -> Expression<Func<'a,'b,'c> > 

