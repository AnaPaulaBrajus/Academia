<%@ Page Title="Cursos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cursos.aspx.cs" Inherits="UI.Web.Cursos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <asp:Panel ID="gridPanel" runat="server">
        <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="false"
            SelectedRowStyle-BackColor="Black"
            SelectedRowStyle-ForeColor="White"
            DataKeyNames="ID" OnSelectedIndexChanged="gridView_SelectedIndexChanged" CssClass="tablaABM">
            <Columns>
                <asp:BoundField HeaderText="Materia" DataField="IdMateria" HeaderStyle-CssClass="tablaColumna" />
                <asp:BoundField HeaderText="Comision" DataField="IdComision" HeaderStyle-CssClass="tablaColumna" />
                <asp:BoundField HeaderText="Año" DataField="AnioCalendario" HeaderStyle-CssClass="tablaColumna" />
                <asp:BoundField HeaderText="Cupo" DataField="Cupo" HeaderStyle-CssClass="tablaColumna" />
                <asp:CommandField SelectText="Seleccionar" ShowSelectButton="true" HeaderStyle-CssClass="tablaColumna" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="formPanel" Visible="false" runat="server">
        <asp:Label ID="lblMateria" runat="server" Text="Materia"></asp:Label>
        <asp:DropDownList ID="ddlMateria" runat="server"></asp:DropDownList>
        <br />
        <asp:Label ID="lblComision" runat="server" Text="Comision"></asp:Label>
        <asp:DropDownList ID="ddlComision" runat="server"></asp:DropDownList>
        <br />
        <asp:Label ID="lblAnio" runat="server" Text="Año"></asp:Label>
        <asp:TextBox ID="txtAnio" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="anioRequerido" runat="server" ErrorMessage="El año no puede ser vacío" ControlToValidate ="txtAnio" ForeColor="Red">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="lblCupo" runat="server" Text="Cupo"></asp:Label>
        <asp:TextBox ID="txtCupo" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="cupoRequerido" runat="server" ErrorMessage="El cupo no puede ser vacío" ControlToValidate ="txtCupo" ForeColor="Red">*</asp:RequiredFieldValidator>
    </asp:Panel>
    <asp:Panel ID="gridActionsPanel" runat="server">
        <asp:LinkButton ID="editarLinkButton" runat="server" OnClick="editarLinkButton_Click">Editar</asp:LinkButton>
        <asp:LinkButton ID="eliminarLinkButton" runat="server" OnClick="eliminarLinkButton_Click">Eliminar</asp:LinkButton>
        <asp:LinkButton ID="nuevoLinkButton" runat="server" OnClick="nuevoLinkButton_Click">Nuevo</asp:LinkButton>
    </asp:Panel>
    <asp:Panel ID="formActionsPanel" runat="server">
    <asp:LinkButton ID="aceptarLinkButton" runat="server" OnClick="aceptarLinkButton_Click">Aceptar</asp:LinkButton>
    <asp:LinkButton ID="cancelarLinkButton" runat="server" OnClick="cancelarLinkButton_Click" CausesValidation="False">Cancelar</asp:LinkButton>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
    </asp:Panel>
</asp:Content>
