#light
#r "Proxy.dll";;

let key = "xxxx xxxx xxxx xxxx"

let google = 
    let temp = new GoogleSearchService()
    temp.Url <- "http://site403.mysite4now.com/robertpi/site1/evilapi/gateway.pl"
    temp

try
    let result = 
        google.doGoogleSearch(key=key, 
                                q="FSharp", 
                                start=0, 
                                maxResults=3, 
                                filter=false, 
                                restrict="", 
                                safeSearch=false, 
                                lr="", 
                                ie="", 
                                oe="")

    result.resultElements
    |> Array.iteri 
        (fun i result -> 
            printf "%i. %s\r\n%s\r\n%s\r\n\r\n" 
                i
                result.title
                result.URL 
                result.snippet)

    read_line() |> ignore
with :? System.Net.WebException as ex -> 
    let reader = new System.IO.StreamReader(ex.Response.GetResponseStream())
    print_endline (reader.ReadToEnd())
