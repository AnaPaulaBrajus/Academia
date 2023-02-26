<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="UI.Web.Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <asp:Panel ID="gridPanel" runat="server">
        <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="false"
            SelectedRowStyle-BackColor="Black"
            SelectedRowStyle-ForeColor="White"
            DataKeyNames="ID" OnSelectedIndexChanged="gridView_SelectedIndexChanged" CssClass="tablaABM">
            <Columns>
                <asp:BoundField HeaderText="Nombre" DataField ="Nombre" HeaderStyle-CssClass="tablaColumna"/>
                <asp:BoundField HeaderText="Apellido" DataField ="Apellido" HeaderStyle-CssClass="tablaColumna"/>
                <asp:BoundField HeaderText="Email" DataField ="Email" HeaderStyle-CssClass="tablaColumna"/>
                <asp:BoundField HeaderText="Usuario" DataField ="NombreUsuario" HeaderStyle-CssClass="tablaColumna"/>
                <asp:BoundField HeaderText="Habilitado" DataField ="Habilitado" HeaderStyle-CssClass="tablaColumna"/>
                <asp:BoundField HeaderText="ID Persona" DataField="IdPersona" HeaderStyle-CssClass="tablaColumna" />
                <asp:CommandField SelectText ="Seleccionar" ShowSelectButton="true" HeaderStyle-CssClass="tablaColumna"/>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="formPanel" Visible="false" runat="server">
        <asp:Label ID="nombreLabel" runat="server" Text="Nombre:"></asp:Label>
        <asp:TextBox ID="nombreTextBox" runat="server" ReadOnly="True"></asp:TextBox>
        <asp:RequiredFieldValidator ID="nombreRequerido" runat="server" ErrorMessage="El nombre no puede estar vacío"
         ControlToValidate ="nombreTextBox" ForeColor="Red">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="apellidoLabel" runat="server" Text="Apellido:"></asp:Label>
        <asp:TextBox ID="apellidoTextBox" runat="server" ReadOnly="True"></asp:TextBox>
        <asp:RequiredFieldValidator ID="apellidoRequerido" runat="server" ErrorMessage="El apellido no puede estar vacío"
         ControlToValidate ="apellidoTextBox" ForeColor="Red">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="emailLabel" runat="server" Text="Email:"></asp:Label>
        <asp:TextBox ID="emailTextBox" runat="server" ReadOnly="True"></asp:TextBox>
         <asp:RequiredFieldValidator ID="emailRequerido" runat="server" ErrorMessage="El email no puede estar vacío"
         ControlToValidate ="emailTextBox" ForeColor="Red">*</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="esMailValido" runat="server" ErrorMessage="El email no es válido"
            ControlToValidate="emailTextBox" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
        <br />
        <asp:Label ID="habilitadoLabel" runat="server" Text="Habilitado:"></asp:Label>
        <asp:CheckBox ID="habilitadoCheckBox" runat="server" />
        <br />
        <asp:Label ID="nombreUsuarioLabel" runat="server" Text="Usuario:"></asp:Label>
        <asp:TextBox ID="nombreUsuarioTextBox" runat="server"></asp:TextBox>
         <asp:RequiredFieldValidator ID="nombreUsuarioRequerido" runat="server" ErrorMessage="El usuario no puede estar vacío"
         ControlToValidate ="nombreUsuarioTextBox" ForeColor="Red">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="claveLabel" runat="server" Text="Clave:"></asp:Label>
        <asp:TextBox ID="claveTextBox" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="claveRequerida" runat="server" ErrorMessage="La clave no puede estar vacía"
         ControlToValidate ="claveTextBox" ForeColor="Red">*</asp:RequiredFieldValidator>
        <asp:CustomValidator ID="ValidarLongitudContraseña" runat="server" ErrorMessage="La contraseña debe tener 8 o más caracteres"
         ControlToValidate="claveTextBox" OnServerValidate="ValidarLongitudContraseña_ServerValidate" ForeColor ="Red">*</asp:CustomValidator>
        <br />
        <asp:Label ID="repetirClaveLabel" runat="server" Text="Repetir Clave:"></asp:Label>
        <asp:TextBox ID="repetirClaveTextBox" TextMode="Password" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="repetirClaveRequerida" runat="server" ErrorMessage="Repetir clave no puede estar vacío"
        ControlToValidate ="repetirClaveTextBox" ForeColor="Red">*</asp:RequiredFieldValidator>
        <asp:CompareValidator ID="compararClaves" runat="server" ErrorMessage="Las claves no coinciden"
         ControlToValidate="claveTextBox" ControlToCompare="repetirClaveTextBox" Type="String" Operator ="Equal" ForeColor="Red">*</asp:CompareValidator>
        <br />
        <asp:Label ID="Label1" runat="server" Text="ID Persona:"></asp:Label>
        <asp:TextBox ID="idPersonaTextBox" runat="server" ReadOnly="True"></asp:TextBox>
        <asp:RequiredFieldValidator ID="idPRequerido" ControlToValidate="idPersonaTextBox" runat="server" ErrorMessage="El Id de la Persona no puede ser vacío" ForeColor="Red">*</asp:RequiredFieldValidator>
        <asp:ValidationSummary ID="ResumenDeErrores" ForeColor="Red" runat="server"/>
        <br />

    </asp:Panel>
    <asp:Panel ID="gridActionsPanel" runat="server">
        <asp:LinkButton ID="editarLinkButton" runat ="server" OnClick="editarLinkButton_Click">Editar</asp:LinkButton>
    </asp:Panel>
        <asp:Panel ID="formActionsPanel" runat="server">
        <asp:LinkButton ID="aceptarLinkButton" runat ="server" OnClick="aceptarLinkButton_Click">Aceptar</asp:LinkButton>
        <asp:LinkButton ID="cancelarLinkButton" runat ="server" OnClick="cancelarLinkButton_Click" CausesValidation="False">Cancelar</asp:LinkButton>
    </asp:Panel>
</asp:Content>
