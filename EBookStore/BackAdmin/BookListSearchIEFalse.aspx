<%@ Page Title="" Language="C#" MasterPageFile="~/BackAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="BookListSearchIEFalse.aspx.cs" Inherits="EBookStore.BackAdmin.BookListSearchIEFalse" %>

<%@ Register Src="~/ShareControls/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


                
    <asp:Button ID="btnCreate" runat="server" Text="新增" OnClick="btnCreate_Click" />
    <asp:Button ID="btnDelete" runat="server" Text="下架" OnClick="btnDelete_Click" /><br />

    <asp:TextBox ID="txtKeyword" runat="server" placeholder="請輸入搜尋書名"></asp:TextBox>
    <asp:Button ID="btnSearch" runat="server" Text="搜尋" OnClick="btnSearch_Click" />
    <br />

    <asp:Literal ID="ltlDisplay" runat="server">顯示: </asp:Literal>
    <asp:Button ID="btnAll" runat="server" Text="全部商品" OnClick="btnAll_Click" />
<%--    <asp:Button ID="btnIsEnableTrue" runat="server" Text="已上架商品" OnClick="btnIsEnableTrue_Click" />
    <asp:Button ID="btnIsEnableFalse" runat="server" Text="已下架商品" OnClick="btnIsEnableFalse_Click" />--%>
    <asp:Button ID="btnSearchIETrue" runat="server" Text="已上架商品" OnClick="btnSearchIETrue_Click" />
    <asp:Button ID="btnSearchIEFalse" runat="server" Text="已下架商品" OnClick="btnSearchIEFalse_Click" />

    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="ckbDel" runat="server" />
                    <asp:HiddenField ID="hfID" runat="server" Value='<%# Eval("BookID") %>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="BookID" HeaderText="書籍代碼" />
            <asp:BoundField DataField="BookName" HeaderText="書名" />
            <asp:BoundField DataField="AuthorName" HeaderText="作者" />

            <asp:TemplateField HeaderText="封面圖">
                <ItemTemplate>
                    <%--圖片的資料繫結使用 ImageUrl='<%# Eval("Image") %>' 記得資料庫的 圖片路徑 改為 相對路徑--%>
                    <asp:Image ID="ImageFromDB" runat="server" ImageUrl='<%# Eval("Image") %>' Width="250px" Height="180px" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="Price" HeaderText="價格(元)" DataFormatString="{0:C}" />
            <asp:BoundField DataField="IsEnable" HeaderText="商品上架" />
            <asp:BoundField DataField="Date" HeaderText="上架日期" DataFormatString="{0:D}" />
            <asp:BoundField DataField="EndDate" HeaderText="下架日期" DataFormatString="{0:D}" />

            <asp:TemplateField HeaderText="管理">
                <ItemTemplate>
                    <%--  <%# Eval("BookID") %>: 資料繫結運算式  --%>
                    <a href="BookDetail.aspx?ID=<%# Eval("BookID") %>">編輯</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:PlaceHolder runat="server" ID="plcEmpty" Visible="false">
        <p>尚未有資料 </p>

    </asp:PlaceHolder>
    <uc1:ucPager runat="server" ID="ucPager" PageSize="10" />
</asp:Content>
