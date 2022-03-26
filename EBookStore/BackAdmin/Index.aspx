<%@ Page Title="" Language="C#" MasterPageFile="~/BackAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="EBookStore.BackAdmin.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    目前登入者：<asp:Literal ID="ltlAccount" runat="server"></asp:Literal>
    <br />
    <asp:Button runat="server" ID="btnLogout" Text="登出" OnClick="btnLogout_Click" />
</asp:Content>
