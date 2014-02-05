<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GuessSecretNumber.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Guess Secret Number </title>
    <link href="~/Content/style.css" rel="stylesheet" />
</head>
<body>
    <div id="container">
        <header><h1>Gissa det hemliga talet</h1></header>
        <hr />
        <form id="form1" runat="server">
            <%-- VALIDATION SUMMARY --%>
            <asp:ValidationSummary ID="ValidationSummary" runat="server" HeaderText="Fel inträffade! Åtgärda felen och försök igen." DisplayMode="BulletList" />

            <%-- CONTROL FOR GUESS INPUT --%>
            <div>
                <p>Ange ett tal mellan 1 och 100:</p>
                <asp:TextBox ID="SecretNumberGuess" runat="server"></asp:TextBox>

                <%-- Validation --%>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorGuess" runat="server" ErrorMessage="Ett tal måste anges" Text="*" Display="Dynamic" ControlToValidate="SecretNumberGuess"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidatorGuess" runat="server" ErrorMessage="Fyll i ett heltal" Text="*" ControlToValidate="SecretNumberGuess" Display="Dynamic" Type="Integer" Operator="DataTypeCheck"></asp:CompareValidator>
                <asp:RangeValidator ID="RangeValidatorGuess" runat="server" ErrorMessage="Din gissning måste ligga mellan 1 och 100." Text="*" Display="None" MinimumValue="1" MaximumValue="100" Type="Integer" ControlToValidate="SecretNumberGuess"></asp:RangeValidator>

                <%-- POST BUTTON --%>
                <asp:Button ID="GuessButton" runat="server" Text="Skicka gissning" OnClick="GuessButton_Click" Visible="true" />
                <asp:Button ID="NewGameButton" runat="server" Text="Starta nytt spel" CausesValidation="false" Visible="false" />
            </div>
            <div>
                <p><asp:Literal ID="Guesses" runat="server"></asp:Literal></p>
                <p><asp:Literal ID="GuessResponse" runat="server"></asp:Literal></p>
            </div>
        </form>
        <div class="clear"></div>
    </div>
</body>
</html>
