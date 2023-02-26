<%@ Page Title="Especialidades" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Especialidades.aspx.cs" Inherits="UI.Web.Especialidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <asp:Panel ID="gridPanel" runat="server">
        <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="false"
            SelectedRowStyle-BackColor ="Black"
            SelectedRowStyle-ForeColor ="White"
            DataKeyNames="ID" OnSelectedIndexChanged="gridView_SelectedIndexChanged" CssClass="tablaABM">
            <Columns>
                <asp:BoundField HeaderText="Descripcion" DataField ="Descripcion" HeaderStyle-CssClass="tablaColumna" />
                <asp:CommandField SelectText ="Seleccionar" ShowSelectButton="true" HeaderStyle-CssClass="tablaColumna" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="formPanel" Visible="false" runat="server">
        <asp:Label ID="descripcionLabel" runat="server" Text="Descripcion:"></asp:Label>
        <asp:TextBox ID="descripcionTextBox" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="descripcionRequerida" runat="server" ErrorMessage="La descripcion no puede estar vacía"
         ControlToValidate ="descripcionTextBox" ForeColor="Red">*</asp:RequiredFieldValidator>
        <br />
    </asp:Panel>
    <asp:Panel ID="gridActionsPanel" runat="server">
    <asp:LinkButton ID="editarLinkButton" runat ="server" OnClick="editarLinkButton_Click">Editar</asp:LinkButton>
    <asp:LinkButton ID="eliminarLinkButton" runat ="server" OnClick="eliminarLinkButton_Click">Eliminar</asp:LinkButton>
    <asp:LinkButton ID="nuevoLinkButton" runat ="server" OnClick="nuevoLinkButton_Click">Nuevo</asp:LinkButton>
    </asp:Panel>
    <asp:Panel ID="formActionsPanel" runat="server">
    <asp:LinkButton ID="aceptarLinkButton" runat ="server" OnClick="aceptarLinkButton_Click">Aceptar</asp:LinkButton>
    <asp:LinkButton ID="cancelarLinkButton" runat ="server" OnClick="cancelarLinkButton_Click" CausesValidation="False">Cancelar</asp:LinkButton>
    </asp:Panel>
</asp:Content>
