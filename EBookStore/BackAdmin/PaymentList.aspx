<%@ Page Title="" Language="C#" MasterPageFile="~/BackAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="PaymentList.aspx.cs" Inherits="EBookStore.BackAdmin.PaymentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="btnCreatePayment" runat="server" Text="新增" OnClick="btnCreatePayment_Click" />
    <asp:Button ID="btnDeletePayment" runat="server" Text="刪除" OnClick="btnDeletePayment_Click" />
    <br />

    <asp:GridView ID="gvPaymentList" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HiddenField runat="server" ID="hfID" Value='<%# Eval("PaymentID") %>' />
                    <asp:CheckBox runat="server" ID="ckbDel" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="PaymentID" HeaderText="支付代碼" />
            <asp:BoundField DataField="PaymentName" HeaderText="支付方式" />
            <asp:BoundField DataField="PaymentDate" HeaderText="創建日期" DataFormatString="{0:D}" />

            <asp:TemplateField HeaderText="管理">
                <ItemTemplate>
                    <a href="PaymentDetail.aspx?ID=<%# Eval("PaymentID") %>">編輯</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:PlaceHolder runat="server" ID="plcEmpty">
        <p>尚未有資料</p>
    </asp:PlaceHolder>
</asp:Content>
