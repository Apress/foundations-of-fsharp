#light
open System.Collections.Generic
open Strangelights.ExpressionParser.Ast

// requesting a value for variable from the user
let getVariableValues e =
    let rec getVariableValuesInner input (variables : Map<string, float>) =
        match input with
        | Ident (s) ->
            match variables.TryFind(s) with
            | Some _ -> variables
            | None ->
                printf "%s: " s
                let v = read_float()
                variables.Add(s,v)
        | Multi (e1, e2) -> 
            variables
            |> getVariableValuesInner e1
            |> getVariableValuesInner e2
        | Div (e1, e2) -> 
            variables
            |> getVariableValuesInner e1
            |> getVariableValuesInner e2
        | Plus (e1, e2) ->
            variables
            |> getVariableValuesInner e1
            |> getVariableValuesInner e2
        | Minus (e1, e2) ->
            variables
            |> getVariableValuesInner e1
            |> getVariableValuesInner e2
        | _ -> variables
    getVariableValuesInner e (Map.Empty())

// function to handle the interpretation
let interpret input (variableDict : Map<string,float>) = 
    let rec interpretInner input =
        match input with
        | Ident (s) -> variableDict.[s] 
        | Val (v) -> v
        | Multi (e1, e2) -> (interpretInner e1) * (interpretInner e2)
        | Div (e1, e2) -> (interpretInner e1) / (interpretInner e2)
        | Plus (e1, e2) -> (interpretInner e1) + (interpretInner e2)
        | Minus (e1, e2) -> (interpretInner e1) - (interpretInner e2)
    interpretInner input
    
// request input from user and interpret it
printf "input expression: "
let lexbuf = Lexing.from_string (read_line())
let e = Pars.Expression Lex.token lexbuf
let args = getVariableValues e
let v = interpret e args
printf "result: %f" v
read_line()