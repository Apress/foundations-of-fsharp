#light
namespace Strangelights.WebServices

open System.Web.Services

[<WebService(Namespace = 
    "http://strangelights.com/FSharp/Foundations/DnaWebService")>]
type DnaWebService = class
    inherit WebService
    new() = {}
    [<WebMethod(Description = "Gets a representation of a yeast molecule")>]
    member x.GetYeastMolecule () =
        let yeast = new molecule(id = "Yeast-tRNA-Phe")
        let id = new identity(name = "Saccharomyces cerevisiae tRNA-Phe")
        let tax = new taxonomy(domain = "Eukaryota", kingdom = "Fungi", 
                               phylum = "Ascomycota", ``class`` = "Saccharomycetes", 
                               order = "Saccharomycetales", 
                               family = "Saccharomycetaceae", 
                               genus = "Saccharomyces", 
                               species = "Saccharomyces cerevisiae")
        let numRange1 = new numberingrange(start = "1", Item = "10")
        let numRange2 = new numberingrange(start = "11", Item = "66")
        let numSys = new numberingsystem(id="natural", usedinfile=true)
        numSys.Items <- [|box numRange1; box numRange2|]
        let seqData = new seqdata()
        seqData.Value <- "GCGGAUUUAG CUCAGUUGGG AGAGCGCCAG ACUGAAGAUC 
          UGGAGGUCCU GUGUUCGAUC CACAGAAUUC GCACCA"
        let seq = new sequence()
        seq.numberingsystem <- [|numSys|]
        seq.seqdata <- seqData
        id.taxonomy <- tax
        yeast.identity <- id
        yeast.sequence <- [|seq|]
        yeast
end