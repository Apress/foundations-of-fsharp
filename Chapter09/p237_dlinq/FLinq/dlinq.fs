// Copyright (c) Microsoft Corporation 2005-2006.
// This sample code is provided "as is" without warranty of any kind. 
// We disclaim all warranties, either express or implied, including the 
// warranties of merchantability and fitness for a particular purpose. 
//


#light

module Microsoft.FSharp.Bindings.DLinq

open System
open System.Query
open System.Collections.Generic
open System.Data.DLinq
open System.Expressions
open System.Reflection
open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.Utilities
open Microsoft.FSharp.Quotations.Raw
open Microsoft.FSharp.Quotations.Typed
open List

//-------------------------------------------------------------------------
// Misc. library utilities

let splitAt n xs =
    if n<0 then failwith "splitAt: -ve" else
    let rec split l = 
        match l with 
        | 0,xs    -> [],xs
        | n,x::xs -> let front,back = split (n-1,xs)
                     x::front,back
        | n,[]    -> failwith "splitAt: not enough elts list"
    split (n,xs)

let nonNull msg x = if (x :> obj) = null then failwith ("unexpected null value: " ^ msg) else x

//-------------------------------------------------------------------------
// Publish the quotation operators so users of this module
// don't need to open Quotations.Typed

let (« ») t     = Quotations.Typed.(« ») t
let (<@ @>) t   = Quotations.Typed.(<@ @>)t
let («| |») p t   = Quotations.Typed.(«| |») p t
let (<@| |@>) p t = Quotations.Typed.(<@| |@>) p t
let lift (x : 'a) : 'a expr = Quotations.Typed.lift x


//-------------------------------------------------------------------------
// Nullable utilities for F#

/// This operator compares Nullable values with non-Nullable values using
/// structural comparison
let (>=?!) (x : Nullable<'a>) (y: 'a) = 
    x.HasValue && x.Value >= y

let (>?!) (x : Nullable<'a>) (y: 'a) = 
    x.HasValue && x.Value > y

let (<=?!) (x : Nullable<'a>) (y: 'a) = 
    not x.HasValue || x.Value <= y

let (<?!) (x : Nullable<'a>) (y: 'a) = 
    not x.HasValue || x.Value < y

let (=?!) (x : Nullable<'a>) (y: 'a) = 
    x.HasValue && x.Value = y

let (<>?!) (x : Nullable<'a>) (y: 'a) = 
    not x.HasValue or x.Value <> y

/// This overloaded operator divides Nullable values by non-Nullable values
/// using the overloaded operator "/".  Inlined to allow use over any type,
/// as this resolves the overloading on "/".
let inline (/?!) (x : Nullable<'a>) (y: 'a) = 
    if x.HasValue then new Nullable<'a>(x.Value / y)
    else x

/// This overloaded operator adds Nullable values by non-Nullable values
/// using the overloaded operator "+".  Inlined to allow use over any type,
/// as this resolves the overloading on "+".
let inline (+?!) (x : Nullable<'a>) (y: 'a) = 
    if x.HasValue then new Nullable<'a>(x.Value + y)
    else x

/// This overloaded operator adds Nullable values by non-Nullable values
/// using the overloaded operator "-".  Inlined to allow use over any type,
/// as this resolves the overloading on "-".
let inline (-?!) (x : Nullable<'a>) (y: 'a) = 
    if x.HasValue then new Nullable<'a>(x.Value - y)
    else x

/// This overloaded operator adds Nullable values by non-Nullable values
/// using the overloaded operator "*".  Inlined to allow use over any type,
/// as this resolves the overloading on "*".
let inline ( *?!) (x : Nullable<'a>) (y: 'a) = 
    if x.HasValue then new Nullable<'a>(x.Value * y)
    else x

/// This overloaded operator adds Nullable values by non-Nullable values
/// using the overloaded operator "%".  Inlined to allow use over any type,
/// as this resolves the overloading on "%".
let inline ( %?!) (x : Nullable<'a>) (y: 'a) = 
    if x.HasValue then new Nullable<'a>(x.Value % y)
    else x

type projEnv = Map.Map<Raw.exprVarName,ParameterExpression> 
type NDecimal = Nullable<System.Decimal>

let efDefnApp tm = fRefineLeft (Raw.efDefn tm) Raw.efApps

//-------------------------------------------------------------------------

let inLang lang e = List.exists (fun m -> fMem m e) lang
let deepMacroExpandUntilInLang lang e = Raw.deepMacroExpandUntil (inLang lang) e


// This is the language the translation understands, addition to 
// primitive F# constructs like tuples, if-then-else, &&, ||, as well as the calls to 
// .NET methods and properties that the DLINQ library comprehends.
// All uses of macros must boil down to uses of these operators.  For example, the
// operators on Nullable above boil down to these operators.
let LINQ = [ efDefn (<@@ (+) @@>); 
             efDefn (<@@ (-) @@>); 
             efDefn (<@@ ( * ) @@>); 
             efDefn (<@@ (%) @@>); 
             efDefn (<@@ (/) @@>); 
             efDefn (<@@ not @@>); 
             efDefn (<@@ (>) @@>); 
             efDefn (<@@ (<) @@>); 
             efDefn (<@@ (>=) @@>); 
             efDefn (<@@ (<=) @@>); 
             efDefn (<@@ (=) @@>); 
             efDefn (<@@ (<>) @@>); 
             efDefn (<@@ (~-) @@>) ]

let expandLINQ x = Typed.map_raw (deepMacroExpandUntilInLang LINQ) x

//-------------------------------------------------------------------------

/// Project F# quotations to Linq expression trees.
/// This is an approximate transformation and is still be rough around the
/// edges.  This translator will also be slow due to the repeated unpickling
/// of target terms.  A more polished LINQ-Quotation translator will be published
/// concert with later versions of LINQ.
let rec projE (env:projEnv) (e: Raw.expr) : Expression = 
    match Raw.efVar.Query e with  
    | Some v -> (projV env v :> Expression)
    | _ -> 
    match Raw.efEquality.Query e with  
    | Some (ty,(x1,x2)) -> 
         (Expression.EQ(projE env x1, projE env x2) :> Expression)
    | _ -> 
    match (efDefnApp (<@@ (+) @@>)).Query(e) with
    | Some ([ty1;ty2;ty3],[x1;x2]) -> 
         let ty1R = RawTypes.dtype_to_Type(ty1)
         let ty2R = RawTypes.dtype_to_Type(ty2)
         if ty1R = (type string) && ty2R = (type string)
         then projE env 
                 (to_raw 
                    («  System.String.Concat( [| _ ; _ |] : string array ) »
                        (of_raw x1) (of_raw x2)  ))
         else (Expression.Add(projE env x1, projE env x2) :> Expression)
    | _ -> 
    match (efDefnApp (<@@ (=) @@>)).Query(e) with
    | Some (_,[x1;x2]) -> 
         (Expression.EQ(projE env x1, projE env x2) :> Expression)
    | _ -> 
    match (efDefnApp (<@@ (/) @@>)).Query(e) with
    | Some (_,[x1;x2]) -> 
         (Expression.Divide (projE env x1, projE env x2) :> Expression)
    | _ -> 
    match (efDefnApp (<@@ (-) @@>)).Query(e) with
    | Some (_,[x1;x2]) -> 
         (Expression.Subtract(projE env x1, projE env x2) :> Expression)
    | _ -> 
    match (efDefnApp (<@@ ( * ) @@>)).Query(e) with
    | Some (_,[x1;x2]) -> 
         (Expression.Multiply(projE env x1, projE env x2) :> Expression)
    | _ -> 
    match (efDefnApp (<@@ (~-) @@>)).Query(e) with
    | Some (_,[x1]) -> 
         (Expression.Negate(projE env x1) :> Expression)
    | _ -> 
    match (efDefnApp (<@@ (>) @@>)).Query(e) with
    | Some (_,[x1;x2]) -> 
         (Expression.GT(projE env x1, projE env x2) :> Expression)
    | _ -> 
    match (efDefnApp (<@@ (>=) @@>)).Query(e) with
    | Some (_,[x1;x2]) -> 
         (Expression.GE(projE env x1, projE env x2) :> Expression)
    | _ -> 
    match (efDefnApp (<@@ (<) @@>)).Query(e) with
    | Some (_,[x1;x2]) -> 
         (Expression.LT(projE env x1, projE env x2) :> Expression)
    | _ -> 
    match (efDefnApp (<@@ (<=) @@>)).Query(e) with
    | Some (_,[x1;x2]) -> 
         (Expression.LE(projE env x1, projE env x2) :> Expression)
    | _ -> 
    match (efDefnApp (<@@ not @@>)).Query(e) with
    | Some (_,[x1]) -> 
         (Expression.Not(projE env x1) :> Expression)
    | _ -> 
    match Raw.efBool.Query e with  
    | Some (x) -> 
         (Expression.Constant(box x) :> Expression)
    | _ -> 
    match Raw.efByte.Query e with  
    | Some (x) -> 
         (Expression.Constant(box x) :> Expression)
    | _ -> 
    match Raw.efSByte.Query e with  
    | Some (x) -> 
         (Expression.Constant(box x) :> Expression)
    | _ -> 
    match Raw.efInt16.Query e with  
    | Some (x) -> 
         (Expression.Constant(box x) :> Expression)
    | _ -> 
    match Raw.efUInt16.Query e with  
    | Some (x) -> 
         (Expression.Constant(box x) :> Expression)
    | _ -> 
    match Raw.efInt32.Query e with  
    | Some (x) -> 
         (Expression.Constant(box x) :> Expression)
    | _ -> 
    match Raw.efUInt32.Query e with  
    | Some (x) -> 
         (Expression.Constant(box x) :> Expression)
    | _ -> 
    match Raw.efInt64.Query e with  
    | Some (x) -> 
         (Expression.Constant(box x) :> Expression)
    | _ -> 
    match Raw.efUInt64.Query e with  
    | Some (x) -> 
         (Expression.Constant(box x) :> Expression)
    | _ -> 
    match Raw.efSingle.Query e with  
    | Some (x) -> 
         (Expression.Constant(box x) :> Expression)
    | _ -> 
    match Raw.efDouble.Query e with  
    | Some (x) -> 
         (Expression.Constant(box x) :> Expression)
    | _ -> 
    match Raw.efString.Query e with  
    | Some (x) -> 
         (Expression.Constant(box x) :> Expression)
    | _ -> 
    match Raw.efChar.Query e with  
    | Some (x) -> 
         (Expression.Constant(box x) :> Expression)
    | _ -> 
    match Raw.efAnd.Query e with  
    | Some (x1,x2) -> 
         (Expression.And(projE env x1, projE env x2) :> Expression)
    | _ -> 
    match Raw.efOr.Query e with  
    | Some (x1,x2) -> 
         (Expression.Or(projE env x1, projE env x2) :> Expression)
    | _ -> 
    match Raw.efLiftedValue.Query e with  
    | Some (x) -> 
         (Expression.Constant(box x) :> Expression)
    | _ -> 
    match Raw.efCoerce.Query e with  
    | Some ((toTy,fromTy),x) -> 
         (Expression.Cast(projE env x,RawTypes.dtype_to_Type toTy) :> Expression)
    | _ -> 
    match Raw.efTupleMk.Query e with  
    | Some (tyargs,args) -> 
         let tyargsP = List.map RawTypes.dtype_to_Type tyargs 
         let tupTy = RawTypes.dtype_to_Type (RawTypes.tfTuple.Make((List.length tyargs,tyargs))) 
         let cons = tupTy.GetConstructor(tyargsP |> Array.of_list) 
         let argsP = args |> List.map (projE env) |> Array.of_list 
         //printf "efTupleMk: cons = %a, argR = %a\n" output_any cons output_any argsP;
         (Expression.New(cons,argsP) :> Expression)
    | _ -> 
    match Raw.efTupleGet.Query e with  
    | Some (info,tyargs,arg) -> 
         let tupTy = RawTypes.dtype_to_Type (RawTypes.tfTuple.Make((List.length tyargs,tyargs))) 
         let pname = sprintf "Item%d" (info.tupleGetIdx + 1) 
         let prop = tupTy.GetProperty(pname) |> nonNull ("projE: failed to bind tuple getter property '"^ pname ^"'") 
         (Expression.Property(projE env arg, prop) :> Expression)
    | _ -> 
    match Raw.efRecdMk.Query e with  
    | Some (info,tyargs,args) -> 
         let recdTy = RawTypes.dtypeNamedConstr_to_Type info.recdMkType tyargs 
         let consl = recdTy.GetConstructors() |> Array.to_list 
         match consl with 
         | [cons] -> (Expression.New(cons,args |> List.map (projE env) |> Array.of_list) :> Expression)
         | _ -> failwith "too many constructors found on F# record type"

    | _ -> 
    match Raw.efArrayMk.Query e with  
    | Some (ty,args) -> 
         let ety = RawTypes.dtype_to_Type ty 
         let argsP = args |> List.map (projE env) |> Array.of_list 
         (Expression.NewArrayInit(ety,argsP) :> Expression)
    | _ -> 
    match Raw.efPropGetCall.Query e with  
    | Some (p,tyargs,objarg) -> 
        let objargP = projE env objarg 
        let tc = p.propGetParent 
        let typ = RawTypes.dtype_to_Type(RawTypes.tfNamed.Make((tc,tyargs))) |> nonNull (sprintf "projE: failed to bind '%O'" tc) 
        let propGetter = typ.GetMethod(p.propGetMethName) |> nonNull ("projE: failed to bind property '"^ p.propGetMethName^"'") 
        (Expression.Property(objargP, propGetter) :> Expression)
    | _ -> 
    match Raw.efCtorCall.Query e with  
    | Some (ctor,tyargs,objargs) -> 
        let objargsP = map (projE env) objargs 
        let tc = ctor.ctorParent 
        let tyargsP = map RawTypes.dtype_to_Type tyargs
        let typ = RawTypes.dtype_to_Type(RawTypes.tfNamed.Make((tc,tyargs)))
        let argtyps = ctor.ctorArgTypes |> map (RawTypes.dtype_to_Type_open (List.nth tyargsP)) |> Array.of_list
        let ctor = typ.GetConstructor(argtyps) |> nonNull ("projE: failed to bind constructor") 
        (Expression.New(ctor,objargsP |> Array.of_list) :> Expression)
    | _ -> 
    match (fRefineRight Raw.efDelegateMk Raw.efLambdas).Query e with  
    | Some (dty,(vs,b)) -> 
        let vsP = map projVbind vs 
        let env = List.fold_right2 (fun v vP -> Map.add v.Raw.vName vP) vs vsP env 
        let bodyP = projE env b 
        let dtyP = RawTypes.dtype_to_Type dty 
        (Expression.Lambda(dtyP, bodyP, Array.of_list vsP) :> Expression) 
    | _ -> 
    match Raw.efMethodCall.Query e with  
    | Some (methInfo,tyargs,args) -> 
        let argsP = map (projE env) args 
        let tc = methInfo.methParent 
        let tcP = RawTypes.dtypeNamedConstr_to_TypeConstructor tc 
        let ntyargs = (tcP.GetGenericArguments()).Length 
        let ctyargs,mtyargs = splitAt ntyargs tyargs
        let nctyargs = List.length ctyargs
        let nmtyargs = List.length mtyargs
        let meth = 
            let ngmeth = Raw.bindMethod (fun env -> RawTypes.dtype_to_Type_open (List.nth env)) tcP (methInfo.methName,nmtyargs,methInfo.methArgTypes,methInfo.methRetType) 
            if (ngmeth.GetGenericArguments()).Length = 0 then ngmeth(* non generic *) 
            else ngmeth.MakeGenericMethod(Array.of_list (map RawTypes.dtype_to_Type mtyargs)) 
        let objR,argsP = if meth.IsStatic then ((null:Expression),argsP) else (match argsP with h::t -> h,t | _ -> failwith "no object argument found for instance method")
        //printf "** Call(%a, %a, %a)\n" output_any meth output_any objR output_any argsP;
        (Expression.Call(meth,objR, argsP |> Array.of_list) :> Expression)
    | None -> 
        printf "Could not project:\n%s\n" (any_to_string e);
        failwith "projE"

and projV env (v: Raw.exprVarName) = Map.find v env
and projVbind (v: Raw.exprVar) = 
    let tyR = RawTypes.dtype_to_Type v.vType
    let nm = v.vName.Text 
    //printf "** Expression .Parameter(%a, %a)\n" output_any tyR output_any nm;
    Expression .Parameter(tyR, nm)

//-------------------------------------------------------------------------

/// Project F# function expressions to Linq LambdaExpression nodes
let projFunc2 (e : ('a -> 'b) expr) : Expression<Func<'a,'b> > = 
    let s = to_raw e 
    match Raw.efLambdas.Query s with 
    | Some([v],body) -> 
        let vP = projVbind v 
        let env = Map.add v.vName vP Map.empty 
        let bodyP = projE env body 
        Expression.Lambda(bodyP,[| vP |])
    | _ -> failwith "QUOTE: Could not project (an explcit lambda must be provided)"
  
/// Project F# function expressions to Linq LambdaExpression nodes
let projFunc3 (e : ('a -> 'b -> 'c) expr) : Expression<Func<'a,'b,'c> > = 
    let s = to_raw e 
    match Raw.efLambdas.Query s with 
    | Some([v1;v2],body) -> 
        let v1P = projVbind v1 
        let v2P = projVbind v2 
        let env = Map.add v1.vName v1P (Map.add v2.vName v2P  Map.empty) 
        Expression.Lambda(projE env body,[| v1P; v2P |])
    | _ -> failwith "QUOTE: Could not project (an explcit lambda must be provided)"
  
//-------------------------------------------------------------------------
// F# - DLinq operators

type nullableDecimal = Nullable<Decimal>

module Query = begin

    let (|>) x f = f x

    let it (coll :> IQueryable<'a>) = coll //projFunc2 « fun x -> x »

    let orderBy qf (coll :> IQueryable<'a>) = 
      System.Query.Queryable.OrderBy(coll,projFunc2 (expandLINQ qf))

    let select (qf : ('a -> 'b) expr) (coll :> IQueryable<'a>) = 
      System.Query.Queryable.Select(coll,projFunc2 (expandLINQ qf))

    let groupBy (qf1 : ('a -> 'b) expr) (coll :> IQueryable<'a>) = 
      System.Query.Queryable.GroupBy(coll,projFunc2 (expandLINQ qf1))

    let groupByAndProject (qf1 : ('a -> 'b) expr) (qf2 : ('a -> 'c) expr) (coll :> IQueryable<'a>) = 
      System.Query.Queryable.GroupBy(coll,projFunc2 (expandLINQ qf1),projFunc2 (expandLINQ qf2))

    let first qf (coll :> IQueryable<'a>) : 'c = 
      System.Query.Queryable.First(coll,projFunc2 (expandLINQ qf))

    let take n (coll :> IQueryable<'a>) = 
      System.Query.Queryable.Take(coll,n)

    let fmin (qf : ('a -> ('c :> IComparable<'c>)) expr) (coll :> IQueryable<'a>) : 'c = 
      System.Query.Queryable.Min(coll,projFunc2 (expandLINQ qf))

    let min (coll :> IQueryable<'a>) : 'c = 
      System.Query.Queryable.Min(coll)

    let averagem (coll :> IQueryable<Decimal>) : 'c = 
      System.Query.Queryable.Average(coll :> IQueryable<Decimal>)

    let averageNm (coll :> IQueryable<nullableDecimal>) : 'c = 
      System.Query.Queryable.Average(coll:> IQueryable<nullableDecimal>)

    let selectMany qf (coll :> IQueryable<'a>) = 
      System.Query.Queryable.SelectMany(coll,projFunc2 (expandLINQ qf))

    let where qf (coll :> IQueryable<'a>) = 
      System.Query.Queryable.Where(coll,projFunc2 (expandLINQ qf))

    let distinct (coll :> IQueryable<'a>) = 
      System.Query.Queryable.Distinct(coll)

    let taken n (coll :> IQueryable<'a>) = 
      System.Query.Queryable.Take(coll,n)


    let iter f (coll :> IEnumerable<'a>) = 
      Idioms.foreachG coll f 

end

