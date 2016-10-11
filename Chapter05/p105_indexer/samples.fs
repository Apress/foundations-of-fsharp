#light
type Indexers(vals:string[]) = class
    member x.Item
        with get (y) = vals.[y]
        and set (y, z) = vals.[y] <- z
    member x.MyString
        with get (y) = vals.[y]
        and set (y, z) = vals.[y] <- z
end

let index = new Indexers [|"One"; "Two"; "Three"; "Four"|]

index.[0] <- "Five";
index.Item(2) <- "Six";
index.MyString(3) <- "Seven";

print_endline index.[0]
print_endline (index.Item(1))
print_endline (index.MyString(2))
print_endline (index.MyString(3))
