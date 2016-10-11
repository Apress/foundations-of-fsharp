#light
#r "System.Configuration.dll";;
open System.Configuration
open System.Collections.Generic
open System.Data
open System.Data.SqlClient
open System.Data.Common
open System

/// Create and open an SqlConnection object using the connection string found
/// in the configuration file for the given connection name
let openSQLConnection(connName:string) =
    let connSetting = ConfigurationManager.ConnectionStrings.Item(connName)
    let connString = connSetting.ConnectionString
    let conn = new SqlConnection(connString)
    conn.Open();
    conn

/// Create and execute a read command for a connection using
/// the connection string found in the configuration file
/// for the given connection name
let openConnectionReader connName cmdString =
    let conn = openSQLConnection(connName)
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

let execCommand (connName : string) (cmdString : string) =
    Seq.generate_using
        // This function gets called to open a connection and create a reader
        (fun () -> openConnectionReader connName cmdString)
        // This function gets called to read a single item in
        // the enumerable for a reader/connection pair
        (fun reader -> readOneRow(reader))

let contactsTable =
    execCommand
        "MyConnection"
        "select * from Person.Contact"
        
for row in contactsTable do
    for col in row.Keys do
        printfn "%s = %O" col (row.Item(col))
