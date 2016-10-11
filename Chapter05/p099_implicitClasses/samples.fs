type Counter(start, increment, length) = class
    let finish = start + length
    let mutable current = start
    member obj.Current = current
    member obj.Increment() =
        if current > finish then failwith "finished!";
            current <- current + increment
end
