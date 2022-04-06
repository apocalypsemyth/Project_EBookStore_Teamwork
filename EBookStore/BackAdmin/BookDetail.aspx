<%@ Page Title="" Language="C#" MasterPageFile="~/BackAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="BookDetail.aspx.cs" Inherits="EBookStore.BackAdmin.BookDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <h1> 打 * 號的項目為必填(或必選) </h1>
    <table border="1" cellspacing="0">        
        <asp:Literal ID="ltlUserID" runat="server"></asp:Literal>
        <asp:Literal ID="ltlBookID" runat="server"></asp:Literal>
        <tr>
            <th> 分類 * </th>
            <td>
                <asp:Literal ID="ltlCategory" runat="server"></asp:Literal><br />
                <asp:TextBox ID="txtCategory" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th> 作者 * </th>
            <td>
                <asp:Literal ID="ltlAuthor" runat="server"></asp:Literal><br />
                <asp:TextBox ID="txtAuthor" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th> 書名 * </th>
            <td>
                <asp:Literal ID="ltlBookName" runat="server"></asp:Literal><br />
                <asp:TextBox ID="txtBookName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th> 內容簡介 * </th>
            <td>
                <asp:Literal ID="ltlDescription" runat="server"></asp:Literal><br />
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="200px" Height="150px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th> 封面圖 * </th>
            <td>
                <asp:PlaceHolder ID="plcCreateImg" runat="server">
                    <asp:FileUpload ID="fuImage" runat="server" /><br />                    
                </asp:PlaceHolder>

                <asp:Repeater ID="rptImage" runat="server">
                    <ItemTemplate>
                            <asp:PlaceHolder runat="server" Visible='<%# !string.IsNullOrWhiteSpace(Eval("Image") as string) %>'>
                                <asp:Image ID="ImageFromDB" runat="server" ImageUrl='<%# Eval("Image") %>' Height="180px"/> 
                            </asp:PlaceHolder>
                    </ItemTemplate>
                </asp:Repeater><br />                                           
            </td>
        </tr>
        <tr>
            <th> 書籍檔案 * </th>
            <td>
                <asp:PlaceHolder ID="plcCreateBookContent" runat="server">
                    <asp:FileUpload ID="fuBookContent" runat="server" /><br />                    
                </asp:PlaceHolder>                                           
            </td>
        </tr>
        <tr>
            <th> 價格 * </th>
            <td>
                <asp:Literal ID="ltlPrice" runat="server"></asp:Literal><br />
                <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>元
            </td>
        </tr>        
    </table>

    <asp:Literal ID="ltlErrorMsg" runat="server"></asp:Literal><br />

    <asp:Button ID="btnSave" runat="server" Text="儲存" OnClick="btnSave_Click" />
    <asp:Button ID="btnUpdate" runat="server" Text="更新" OnClick="btnUpdate_Click"/>
    <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />

</asp:Content>