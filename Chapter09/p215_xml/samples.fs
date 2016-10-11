#light
open System.Xml
let animals = [ "ants", "6"; "spiders", "8"; "cats", "4" ]

let animalsDoc =
    let temp = new XmlDocument()
    let root = temp.CreateElement("animals")
    temp.AppendChild(root) |> ignore
    animals
    |> List.iter (fun x ->
        let element = temp.CreateElement(fst x)
        element.InnerText <- (snd x)
        root.AppendChild(element) |> ignore )
    temp
    
animalsDoc.Save("animals.xml")