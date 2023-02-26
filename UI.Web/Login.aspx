<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="UI.Web.Login" %>
  <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Iniciar Sesion</title>
    <link href="Styles/LoginStylesheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            font-size: 20px;
            padding: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="formulario">
        <h1>Inicio de Sesión</h1>

        <div class="contenedor">
            <div class="input-contenedor">
                <asp:Label ID="lblUsuario" runat="server" Text="Usuario"></asp:Label>
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="texto"></asp:TextBox>
            </div>
            <div class="input-contenedor">
                <asp:Label ID="lblClave" runat="server" Text="Clave"></asp:Label>
                <asp:TextBox ID="txtClave" runat="server" TextMode="Password" CssClass="texto"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" OnClick="btnIngresar_Click" CssClass="boton" />
            </div>
            <div>
                <asp:LinkButton ID="OlvidoClave" runat="server" Text="Olvidé mi Clave" OnClick="lnkRecordarClave_Click" CssClass="link"></asp:LinkButton>
            </div>
        </div>
    </form>
</body>
</html>