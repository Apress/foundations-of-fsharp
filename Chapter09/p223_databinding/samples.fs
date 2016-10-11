#light
#r "Microsoft.Practices.EnterpriseLibrary.Data.dll";;
open System
open System.Collections.Generic
open System.Data
open System.Windows.Forms
open Microsoft.Practices.EnterpriseLibrary.Data

let opener commandString =
    let database = DatabaseFactory.CreateDatabase()
    database.ExecuteReader(CommandType.Text, commandString)
    
let generator (reader : IDataReader) =
    if reader.Read() then
        let dict = new Dictionary<string, obj>()
        for x = 0 to (reader.FieldCount - 1) do
            dict.Add(reader.GetName(x), reader.Item(x))
        Some(dict)
    else
        None
let execCommand (commandString : string) =
    Seq.generate_using
        (fun () -> opener commandString)
        (fun r -> generator r)
        
let contactsTable =
    execCommand
        "select top 10 * from Person.Contact"
let contacts =
    [| for row in contactsTable ->
        Printf.sprintf "%O %O"
            (row.Item("FirstName"))
            (row.Item("LastName")) |]
let form =
    let temp = new Form()
    let combo = new ComboBox(Top=8, Left=8, DataSource=contacts)
    temp.Controls.Add(combo)
    temp
    
Application.Run(form)
