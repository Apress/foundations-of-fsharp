#light
open System
open System.IO
open System.Net
open System.Net.Sockets
open System.Threading
open System.Collections.Generic

type ClientTable() = class
    let clients = new Dictionary<string,StreamWriter>()

    /// Add a client and it's its stream writer 
    member t.Add(name,sw:StreamWriter) = 
        lock clients (fun () -> 
            if clients.ContainsKey(name) then 
                sw.WriteLine("ERROR - Name in use already!")
                sw.Close()
            else 
                clients.Add(name,sw))

    /// Remove a client and close it, if no one else has done that first
    member t.Remove(name) = 
        lock clients (fun () -> clients.Remove(name) |> ignore)

    /// Grab a copy of the current list of clients. 
    member t.Current = 
        lock clients (fun () -> clients.Values |> Seq.to_array)        

    /// Check if whether a client exists
    member t.ClientExists(name) = 
        lock clients (fun () -> clients.ContainsKey(name))

    
end

type Server() = class

    let clients = new ClientTable()
    
    let sendMessage name message =
        let combinedMessage = 
            Printf.sprintf "%s: %s" name message
        for sw in clients.Current do 
            try
                lock sw (fun () -> 
                    sw.WriteLine(combinedMessage) 
                    sw.Flush())
            with
            | _ -> () // Some clients may fail 

    let emptyString s = (s = null || s = "") 

    let handleClient (connection : TcpClient) =
        let stream = connection.GetStream()
        let sr = new StreamReader(stream)
        let sw = new StreamWriter(stream)
        let rec requestAndReadName() = 
            sw.WriteLine("What is your name? "); 
            sw.Flush()
            let rec readName() = 
                let name = sr.ReadLine()
                if emptyString(name) then 
                    readName()
                else
                    name
            let name = readName()
            if clients.ClientExists(name) then
                sw.WriteLine("ERROR - Name in use already!")
                sw.Flush() 
                requestAndReadName()
            else 
                name
        let name = requestAndReadName()
        clients.Add(name,sw)
            
        let rec listen() = 
            let text = try Some(sr.ReadLine()) with _ -> None
            match text with 
            | Some text -> 
                if not (emptyString(text)) then 
                    sendMessage name text
                Thread.Sleep(1)
                listen()
            | None -> 
                clients.Remove name
                sw.Close()

        listen()

    let server = new TcpListener(IPAddress.Loopback, 4242)

    let rec handleConnections() = 
        server.Start()
        if (server.Pending()) then
            let connection = server.AcceptTcpClient()
            printf "New Connection" 
            let t = new Thread(fun () -> handleClient connection) 
            t.Start() 
        Thread.Sleep(1);
        handleConnections()
        
    member server.Start() = handleConnections()
end
(new Server()).Start()
