#light

#I @"C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.0" ;;
#r @"PresentationCore.dll" ;;
#r @"PresentationFramework.dll" ;;
#r @"WindowsBase.dll" ;;

open System
open System.Collections.Generic 
open System.IO
open System.Windows
open System.Windows.Controls
open System.Windows.Markup
open System.Windows.Media
open System.Windows.Media.Media3D
open System.Windows.Threading
open System.Xml


// creates the window and loads given the Xaml file into it
let createWindow (file : string) = 
    Idioms.using  (XmlReader.Create(file))
        (fun stream -> 
            let temp = XamlReader.Load(stream) :?> Window
            temp.Height <- 400.0
            temp.Width <- 400.0
            temp.Title <- "F# meets Xaml"
            temp)

// finds all the MeshGeometry3D in a given 3D view port
let findMeshes ( viewport : Viewport3D ) = 
    viewport.Children 
    |> Seq.choose 
        (function :? ModelVisual3D as c -> Some(c.Content) | _ -> None)
    |> Seq.choose 
        (function :? Model3DGroup as mg -> Some(mg.Children) | _ -> None)
    |> Seq.concat
    |> Seq.choose 
        (function :? GeometryModel3D as mg -> Some(mg.Geometry) | _ -> None)
    |> Seq.choose 
        (function :? MeshGeometry3D as mv -> Some(mv) | _ -> None)

// loop function to create all items necessary for a plane
let createPlaneItemList f (xRes : int) (yRes : int) = 
    let list = new List<_>() 
    for x = 0 to xRes - 1 do
        for y = 0 to yRes - 1 do
            f list x y
    list

// function to initalise a point
let point x y = new Point(x, y)
// function to initalise a "d point
let point3D x y = new Point3D(x, y, 0.0)

// create all the points necessary for a square in the plane
let createSquare 
    f (xStep : float) (yStep : float) (list : List<_>) (x : int) (y : int) = 
    let x' =  Float.of_int x * xStep
    let y' =  Float.of_int y * yStep
    list.Add(f x' y')
    list.Add(f (x' + xStep) y')
    list.Add(f (x' + xStep) (y' + yStep))
    list.Add(f (x' + xStep) (y' + yStep))
    list.Add(f x' (y' + yStep))
    list.Add(f x' y')

// create all items in a plane
let create_plane_points f xRes yRes = 
    let xStep = 1.0 / Float.of_int xRes 
    let yStep = 1.0 / Float.of_int yRes 
    createPlaneItemList (createSquare f xStep yStep) xRes yRes

// create the 3D positions for a plane, i.e. the thing that says where
// the plane will be in 3D space
let createPlanePositions xRes yRes = 
    let list = create_plane_points point3D xRes yRes 
    new Point3DCollection(list)

// create the texture mappings for a plane, i.e. the thing that 
// maps the 2D image to the 3D plane
let createPlaneTextures xRes yRes = 
    let list = create_plane_points point xRes yRes 
    new PointCollection(list)

// create indices list fora ll our triangles	
let createIndicesPlane width height = 
    let list = new System.Collections.Generic.List<int>() 
    for index = 0 to width * height * 6 do
        list.Add(index)
    new Int32Collection(list)

// center the plane in the field of view
let mapPositionsCenter (positions : Point3DCollection) = 
    let newPositions =
        positions 
        |> Seq.map  
            (fun position -> 
                new Point3D((position.X - 0.5 ) * -1.0 , (position.Y - 0.5 ) * -1.0, position.Z))
    new Point3DCollection(newPositions)

// create a plane and add it to the given mesh
let addPlaneToMesh (mesh : MeshGeometry3D) xRes yRes = 
    mesh.Positions <- mapPositionsCenter (createPlanePositions xRes yRes)
    mesh.TextureCoordinates <- createPlaneTextures xRes yRes
    mesh.TriangleIndices <- createIndicesPlane xRes yRes

// create our window
let window = createWindow "Window2.xaml"

let mesh =
    // grab the 3D view port
    let viewport = window.FindName("ViewPort") :?> Viewport3D
    // find all the meshes and get the first one
    let meshes = findMeshes viewport
    let mesh = Seq.hd meshes
    // add plane to the mesh
    addPlaneToMesh mesh 20 20
    mesh

let mutable f = (fun t x y -> 0.0) 

let changePositions () =
    let dispatcherTimer = new DispatcherTimer()
    dispatcherTimer.Tick.Add 
        (fun e -> 
            let t = (float_of_int DateTime.Now.Millisecond) / 2000.0
            let new_positions =
                mesh.Positions 
                |> Seq.map  
                    (fun position -> 
                        let z = f t position.X position.Y
                        new Point3D(position.X, position.Y, z))
            mesh.Positions <- new Point3DCollection(new_positions))
    dispatcherTimer.Interval <- new TimeSpan(0,0,0,0,100)
    dispatcherTimer.Start()

// show the window, set it the top, and activate the function that will
// set it moving
window.Show()
window.Topmost <- true
changePositions ()

// ------------------------------------------------------------------------------

// define funtions for changing the plance

// map the plane's z coordiante using a cos wave of the y coordiante
let cosY _ _ y = 
    Math.Cos(y * Math.PI)

// map the plane's z coordiante using a cos wave of the x and y coordiantes
let cosXY _ x y = 
    Math.Cos(x * Math.PI) * Math.Cos(y * Math.PI)

let movingCosXY (t : float) x y = 
    (Math.Cos((x + t) * Math.PI) / 2.0) * (Math.Cos((y - t) * Math.PI) / 2.0)

// map the plane's z coordiante using a cos wave of the x and y coordiantes, that 
// has been scaled to give a rippled effect
let waves t x y = 
    (Math.Cos(x * Math.PI * 4.0) / 3.0) * (Math.Cos(y * Math.PI * 2.0) / 3.0)

// a wave effect that moves over time
let movingWaves (t : float) x y = 
    (Math.Cos((x + t) * Math.PI * 4.0) / 3.0) * (Math.Cos(y * Math.PI * 2.0) / 3.0) 

// map the plane's Z coordiante randomly
let rand = new Random()

// a function that will give a shattered effect
let shatter _ _ _ = 
    rand.NextDouble() / 10.0

// a function that will give a static random plane
let mutable positions = new Dictionary<float * float, float>()
let random _ x y = 
    let z = ref 0.0
    let res =
        positions.Keys
        |> Seq.tryfind (fun (xdic, ydic) -> xdic = x && ydic = y)
    match res with
    | Some z -> positions.Item(z)
    | None ->
        let z = (rand.NextDouble() / 10.0)
        positions.Add((x,y),z)
        z

// --------------------------------------------------------------------------------

// apply functions to plane

f <- cosY
f <- cosXY
f <- movingCosXY
f <- waves
f <- movingWaves
f <- shatter
f <- random
