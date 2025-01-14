<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AccountNotespayable.aspx.cs" Inherits="Campus.SchoolManagment.Web.Reports.Account_View.AccountNotespayable" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src='<%=ResolveUrl("~/Reports/crystalreportviewers13/js/crviewer/crv.js")%>' type="text/javascript"></script>
    <link href="../../Content/Css/bootstrap.min.css" rel="stylesheet" />
    <style>

        .alert-danger {
        
        width:70%; margin-top:20px; background-color:#fff;
        }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="alert alert-danger"  >
     <asp:TextBox ID="txtfrom" runat="server" Width="158px"  ></asp:TextBox>
    <asp:TextBox ID="txtto" runat="server" style="margin-left: 32px"></asp:TextBox>
 
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="بحث" style="z-index: 1;      width: 186px;    height: 29px;" CssClass="btn btn-primary" />
     <br />
     <br />

    </div>
     <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" OnLoad="CrystalReportViewer1_Load" ToolPanelView="None" />
     <br />
    <br />
    </asp:Content>
