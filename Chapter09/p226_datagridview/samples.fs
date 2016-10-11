#light
#r "Microsoft.Practices.EnterpriseLibrary.Data.dll";;
open System
open System.Collections.Generic
open System.Data
open System.Windows.Forms
open Microsoft.Practices.EnterpriseLibrary.Data

let execCommand<'a> commandString : seq<'a> =
    let opener() =
        let database = DatabaseFactory.CreateDatabase()
        database.ExecuteReader(CommandType.Text, commandString)
        
    let generator (reader : IDataReader) =
        if reader.Read() then
            let t = (type 'a)
            let props = t.GetProperties()
            let types =
                props
                |> Seq.map (fun x -> x.PropertyType)
                |> Seq.to_array
            let cstr = t.GetConstructor(types)
            let values = Array.create reader.FieldCount (new obj())
            reader.GetValues(values) |> ignore
            let values =
                values
                |> Array.map
                    (fun x -> match x with | :? DBNull -> null | _ -> x)
            Some (cstr.Invoke(values) :?> 'a)
        else
            None
            
    Seq.generate_using
        opener
        generator

type Contact =
    {
        ContactID : Nullable<int> ;
        NameStyle : Nullable<bool> ;
        Title : string ;
        FirstName : string ;
        MiddleName : string ;
        LastName : string ;
        Suffix : string ;
        EmailAddress : string ;
        EmailPromotion : Nullable<int> ;
        Phone: string ;
        PasswordHash : string ;
        PasswordSalt : string ;
        AdditionalContactInfo : string ;
        rowguid : Nullable<Guid> ;
        ModifiedDate : Nullable<DateTime> ;
    }

let form =
    let temp = new Form()
    let grid = new DataGridView(Dock = DockStyle.Fill)
    temp.Controls.Add(grid)
    let contacts =
        execCommand<Contact> "select top 10 * from Person.Contact"
    let contactsArray = contacts |> Seq.to_array
    grid.DataSource <- contactsArray
    temp
    
Application.Run(form)