#light
open System.Collections.Generic
open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.Typed

let interpret exp =
    let uexp = to_raw exp
    let operandsStack = new Stack<int>()
    let rec interpretInner uexp =
        match uexp with
        | Raw.Apps(op, args) when args.Length > 0 ->
            args |> List.iter (fun x -> interpretInner x)
            interpretInner op
        | _ ->
        match uexp with
        | Raw.Int32 x ->
            printf "Push: %i\r\n" x
            operandsStack.Push(x)
        | Raw.AnyTopDefnUse(def, types) ->
            let preformOp f name =
                let x, y = operandsStack.Pop(), operandsStack.Pop()
                printf "%s %i, %i\r\n" name x y
                let result = f x y
                operandsStack.Push(result)
            let _,name = def.Path
            match name with
            | "op_Addition" ->
                let f x y = x + y
                preformOp f "Add"
            | "op_Subtraction" ->
                let f x y = y - x
                preformOp f "Sub"
            | "op_Multiply" ->
                let f x y = y * x
                preformOp f "Multi"
            | "op_Division" ->
                let f x y = y / x
                preformOp f "Div"
            | _ -> failwith "not a valid op"
        | _ -> failwith "not a valid op"
    interpretInner uexp
    printfn "Result: %i" (operandsStack.Pop())
    
interpret « (2 * (2 - 1)) / 2 »
read_line()
