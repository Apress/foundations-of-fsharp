#light
#r "Microsoft.Practices.EnterpriseLibrary.Data.dll";;
open System
open Microsoft.Practices.EnterpriseLibrary.Data

let database = DatabaseFactory.CreateDatabase()

let reader = database.ExecuteReader(
        "uspGetBillOfMaterials",
        [| box 316; box (new DateTime(2006,1,1)) |])
        
while reader.Read() do
    for x = 0 to (reader.FieldCount - 1) do
        printfn "%s = %O"
            (reader.GetName(x))
            (reader.Item(x))

