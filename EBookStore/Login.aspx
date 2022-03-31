<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EBookStore.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Css/Global.css" />
    <link rel="stylesheet" href="Css/Components/Navbar.css" />
    <link rel="stylesheet" href="Css/Components/Footer.css" />
    <link rel="stylesheet" href="Content/bootstrap.min.css" />
    <style>
        body {
             background:url("https://images.pexels.com/photos/159866/books-book-pages-read-literature-159866.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1") no-repeat;
            background-size:cover;;
        }

        table {
            margin-left: auto;
            margin-right: auto;
            margin-top: auto;
            margin-bottom: auto;
            vertical-align: middle;
        }

        div {
            color: black;
            font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif
        }

        p {
        }
        /*border-color {
            white
        } */
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table align="center" border="1" bordercolor="white">
            <tr>
                <td>
                    <asp:PlaceHolder ID="plcLogin" runat="server">
                        <div>
                            <p>Account:</p>
                            <asp:TextBox ID="txtAccount" runat="server"></asp:TextBox>
                            <p>Password:</p>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:Button ID="btnLogin" runat="server" Text="登入" OnClick="btnLogin_Click" />
                            <asp:Button ID="btnRegister" runat="server" Text="註冊" OnClick="btnRegister_Click" />
                            <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
                        </div>
                    </asp:PlaceHolder>
                </td>
            </tr>
        </table>
        <table border="0" align="center">
            <tr>
                <td>
                    <asp:PlaceHolder ID="plcUserInfo" runat="server">
                        <asp:Literal ID="ltlAccount" runat="server"></asp:Literal><br />
                        <p>請前往<a href="/BackAdmin/AdminOnlyMaster.aspx">後台</a></p>
                    </asp:PlaceHolder>
                </td>
            </tr>
        </table>
    </form>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/jquery.min.js"></script>
</body>
</html>
