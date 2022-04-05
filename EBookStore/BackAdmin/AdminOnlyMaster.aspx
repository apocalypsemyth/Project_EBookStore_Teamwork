<%@ Page Title="" Language="C#" MasterPageFile="~/BackAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="AdminOnlyMaster.aspx.cs" Inherits="Project.BackAdmin.AdminOnlyMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     目前登入者：<asp:Literal ID="ltlAccount" runat="server" Text="user"></asp:Literal>
         <br />
    <br />
 



    <asp:Button ID="btnCreate" runat="server" Text="新增" OnClick="btnCreate_Click" />
    <asp:Button ID="btnDelete" runat="server" Text="刪除" OnClick="btnDelete_Click" />
         <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />
    <asp:Button ID="BtnLogout" runat="server" Text="登出" OnClick="BtnLogout_Click1" />
         <br />

    <asp:TextBox runat="server" ID="txtKeyword" placeholder="請輸入搜尋文字"></asp:TextBox>
    <asp:Button runat="server" ID="btnSearch" Text="搜尋" OnClick="btnSearch_Click" />
    <br />

    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField>
                 <ItemTemplate>
                     <asp:CheckBox runat="server" ID="ckbDel" />
                     <asp:HiddenField runat="server" ID="hfID" Value='<%# Eval("UserID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="UserID" HeaderText="代碼" />
            <asp:BoundField DataField="Account" HeaderText="帳號" />
             <asp:BoundField DataField="Phone" HeaderText="電話" />
              <asp:BoundField DataField="Email" HeaderText="Email" />
            <%--<asp:BoundField DataField="CreateDate" HeaderText="建立日期" />--%>
            <asp:TemplateField HeaderText="管理">
                <ItemTemplate>
                    <a href="EditWithMain.aspx?UserID=<%# Eval("UserID") %>">編輯</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:PlaceHolder runat="server" ID="plcEmpty" Visible="false">
        <p>未有資料</p>
    </asp:PlaceHolder>
</asp:Content>
