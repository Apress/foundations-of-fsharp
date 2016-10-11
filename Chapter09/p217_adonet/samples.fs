#light
#r "System.Configuration.dll";;
open System.Configuration
open System.Data
open System.Data.SqlClient

let connectionSetting =
    ConfigurationManager.ConnectionStrings.Item("MyConnection")
    
let connectionString =
    connectionSetting.ConnectionString
    
using (new SqlConnection(connectionString))
    (fun connection ->
        let command =
            let temp = connection.CreateCommand()
            temp.CommandText <- "select * from Person.Contact"
            temp.CommandType <- CommandType.Text
            temp
            
        connection.Open()
        
        using (command.ExecuteReader())
            (fun reader ->
                let title = reader.GetOrdinal("Title")
                let firstName = reader.GetOrdinal("FirstName")
                let lastName = reader.GetOrdinal("LastName")
                let getString (r : #IDataReader) x =
                    if r.IsDBNull(x) then
                        ""
                    else
                        r.GetString(x)
                while reader.Read() do
                    printfn "%s %s %s"
                        (getString reader title )
                        (getString reader firstName)
                        (getString reader lastName)))
