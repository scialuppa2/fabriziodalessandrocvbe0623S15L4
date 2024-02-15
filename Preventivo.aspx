<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Preventivo.aspx.cs" Inherits="Concessionaria.Preventivo" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Concessionaria</title>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <h1>Concessionaria Auto</h1>

            <!-- Selezione dell'auto -->
            <label for="ddlAuto">Seleziona un'auto:</label>
            <asp:DropDownList ID="ddlAuto" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAuto_SelectedIndexChanged">
                <asp:ListItem Text="-- Seleziona --" Value=""></asp:ListItem>
            </asp:DropDownList>
            <br />

            <!-- Visualizzazione dell'immagine dell'auto -->
            <asp:Image ID="imgAuto" Width="400px" runat="server" />



            <!-- Visualizzazione del prezzo base -->
            <asp:Label ID="lblPrezzoBase" runat="server"></asp:Label>

            <!-- Selezione degli optional -->
            <h2>Optional</h2>
            <asp:CheckBoxList ID="CheckBoxList1" runat="server"></asp:CheckBoxList>


            <!-- Selezione della garanzia -->
            <h2>Garanzia</h2>
            <asp:DropDownList ID="ddlGaranzia" runat="server">
                <asp:ListItem Text="-- Seleziona --" Value=""></asp:ListItem>
                <asp:ListItem Text="1 Anno" Value="1"></asp:ListItem>
                <asp:ListItem Text="2 Anni" Value="2"></asp:ListItem>
                <asp:ListItem Text="3 Anni" Value="3"></asp:ListItem>
                <asp:ListItem Text="4 Anni" Value="4"></asp:ListItem>
                <asp:ListItem Text="5 Anni" Value="5"></asp:ListItem>
            </asp:DropDownList>

            <!-- Pulsante per calcolare il preventivo -->
            <asp:Button ID="btnCalcolaPreventivo" runat="server" Text="Calcola Preventivo" OnClick="btnCalcolaPreventivo_Click" />

            <!-- Visualizzazione del preventivo -->
            <div id="divPreventivo" runat="server" visible="false">
                <h2>Preventivo</h2>
                <!-- Visualizzazione dei dettagli del preventivo -->
                <asp:Label ID="lblDettagliPreventivo" runat="server"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
