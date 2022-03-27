<%@ Page Title="" Language="C#" MasterPageFile="~/BackAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="PaymentDetail.aspx.cs" Inherits="EBookStore.BackAdmin.PaymentDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1> 打 * 號的項目為必填(或必選) </h1>
    <table border="1" cellspacing="0">
        <tr>
            <th> 支付代碼 </th>
            <td>
                <asp:Literal ID="ltlPaymentID" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th> 支付方式 * </th>
            <td>
                <asp:Literal ID="ltlPaymentName" runat="server"></asp:Literal><br />
                <asp:TextBox ID="txtPaymentName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th> 創建日期 </th>
            <td>
                <asp:Literal ID="ltlPaymentDate" runat="server"></asp:Literal><br />
            </td>
        </tr>
    </table>
    <br />

    <asp:Button ID="btnCreatePayment" runat="server" Text="新增" OnClick="btnCreatePayment_Click" />
    <asp:Button ID="btnUpdatePayment" runat="server" Text="更新" OnClick="btnUpdatePayment_Click" />
</asp:Content>
