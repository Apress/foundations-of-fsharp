#light
#I @"C:\Program Files\FirebirdClient";;
#r @"FirebirdSql.Data.FirebirdClient.dll";;
open System.Configuration
open System.Collections.Generic
open System.Data
open FirebirdSql.Data.FirebirdClient;
open System.Data.Common
open System

let connectionString =
    @"Database=C:\Program Files\Firebird\" +
    @"Firebird_2_0\examples\empbuild\EMPLOYEE.FDB;" +
    @"User=SYSDBA;" + "Password=masterkey;" +
    @"Dialect=3;" + "Server=localhost";
    
let openFBConnection() =
    let connection = new FbConnection (connectionString)
    connection.Open();
    connection
    
let openConnectionReader cmdString =
    let conn = openFBConnection()
    let cmd = conn.CreateCommand(CommandText=cmdString,
                                    CommandType = CommandType.Text)
    let reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    reader
    
let readOneRow (reader: #DbDataReader) =
    if reader.Read() then
        let dict = new Dictionary<string, obj>()
        for x = 0 to (reader.FieldCount - 1) do
            dict.Add(reader.GetName(x), reader.Item(x))
        Some(dict)
    else
        None
        
let execCommand (cmdString : string) =
    Seq.generate_using
        // This function gets called to open a conn and create a reader
        (fun () -> openConnectionReader cmdString)
        // This function gets called to read a single item in
        // the enumerable for a reader/conn pair
        (fun reader -> readOneRow(reader))
        
let employeeTable =
    execCommand
        "select * from Employee"
        
for row in employeeTable do
    for col in row.Keys do
        printfn "%s = %O " col (row.Item(col))
