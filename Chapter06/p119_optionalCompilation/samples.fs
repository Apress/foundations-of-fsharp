#light
open Microsoft.FSharp.Compatibility
#if FRAMEWORK_AT_LEAST_2_0
let getArray() = [|1; 2; 3|]
#else
let getArray() = CompatArray.of_array [|1; 2; 3|]
#endif
