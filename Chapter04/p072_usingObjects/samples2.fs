#light
open System
let intList =
    let temp = new ResizeArray<int>() in
    temp.AddRange([| 1 ; 2 ; 3 |]);
    temp
    
intList.ForEach( fun i -> Console.WriteLine(i) )