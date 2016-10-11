#light
try
    if System.DateTime.Now.Second % 3 = 0 then
        raise (new System.Exception())
    else
        raise (new System.ApplicationException())
with
| :? System.ApplicationException ->
    print_endline "A second that was not a multiple of 3"
| _ ->
    print_endline "A second that was a multiple of 3"
