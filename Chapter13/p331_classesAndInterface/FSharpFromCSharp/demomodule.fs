#light
module Strangelights.DemoModule
open System
let hourAndMinute (time : DateTime) = time.Hour, time.Minute

let hour (time : DateTime) = time.Hour
let minute (time : DateTime) = time.Minute
//-----------------------------------------------------------
#light
open System
open System.Collections.Generic

let filterStringList f (l : List<string>) = l |> Seq.filter f

let filterStringListDelegate 
    (del : Predicate<string>) 
    (l : List<string>) = 
        let f x = del.Invoke(x)
        new List<string>(l |> Seq.filter f)

//-----------------------------------------------------------
#light
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

type HardQuantity =
| None
| Discrete of int
| Continuous of float
    with
        member x.ToFloat() =
            match x with
            | None -> nan
            | Discrete x -> float_of_int x
            | Continuous x -> x
    end

let getRandomHardQuantity() =
    match rand.Next(2) with
    | 0 -> HardQuantity.None
    | 1 -> HardQuantity.Discrete (rand.Next())
    | _ -> 
        HardQuantity.Continuous 
            (rand.NextDouble() * float_of_int (rand.Next()))

let getList() =
    [1; 2; 3]