<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucNavbar.ascx.cs" Inherits="EBookStore.Components.ucNavbar" %>

<nav class="navbar navbar-expand-lg navbar-light bg-light fixed-top">
    <div class="container-fluid">
        <h1 class="navbar__logo-site-name">
            <a class="navbar__logo-site-name-link" href="BookList.aspx">
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

        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#divNavbarCollapseContainer" aria-controls="divNavbarCollapseContainer" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>

        <div class="collapse navbar-collapse" id="divNavbarCollapseContainer">
            <div class="d-flex" style="width: 50vw;">
                <asp:TextBox ID="txtSearch" CssClass="form-control me-2" placeholder="搜尋" aria-label="搜尋" runat="server" />
                <asp:Button ID="btnSearch" CssClass="btn btn-outline-success" runat="server" Text="搜尋" OnClick="btnSearch_Click" />
            </div>

            <ul id="ulNavbarList" class="navbar-nav me-auto mb-2 mb-lg-0">
                <li class="nav-item">
                    <a class="nav-link active" aria-current="page" href="BookList.aspx">首頁</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="#">關於敝社</a>
                </li>
                <li class="nav-item">
                    <a id="aLinkMyBookList" class="nav-link" href="#" runat="server">我的書庫</a>
                </li>
                <li class="nav-item">
                    <asp:Button ID="btn_Login" runat="server" Text="登入" OnClick="btn_Login_Click" class="btn btn-success position-relative" />
                </li>
                <li class="nav-item">
                    <asp:Button ID="btn_Logout" runat="server" Text="登出" OnClick="btn_Logout_Click" class="btn btn-danger position-relative" />
                </li>
                <li class="nav-item">
                    <button id="btnShoppingCart" type="button" class="btn btn-primary position-relative">
                        購物車
                    </button>
                </li>
            </ul>
        </div>
    </div>
</nav>
