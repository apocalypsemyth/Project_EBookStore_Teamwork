<%@ Page Title="" Language="C#" MasterPageFile="~/BackAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="BookDetail.aspx.cs" Inherits="EBookStore.BackAdmin.BookDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1> 打 * 號的項目為必填(或必選) </h1>
    <table border="1" cellspacing="0">
        <%--<tr>
            <th> 書籍代碼 </th>
            <td>
                <asp:Literal ID="ltlBookID" runat="server"></asp:Literal>
            </td>
        </tr>--%>
        <%--<tr>
            <th> 管理員編號 * </th>
            <td>
                <asp:Literal ID="ltlUserID" runat="server"></asp:Literal><br />
                <asp:TextBox ID="txtUserID" runat="server"></asp:TextBox>
            </td>
        </tr>--%>
        <asp:Literal ID="ltlUserID" runat="server"></asp:Literal>
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
                    <%--<asp:Image ID="UploadingImage" runat="server" />--%>
                </asp:PlaceHolder>

                <asp:Repeater ID="rptImage" runat="server">
                    <ItemTemplate>
                            <asp:PlaceHolder runat="server" Visible='<%# !string.IsNullOrWhiteSpace(Eval("Image") as string) %>'>
                                <asp:Image ID="ImageFromDB" runat="server" ImageUrl='<%# Eval("Image") %>' Width="250px" Height="180px"/> 
                            </asp:PlaceHolder>
                    </ItemTemplate>
                </asp:Repeater><br />
                <asp:Button ID="btnImgChange" runat="server" Text="換圖片" OnClick="btnImgChange_Click" />

                <asp:PlaceHolder ID="plcEditImg" runat="server">
                    <asp:FileUpload ID="fuEditImage" runat="server" /><br />
                    <%--<asp:Image ID="UploadingEditImage" runat="server" /><br />--%>
                </asp:PlaceHolder>
                <asp:Button ID="btnEditImgCancel" runat="server" Text="取消" OnClick="btnEditImgCancel_Click" />
            </td>
        </tr>
        <tr>
            <th> 書籍檔案 * </th>
            <td>
                <asp:PlaceHolder ID="plcCreateBookContent" runat="server">
                    <asp:FileUpload ID="fuBookContent" runat="server" /><br />                    
                </asp:PlaceHolder>

                <asp:Repeater ID="rptBookContent" runat="server">
                    <ItemTemplate>
                            <asp:PlaceHolder runat="server" Visible='<%# !string.IsNullOrWhiteSpace(Eval("BookContent") as string) %>'>
                                <asp:Image ID="ImageFromDB" runat="server" ImageUrl='<%# Eval("BookContent") %>' Width="250px" Height="180px"/> 
                            </asp:PlaceHolder>
                    </ItemTemplate>
                </asp:Repeater><br />
                <asp:Button ID="btnBookContentChange" runat="server" Text="更換檔案" OnClick="btnBookContentChange_Click" />

                <asp:PlaceHolder ID="plcEditBookContent" runat="server">
                    <asp:FileUpload ID="fuEditBookContent" runat="server" /><br />                    
                </asp:PlaceHolder>
                <asp:Button ID="btnEditBookContentCancel" runat="server" Text="取消" OnClick="btnEditBookContentCancel_Click" />
            </td>
        </tr>
        <tr>
            <th> 價格 * </th>
            <td>
                <asp:Literal ID="ltlPrice" runat="server"></asp:Literal><br />
                <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>元
            </td>
        </tr>
        <%--<tr>
            <th> 商品上架 * </th>
            <td>
                &nbsp;&nbsp;<asp:Literal ID="ltlIsEnable" runat="server"></asp:Literal><br />
                <asp:RadioButtonList ID="rbtnList" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="True" Value="True"></asp:ListItem>
                    <asp:ListItem Text="False" Value="False"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>--%>
        <%--<tr>
            <th> 上架日期 * </th>
            <td>
                <asp:Literal ID="ltlDate" runat="server"></asp:Literal><br />
                <asp:TextBox ID="txtDate" runat="server" Text="2022/01/22"></asp:TextBox>
            </td>
        </tr>--%>
        <%--<tr>
            <th> 下架日期 * </th>
            <td>
                <asp:Literal ID="ltlEndDate" runat="server"></asp:Literal><br />
                <asp:TextBox ID="txtEndDate" runat="server" Text="2023/01/01"></asp:TextBox>
            </td>
        </tr>--%>
    </table>

    <asp:Literal ID="ltlErrorMsg" runat="server"></asp:Literal><br />

    <asp:Button ID="btnSave" runat="server" Text="儲存" OnClick="btnSave_Click" />
    <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />

</asp:Content>