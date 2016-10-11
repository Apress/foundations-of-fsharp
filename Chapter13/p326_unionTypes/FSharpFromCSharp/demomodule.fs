#light
module Strangelights.DemoModule

open System

let rand = new Random()

type Quantity =
| Discrete of int
| Continuous of float

let getRandomQuantity() =
    match rand.Next(1) with
    | 0 -> Quantity.Discrete (rand.Next())
    | _ -> 
        Quantity.Continuous 
            (rand.NextDouble() * float_of_int (rand.Next()))

type EasyQuantity =
| Discrete of int
| Continuous of float
    with
        member x.ToFloat() =
            match x with
            | Discrete x -> float_of_int x
            | Continuous x -> x
        member x.ToInt() =
            match x with
            | Discrete x -> x
            | Continuous x -> int_of_float x
    end

let getRandomEasyQuantity() =
    match rand.Next(1) with
    | 0 -> EasyQuantity.Discrete (rand.Next())
    | _ -> 
        EasyQuantity.Continuous 
            (rand.NextDouble() * float_of_int (rand.Next()))

