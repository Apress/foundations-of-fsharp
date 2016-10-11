#light
#r "Microsoft.Practices.EnterpriseLibrary.Data.dll";;
#r "flinq.dll";;
#r "AdventureWorks.dll";; 
#r "System.Data.DLinq.dll";; 
#r "System.Query.dll";;

open System.Windows.Forms
open Microsoft.FSharp.Quotations.Typed
open Microsoft.FSharp.Bindings.DLinq.Query
open Microsoft.Practices.EnterpriseLibrary.Data

module sOps = Microsoft.FSharp.Bindings.Linq.SequenceOps

let database = DatabaseFactory.CreateDatabase()
let adventureWorks = new AdventureWorks(database.CreateConnection())

type Person = 
    { Title : string ; 
      FirstName : string ; 
      LastName : string ; }

let contacts =
    adventureWorks.Person.Contact
    |> where « fun c -> c.FirstName = "Robert" »
    |> sOps.select 
        (fun c -> 
            { Title = c.Title ; 
              FirstName = c.FirstName; 
              LastName = c.LastName })
    |> IEnumerable.to_array 

let form =
    let temp = new Form()
    let grid = new DataGridView()
    grid.Dock <- DockStyle.Fill
    temp.Controls.Add(grid)
    grid.DataSource <- contacts
    temp

Application.Run(form)