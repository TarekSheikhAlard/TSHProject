<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/SiteMaster.Master" AutoEventWireup="true" CodeBehind="BusStudentOccupation.aspx.cs" Inherits="Campus.SchoolManagment.Web.Reports.Account_View.BusStudentOccupation" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

        <script src='<%=ResolveUrl("~/Reports/crystalreportviewers13/js/crviewer/crv.js")%>' type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <asp:TextBox ID="TextBox1" runat="server" Width="227px"></asp:TextBox>
        <br />
        <br />
        <br />
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" OnLoad="CrystalReportViewer1_Load" ToolPanelView="None" />
        <br />
        <br />

        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="بحث" Height="25px" style="z-index: 1; left: 525px; top: 151px; position: absolute; width: 216px; margin-left: 2px" />

</asp:Content>
