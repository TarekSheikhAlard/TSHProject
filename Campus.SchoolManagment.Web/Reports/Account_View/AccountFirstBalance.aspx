﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AccountFirstBalance.aspx.cs" Inherits="Campus.SchoolManagment.Web.Reports.Account_View.AccountFirstBalance" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src='<%=ResolveUrl("~/Reports/crystalreportviewers13/js/crviewer/crv.js")%>' type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" OnLoad="CrystalReportViewer1_Load" ToolPanelView="None" />
</asp:Content>