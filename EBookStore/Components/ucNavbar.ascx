<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucNavbar.ascx.cs" Inherits="EBookStore.Components.ucNavbar" %>

<header class="navbar__container">
    <h1 class="navbar__logo-site-name">
        <a class="navbar__logo-site-name-link" href="index.html">
            <div class="navbar__logo-container">
                <img
                    class="navbar__logo"
                    src="Images/logo.jpg"
                    alt="logo"
                    title="Logo" />
            </div>
            PC Phone
        </a>
    </h1>

    <div class="navbar__seperator">
        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>

        <%--這是利用Jquery請求API至資料庫抓資料部分實作--%>
        <%--<input type="text" id="txtSearch2" name="searchText" />--%>
        <asp:Button ID="btnSearch" runat="server" Text="搜尋" OnClick="btnSearch_Click" />

        <%--這是利用Jquery請求API至資料庫抓資料部分實作--%>
        <%--<button type="submit" id="btnSearch2">搜尋</button>--%>
    </div>

    <nav class="navbar__navlist-container">
        <ul class="navbar__navlist">
            <li class="navbar__navitem"><a href="index.html">首頁</a></li>
            <li class="navbar__navitem">
                <a href="pages/about.html">關於敝社</a>
            </li>
            <li class="navbar__navitem"><a href="pages/goods.html">商品</a></li>
            <li class="navbar__navitem">
                <a href="pages/partners.html">合作夥伴</a>
            </li>
             <asp:Button ID="btn_Login" runat="server" Text="登入" OnClick="btn_Login_Click"  class="btn btn-success position-relative"  />
            <asp:Button ID="btn_Logout" runat="server" Text="登出" OnClick="btn_Logout_Click" class="btn btn-danger position-relative" />
          
            
            <li class="navbar__navitem">
                <button id="btnShoppingCart" type="button" class="btn btn-primary position-relative">
                    購物車
                        <%--<asp:Label ID="lblShoppingCartBadge" CssClass="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger" runat="server">
                        </asp:Label>--%>
                </button>
            </li>
        </ul>
    </nav>
</header>
