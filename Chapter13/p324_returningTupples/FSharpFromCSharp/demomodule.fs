#light
module Strangelights.DemoModule
open System
let hourAndMinute (time : DateTime) = time.Hour, time.Minute

let hour (time : DateTime) = time.Hour
let minute (time : DateTime) = time.Minute
