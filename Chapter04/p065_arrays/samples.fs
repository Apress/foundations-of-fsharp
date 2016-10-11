#light
let pascalsTriangle = [|
    [|1|];
    [|1; 1|];
    [|1; 2; 1|];
    [|1; 3; 3; 1|];
    [|1; 4; 6; 4; 1|];
    [|1; 5; 10; 10; 5; 1|];
    [|1; 6; 15; 20; 15; 6; 1|];
    [|1; 7; 21; 35; 35; 21; 7; 1|];
    [|1; 8; 28; 56; 70; 56; 28; 8; 1|];
    |]
    
let numbers =
    let length = (Array.length pascalsTriangle) in
    let temp = Array2.create 3 length 0 in
    for index = 0 to length - 1 do
        let naturelIndex = index - 1 in
        if naturelIndex >= 0 then
            temp.[0, index] <- pascalsTriangle.[index].[naturelIndex];
        let triangularIndex = index - 2 in
        if triangularIndex >= 0 then
            temp.[1, index] <- pascalsTriangle.[index].[triangularIndex];
        let tetrahedralIndex = index - 3 in
        if tetrahedralIndex >= 0 then
            temp.[2, index] <- pascalsTriangle.[index].[tetrahedralIndex]
    done
    temp

print_any numbers

