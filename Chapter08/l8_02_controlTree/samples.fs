#light
open System.Windows.Forms

// The tree type
type 'a Tree =
| Node of 'a Tree * 'a Tree
| Leaf of 'a

// The definition of the tee
let tree =
    Node(
        Node(
            Leaf "one",
            Node(Leaf "two", Leaf "three")),
        Node(
            Node(Leaf "four", Leaf "five"),
            Leaf "six"))
            
// A function to transform our tree into a tree of controls
let mapTreeToTreeNode t =
    let rec mapTreeToTreeNodeInner t (node : TreeNode) =
        match t with
        | Node (l, r) ->
            let newNode = new TreeNode("Node")
            node.Nodes.Add(newNode) |> ignore
            mapTreeToTreeNodeInner l newNode
            mapTreeToTreeNodeInner r newNode
        | Leaf x ->
            node.Nodes.Add(new TreeNode(any_to_string x)) |> ignore
    let root = new TreeNode("Root")
    mapTreeToTreeNodeInner t root
    root
    
// create the form object
let form =
    let temp = new Form()
    let treeView = new TreeView(Dock = DockStyle.Fill)
    treeView.Nodes.Add(mapTreeToTreeNode tree) |> ignore
    treeView.ExpandAll()
    temp.Controls.Add(treeView)
    temp
    
Application.Run(form)
