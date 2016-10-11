#light
open System.Diagnostics
open System.Net
open System.Xml

let getUrlAsXml (url : string) =
    let request = WebRequest.Create(url)
    let response = request.GetResponse()
    let stream = response.GetResponseStream()
    let xml = new XmlDocument()
    xml.Load(new XmlTextReader(stream))
    xml
    
let url = "http://newsrss.bbc.co.uk/rss/newsonline_uk_edition/sci/tech/rss.xml"
let xml = getUrlAsXml url

let mutable i = 1
for node in xml.SelectNodes("/rss/channel/item/title") do
    printf "%i. %s\r\n" i node.InnerText
    i <- i + 1
    
let item = read_int()

let newUrl =
    let xpath = sprintf "/rss/channel/item[%i]/link" item
    let node = xml.SelectSingleNode(xpath)
    node.InnerText
let proc = new Process()
proc.StartInfo.UseShellExecute <- true
proc.StartInfo.FileName <- newUrl
proc.Start()
