#light
#r "System.Configuration.dll";;
open System.Configuration

let setting = ConfigurationManager.AppSettings.Item("MySetting")

print_string setting