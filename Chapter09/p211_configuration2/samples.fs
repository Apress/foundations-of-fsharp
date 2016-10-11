#light
#r "System.Configuration.dll";;
open System.Configuration

let config =
    ConfigurationManager.OpenMachineConfiguration()
    
for x in config.Sections do
    print_endline x.SectionInformation.Name