#light
open System.Collections.Generic
open System.Reflection
open System.Reflection.Emit
open Strangelights.ExpressionParser.Ast

// get a list of all the parameter names
let rec getParamList e = 
    let rec getParamListInner e names = 
        match e with
        | Ident name -> 
            if not (List.exists (fun s -> s = name) names) then 
                name :: names
            else
                names
        | Multi (e1 , e2) -> 
            names
            |> getParamListInner e1
            |> getParamListInner e2 
        | Div (e1 , e2) -> 
            names
            |> getParamListInner e1
            |> getParamListInner e2 
        | Plus (e1 , e2) -> 
            names
            |> getParamListInner e1
            |> getParamListInner e2 
        | Minus (e1 , e2) -> 
            names
            |> getParamListInner e1
            |> getParamListInner e2 
        | _ -> names
    getParamListInner e []

// create the dyncamic method
let createDynamicMethod e (paramNames: string list) =
    let generateIl e (il : ILGenerator) =
        let rec generateIlInner e  = 
            match e with
            | Ident name -> 
                let index = List.find_index (fun s -> s = name) paramNames
                il.Emit(OpCodes.Ldarg, index)
            | Val x -> il.Emit(OpCodes.Ldc_R8, x)
            | Multi (e1 , e2) -> 
                generateIlInner e1
                generateIlInner e2
                il.Emit(OpCodes.Mul)
            | Div (e1 , e2) -> 
                generateIlInner e1
                generateIlInner e2
                il.Emit(OpCodes.Div)
            | Plus (e1 , e2) -> 
                generateIlInner e1
                generateIlInner e2
                il.Emit(OpCodes.Add)
            | Minus (e1 , e2) -> 
                generateIlInner e1
                generateIlInner e2
                il.Emit(OpCodes.Sub)
        generateIlInner e
        il.Emit(OpCodes.Ret)

    let paramsTypes = Array.create paramNames.Length (type float)
    let meth = MethodInfo.GetCurrentMethod()
    let temp = new DynamicMethod("", (type float), paramsTypes, meth.Module)
    let il = temp.GetILGenerator()
    generateIl e il
    temp

let collectArgs (paramNames : string list) =
    paramNames
    |> IEnumerable.map 
        (fun n -> 
            printf "%s: " n
            box (read_float()))
    |> Array.of_seq

printf "input expression: "
let lexbuf = Lexing.from_string (read_line())
let e = Pars.Expression Lex.token lexbuf
let paramNames = getParamList e
let dm = createDynamicMethod e paramNames
let args = collectArgs paramNames
printf "result: %O" (dm.Invoke(null, args))
read_line()
