<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="webReportePlanes.aspx.cs" Inherits="UI.Web.webReportePlanes" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226" Width="100%">
                <localreport reportpath="InformePlanes.rdlc">
                    <datasources>
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSetPlanes" />
                    </datasources>
                </localreport>
            </rsweb:ReportViewer>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="DataSetPlanesTableAdapters.planesTableAdapter" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" UpdateMethod="Update">
                <DeleteParameters>
                    <asp:Parameter Name="Original_id_plan" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="desc_plan" Type="String" />
                    <asp:Parameter Name="id_especialidad" Type="Int32" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="desc_plan" Type="String" />
                    <asp:Parameter Name="id_especialidad" Type="Int32" />
                    <asp:Parameter Name="Original_id_plan" Type="Int32" />
                </UpdateParameters>
            </asp:ObjectDataSource>
        </div>
    </form>
</body>
</html>