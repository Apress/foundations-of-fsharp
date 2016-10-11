#light
type recipe =
    { recipeName : string ;
    ingredients : ingredient list ;
    instructions : string }
and ingredient =
    { ingredientName : string ;
    quantity : int }
let greenBeansPineNuts =
    { recipeName = "Green Beans & Pine Nuts" ;
     ingredients =
        [{ ingredientName = "Green beans" ; quantity = 250 };
        { ingredientName = "Pine nuts" ; quantity = 250 };
        { ingredientName = "Feta cheese" ; quantity = 250 };
        { ingredientName = "Olive oil" ; quantity = 10 };
        { ingredientName = "Lemon" ; quantity = 1 }] ;
        instructions = "Parboil the green beans for about 7 minutes. Roast the pine
        nuts carefully in a frying pan. Drain the beans and place in a salad bowl
        with the roasted pine nuts and feta cheese. Coat with the olive oil
        and lemon juice and mix well. Serve ASAP." }
        
let name = greenBeansPineNuts.recipeName
let toBuy =
    List.fold_left
        (fun acc x ->
            acc +
            (Printf.sprintf "\t%s - %i\r\n" x.ingredientName x.quantity) )
        "" greenBeansPineNuts.ingredients
        
let instructions = greenBeansPineNuts.instructions
printf "%s\r\n%s\r\n\r\n\t%s" name toBuy instructions
