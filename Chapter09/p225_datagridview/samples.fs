#light
#r "Microsoft.Practices.EnterpriseLibrary.Data.dll";;
open System
open System.Collections.Generic
open System.Data
open System.Windows.Forms
open Microsoft.Practices.EnterpriseLibrary.Data

let database = DatabaseFactory.CreateDatabase()
let dataSet = database.ExecuteDataSet
                        (CommandType.Text,
                          "select top 10 * from Person.Contact")
let form =
    let temp = new Form()
    let grid = new DataGridView(Dock = DockStyle.Fill)
    temp.Controls.Add(grid)
    grid.DataSource <- dataSet.Tables.Item(0)
    temp
    
Application.Run(form)