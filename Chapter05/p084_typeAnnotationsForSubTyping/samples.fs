#light
open System
open System.Windows.Forms

let showFormRevised (form : #Form) =
    form.Show()

// ThreadExceptionDialog is define in the BCL and is
// directly derived type of the Form class
let anotherForm = new ThreadExceptionDialog(new Exception())
showFormRevised anotherForm