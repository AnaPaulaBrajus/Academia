<%@ Page Title="Materias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Materias.aspx.cs" Inherits="UI.Web.Materias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <asp:Panel ID="gridPanel" runat="server">
        <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="false"
            SelectedRowStyle-BackColor ="Black"
                SelectedRowStyle-ForeColor="White"
                DataKeyNames="ID" OnSelectedIndexChanged="gridView_SelectedIndexChanged" CssClass="tablaABM">
                <Columns>
                    <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" HeaderStyle-CssClass="tablaColumna"/>
                    <asp:BoundField HeaderText ="Horas semanales" DataField="HsSemanales" HeaderStyle-CssClass="tablaColumna"/>
                    <asp:BoundField HeaderText="Horas totales" DataField="HsTotales" HeaderStyle-CssClass="tablaColumna"/>
                    <asp:BoundField HeaderText="Id plan" DataField="IdPlan" HeaderStyle-CssClass="tablaColumna"/>
                    <asp:CommandField SelectText="Seleccionar" ShowSelectButton="true" HeaderStyle-CssClass="tablaColumna"/>
                </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="formPanel" Visible="false" runat="server">
        <asp:Label ID="descripcionLabel" runat="server" Text="Descripción:"></asp:Label>
        <asp:TextBox ID="descripcionTextBox" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="descripcionRequerida" runat="server" ErrorMessage="La descripción no puede estar vacia" ControlToValidate="descripcionTextBox" ForeColor="Red">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="hs_semanalesLabel" runat="server" Text="Horas semanales:"></asp:Label>
        <asp:TextBox ID="hs_semanalesTextBox" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="hs_semanalesRequeridas" runat="server" ErrorMessage="Las horas semanales no puede estar vacío" ControlToValidate="hs_semanalesTextBox" ForeColor="Red">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="hs_totalesLabel" runat="server" Text="Horas totales:"></asp:Label>
        <asp:TextBox ID="hs_totalesTextBox" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="hs_totalesRequerida" runat="server" ErrorMessage="Las horas totales no puede estar vacía" ControlToValidate="hs_totalesTextBox" ForeColor="Red">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="IdPlanLabel" runat="server" Text="Plan: "></asp:Label>
        <asp:DropDownList ID="cmbIdPlan" runat="server">
        </asp:DropDownList>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
    </asp:Panel>
    <asp:Panel ID ="gridActionsPanel" runat="server">
        <asp:LinkButton ID="editarLinkButton" runat="server" OnClick="editarLinkButton_Click">Editar</asp:LinkButton>
        <asp:LinkButton ID="eliminarLinkButton" runat="server" OnClick="eliminarLinkButton_Click">Eliminar</asp:LinkButton>
        <asp:LinkButton ID="nuevoLinkButton" runat="server" OnClick="nuevoLinkButton_Click">Nuevo</asp:LinkButton>
    </asp:Panel>
    <asp:Panel ID="formActionsPanel" runat="server">
        <asp:LinkButton ID="aceptarLinkButton" runat="server" OnClick="aceptarLinkButton_Click">Aceptar</asp:LinkButton>
        <asp:LinkButton ID="cancelarLinkButton" runat="server" OnClick="cancelarLinkButton_Click">Cancelar</asp:LinkButton>
    </asp:Panel>
</asp:Content>
