<%@ Page Inherits="Strangelights.HttpHandlers.HelloUser" %>
<html>
    <head>
        <title>F# - Hello User</title>
    </head>
    <body>
        <p>Hello User</p>
        <form id="theForm" runat="server">
        <asp:Label
            ID="OutputControl"
            Text="Enter you're name ..."
            runat="server" />
        <br />
        <asp:TextBox
            ID="InputControl"
            runat="server" />
        <br />
        <asp:LinkButton
            ID="SayHelloButton"
            Text="Say Hello ..."
            runat="server"
            OnClick="SayHelloButton_Click" />
        </form>
    </body>
</html>
