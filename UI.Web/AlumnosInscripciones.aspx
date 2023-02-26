<%@ Page Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="AlumnosInscripciones.aspx.cs" Inherits="UI.Web.AlumnosInscripciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
       <asp:Panel ID="gridPanel" runat="server">
        <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="false"
            SelectedRowStyle-BackColor="Black"
            SelectedRowStyle-ForeColor="White"
            DataKeyNames="ID" OnSelectedIndexChanged="gridView_SelectedIndexChanged" CssClass="tablaABM">
            <Columns>
                <asp:BoundField HeaderText="Id Inscripcion" DataField="ID" HeaderStyle-CssClass="tablaColumna" />
                <asp:BoundField HeaderText="Id Alumno" DataField="IdAlumno" HeaderStyle-CssClass="tablaColumna" />
                <asp:BoundField HeaderText="Id Curso" DataField="IdCurso" HeaderStyle-CssClass="tablaColumna" />
                <asp:BoundField HeaderText="Condicion" DataField="Condicion" HeaderStyle-CssClass="tablaColumna" />
                <asp:BoundField HeaderText="Nota" DataField="Nota" HeaderStyle-CssClass="tablaColumna" />
                <asp:CommandField SelectText="Seleccionar" ShowSelectButton="true" HeaderStyle-CssClass="tablaColumna" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
       <asp:Panel ID="formPanel" Visible="false" runat="server">
        <asp:Label ID="lblCurso" runat="server" Text="Curso"></asp:Label>
        <asp:DropDownList ID="ddlCurso" runat="server" OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
           &nbsp;&nbsp;&nbsp;
           <asp:Label ID="lblMateria" runat="server" Text="Materia"></asp:Label>
           &nbsp;&nbsp;&nbsp;
           <asp:Label ID="lblComision" runat="server" Text="Comision"></asp:Label>
           &nbsp;&nbsp;&nbsp;
           <asp:Label ID="lblAnio" runat="server" Text="Año"></asp:Label>
           &nbsp;&nbsp;&nbsp;
           <asp:Label ID="lblCupo" runat="server" Text="Cupo"></asp:Label>
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