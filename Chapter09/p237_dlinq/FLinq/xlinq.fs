// Copyright (c) Microsoft Corporation 2005-2006.
// This sample code is provided "as is" without warranty of any kind. 
// We disclaim all warranties, either express or implied, including the 
// warranties of merchantability and fitness for a particular purpose. 
//

#light

module Microsoft.FSharp.Bindings.XLinq

open System
open System.Query
open System.Collections.Generic

module SequenceOps = begin

    let elements s (coll :> IEnumerable<_>) = 
        System.Xml.XLinq.XElementSequence.Elements(coll,s)

    let ancestors s (coll :> IEnumerable<_>) = 
        System.Xml.XLinq.XElementSequence.Ancestors(coll,s)

    let descendants s (coll :> IEnumerable<_>) = 
        System.Xml.XLinq.XElementSequence.Descendants(coll,s)

    let selfAndAncestors s (coll :> IEnumerable<_>) = 
        System.Xml.XLinq.XElementSequence.SelfAndAncestors(coll,s)

    let selfAndDescendants s (coll :> IEnumerable<_>) = 
        System.Xml.XLinq.XElementSequence.SelfAndDescendants(coll,s)

    let xname n = System.Xml.XLinq.XName.op_Implicit(n)

    let attribute (s:string) (e: System.Xml.XLinq.XElement) = 
        e.Attribute(xname s)

    let attribute_to_string (e: System.Xml.XLinq.XAttribute) = 
        (System.Xml.XLinq.XAttribute.op_Explicit(e) : string)

    let element_to_string (e: System.Xml.XLinq.XElement) = 
        (System.Xml.XLinq.XElement.op_Explicit(e) : string)

    let element_to_DateTime (e: System.Xml.XLinq.XElement) = 
        (System.Xml.XLinq.XElement.op_Explicit(e) : DateTime)

    let element_to_int (e: System.Xml.XLinq.XElement) = 
        (System.Xml.XLinq.XElement.op_Explicit(e) : int)

    let xargs l = Idioms.ParamArray (List.map box l)
    
end
