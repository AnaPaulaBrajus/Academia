<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OlvidarContraseña.aspx.cs" Inherits="UI.Web.OlvidarContraseña" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Recuperar Contraseña</title>
    <link href="Styles/LoginStylesheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form2" runat="server" class="formulario">
        <h1>Recuperar mi contraseña</h1>

        <div class="contenedor">
            <div class="input-contenedor">
                <asp:Label ID="lblUsuario" runat="server" Text="Usuario"></asp:Label>
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="texto"></asp:TextBox>
                <asp:RequiredFieldValidator ID="usuarioRequerido" runat="server" ErrorMessage="El usuario no puede estar vacío" ControlToValidate="txtUsuario" ForeColor="Red">*</asp:RequiredFieldValidator>
            </div>
            <div class="input-contenedor">
                <asp:Label ID="lblClaveAnterior" runat="server" Text="Clave Anterior"></asp:Label>
                <asp:TextBox ID="txtClaveAnterior" runat="server" TextMode="Password" style="height: 22px" CssClass="texto"></asp:TextBox>
                <asp:RequiredFieldValidator ID="claveAnteriorRequerida" runat="server" ErrorMessage="La clave anterior no puede estar vacía" ControlToValidate="txtClaveAnterior" ForeColor="Red">*</asp:RequiredFieldValidator>
            </div>
            <div class="input-contenedor">
                <asp:Label ID="lblClaveNueva" runat="server" Text="Clave Nueva"></asp:Label>
                <asp:TextBox ID="txtNuevaContraseña" runat="server" TextMode="Password" CssClass="texto"></asp:TextBox>
                <asp:RequiredFieldValidator ID="nuevaContraRequerida" runat="server" ErrorMessage="La nueva contraseña no puede estar vacía" ControlToValidate="txtNuevaContraseña" ForeColor="Red">*</asp:RequiredFieldValidator>
            </div>
            <div>
                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" CssClass="boton" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" CausesValidation="False" CssClass="boton" />
            </div>
            <div>
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ForeColor="Red" />
            </div>
        </div>
    </form>
</body>
</html>

