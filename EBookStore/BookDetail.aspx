<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BookDetail.aspx.cs" Inherits="EBookStore.BookDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Css/BookDetail.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row align-items-center justify-content-center">
            <div class="col-md-4">
                <div class="row align-items-center justify-content-center">
                    <img id="imgImage" class="img-thumbnail rounded" src="..." runat="server" />
                </div>
            </div>

            <div class="col-md-4">
                <div class="row align-items-center justify-content-center">
                    <p class="col-12">
                        類別：
                        <asp:Literal ID="ltlCategoryName" runat="server" />
                    </p>
                    <h1 class="col-12">
                        書名：
                        <asp:Literal ID="ltlBookName" runat="server" />
                    </h1>
                    <h2 class="col-12">
                        作者：
                        <asp:Literal ID="ltlAuthorName" runat="server" />
                    </h2>
                    <h3 class="col-12">
                        描述：
                        <asp:Literal ID="ltlDescription" runat="server" />
                    </h3>
                    <p class="col-12">
                        價格：
                        <asp:Literal ID="ltlPrice" runat="server" />
                    </p>
                    <p class="col-12">
                        伊始期：
                        <asp:Literal ID="ltlDate" runat="server" />
                    </p>
                    <p class="col-12">
                        <asp:Literal ID="ltlEndDate" runat="server" />
                    </p>
                </div>
            </div>

            <div class="col-md-4">
                <div class="book__add-shopping-cart-container">
                    <button id="btnAddShoppingCart" class="book__add-shopping-cart">加入購物車</button>
                </div>
            </div>
        </div>

        <div class="row align-items-center justify-content-center">
            <asp:Repeater ID="rptMayInterestBookList" runat="server">
                <ItemTemplate>
                    <div class="col-md-3">
                        <div class="card">
                            <a class="btn" href="BookDetail.aspx?ID=<%# Eval("BookID") %>" title="前往查看：<%# Eval("BookName") %>">
                                <img class="card-img-top" src="<%# Eval("Image") %>" />
                                <div class="card-body">
                                    <h5 class="card-title">書名:
                                        <%# Eval("BookName") %>
                                    </h5>
                                    <p class="card-text"><%# Eval("Price") %></p>
                                </div>
                            </a>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
