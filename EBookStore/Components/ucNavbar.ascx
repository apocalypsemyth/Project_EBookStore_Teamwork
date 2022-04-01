<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucNavbar.ascx.cs" Inherits="EBookStore.Components.ucNavbar" %>

<nav class="navbar navbar-expand-md navbar-dark bg-primary fixed-top px-3 px-md-0">
    <div class="container-md">
        <h1 class="col-0 col-md-3 navbar__logo-site-name">
            <a class="fw-bold navbar__logo-site-name-link" href="BookList.aspx">
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

        <div class="collapse navbar-collapse col-0 offset-md-1 col-md-8" id="divNavbarCollapseContainer">
            <div class="d-flex w-50 me-2">
                <asp:TextBox ID="txtSearch" CssClass="form-control me-2" placeholder="搜尋" aria-label="搜尋" runat="server" />
                <asp:Button ID="btnSearch" CssClass="btn btn-outline-light fs-5" runat="server" Text="搜尋" OnClick="btnSearch_Click" />
            </div>

            <ul id="ulNavbarList" class="navbar-nav me-auto mb-2 mb-lg-0">
                <li class="nav-item">
                    <a class="nav-link active fs-5" aria-current="page" href="BookList.aspx">首頁</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link fs-5" href="#">關於敝社</a>
                </li>
                <li class="nav-item">
                    <a id="aLinkMyBookList" class="nav-link fs-5" href="#" runat="server">我的書庫</a>
                </li>
                <li class="nav-item">
                    <asp:Button ID="btn_Login" runat="server" Text="登入" OnClick="btn_Login_Click" CssClass="btn btn-success position-relative fs-5" />
                </li>
                <li class="nav-item">
                    <asp:Button ID="btn_Logout" runat="server" Text="登出" OnClick="btn_Logout_Click" CssClass="btn btn-danger position-relative fs-5" />
                </li>
                <li class="nav-item">
                    <a id="aLinkShoppingCart" class="btn btn-primary position-relative fs-5" href="#" runat="server">
                        購物車
                    </a>
                </li>
            </ul>
        </div>
    </div>
</nav>
