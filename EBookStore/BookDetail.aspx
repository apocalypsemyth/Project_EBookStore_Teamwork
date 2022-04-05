<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BookDetail.aspx.cs" Inherits="EBookStore.BookDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row align-items-center justify-content-center height-preset gy-5 pt-3 pt-md-0">
        <div class="col-md-11">
            <nav style="--bs-breadcrumb-divider: '>';" aria-label="breadcrumb" class="ms-3 ms-md-0">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a class="text-decoration-none" href="BookList.aspx">首頁</a></li>
                    <li class="breadcrumb-item active" aria-current="page">
                        <asp:Literal ID="ltlBreadCrumbBookName" runat="server"></asp:Literal>
                    </li>
                </ol>
            </nav>
        </div>

        <div class="col-md-11">
            <div class="row align-items-center justify-content-center gy-5 gy-md-0 mx-2 mx-md-0">
                <div class="col-md-3">
                    <div class="d-flex align-items-center justify-content-center ratio ratio-1x1">
                        <img id="imgImage" class="image-preset" src="..." runat="server" />
                    </div>
                </div>

                <div class="col-md-7">
                    <div class="row align-items-center justify-content-center gy-2">
                        <p class="col-10">
                            類別：
                            <asp:Literal ID="ltlCategoryName" runat="server" />
                        </p>
                        <h1 class="col-10">
                            書名：
                            <asp:Literal ID="ltlBookName" runat="server" />
                        </h1>
                        <h2 class="col-10">
                            作者：
                            <asp:Literal ID="ltlAuthorName" runat="server" />
                        </h2>
                        <h3 class="col-10">
                            描述：
                            <asp:Literal ID="ltlDescription" runat="server" />
                        </h3>
                        <p class="col-10">
                            價格：
                            <asp:Literal ID="ltlPrice" runat="server" />
                        </p>
                        <p class="col-10">
                            伊始期：
                            <asp:Literal ID="ltlDate" runat="server" />
                        </p>
                        <p id="paraEndDate" class="col-10" runat="server">
                            <asp:Literal ID="ltlEndDate" runat="server" />
                        </p>
                    </div>
                </div>

                <div class="col-md-2">
                    <div class="d-flex align-content-center justify-content-end">
                        <button id="btnAddShoppingCart" class="btn btn-success fs-4">加入購物車</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-11">
            <div class="row align-items-center justify-content-start gy-3">
                <h3 id="h3MayInterestBookList" class="ms-3 text-muted" runat="server">其他推薦書籍：</h3>

                <asp:Repeater ID="rptMayInterestBookList" runat="server">
                    <ItemTemplate>
                        <div class="col-md-3">
                            <a class="d-block btn" href="BookDetail.aspx?ID=<%# Eval("BookID") %>" title="前往查看：<%# Eval("BookName") %>">
                                <div class="card pt-5 pb-3 pt-md-3 pb-md-0">
                                    <asp:PlaceHolder runat="server" Visible='<%# 
                    !string.IsNullOrWhiteSpace(Eval("Image") as string) 
                %>'>
                                        <div class="d-flex align-items-center justify-content-center ratio ratio-1x1">
                                            <img class="card-img-top image-preset" src="<%# Eval("Image") %>" />
                                        </div>
                                    </asp:PlaceHolder>

                                    <div class="card-body d-flex flex-column align-items-center justify-content-center">
                                        <p class="card-text"><%# Eval("CategoryName") %></p>
                                        <h2 class="card-title">
                                            <%# Eval("BookName").ToString().Length > 6 
                                                    ? Eval("BookName").ToString().Substring(0, 5) + "..." 
                                                    : Eval("BookName") %>
                                        </h2>
                                        <p class="card-text"><%# Eval("AuthorName") %></p>
                                        <p class="card-text"><%# Eval("Price", "{0:0.#}") %>元</p>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
