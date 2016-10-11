#light
open System.Xml

let fruitsDoc =
    let temp = new XmlDocument()
    temp.Load("fruits.xml")
    temp
    
let fruits = fruitsDoc.SelectNodes("/fruits/*")

for x in fruits do
    printfn "%s = %s " x.Name x.InnerText
