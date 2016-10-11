#light
#r "System.Configuration.dll";;
open System.Configuration

let connectionStringDetails =
    ConfigurationManager.ConnectionStrings.Item("MyConnectionString")
    
let connectionString = connectionStringDetails.ConnectionString
let providerName = connectionStringDetails.ProviderName

printfn "%s\r\n%s"
    connectionString
    providerName