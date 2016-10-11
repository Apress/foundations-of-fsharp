#light
open System
open System.Threading

// function to print to the console character by character
// this increases the chance of there being a context switch
// between threads.
let printSlowly (s : string) =
    s.ToCharArray()
    |> Array.iter print_char
    
// create a thread that prints to the console in an unsafe way
let makeUnsafeThread() =
    new Thread(fun () ->
    for x = 1 to 100 do
        printSlowly "One ... Two ... Three ... "
        print_newline()
    done)
    
// the object that will be used as a lock
let lockObj = new Object()

// create a thread that prints to the console in a safe way
let makeSafeThread() =
    new Thread(fun () ->
        for x = 1 to 100 do
            // use lock to ensure operation is atomic
            lock lockObj (fun () ->
                printSlowly "One ... Two ... Three ... "
                print_newline())
        done)
        
// helper function to run the test to
let runTest (f : unit -> Thread) message =
    print_endline message;
    let t1 = f() in
    let t2 = f() in
    t1.Start()
    t2.Start()
    t1.Join()
    t2.Join()
    
// runs the demonstrations
let main() =
    runTest
        makeUnsafeThread
        "Running test without locking ..."
    runTest
        makeSafeThread
        "Running test with locking ..."
    
main ()
