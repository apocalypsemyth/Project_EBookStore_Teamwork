<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BookDetail.aspx.cs" Inherits="EBookStore.BookDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Css/BookDetail.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row align-items-center justify-content-center">
        <div class="col-md-11">
            <div class="row align-items-center justify-content-center">
                <div class="col-md-3">
                    <div class="d-flex align-items-center justify-content-center">
                        <img id="imgImage" class="img-thumbnail rounded-3 w-100 image-preset" src="..." runat="server" />
                    </div>
                </div>

                <div class="col-md-7">
                    <div class="row align-items-center justify-content-center">
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
                    <button id="btnAddShoppingCart" class="btn btn-success">加入購物車</button>
                </div>
            </div>
        </div>

        <div class="col-md-11">
            <div class="row align-items-center justify-content-start">
                <asp:Repeater ID="rptMayInterestBookList" runat="server">
                    <ItemTemplate>
                        <div class="col-md-3">
                            <a class="d-block btn" href="BookDetail.aspx?ID=<%# Eval("BookID") %>" title="前往查看：<%# Eval("BookName") %>">
                                <div class="card">
                                    <asp:PlaceHolder runat="server" Visible='<%# 
                    !string.IsNullOrWhiteSpace(Eval("Image") as string) 
                %>'>
                                        <div class="d-flex align-items-center justify-content-center ratio ratio-1x1">
                                            <img class="card-img-top image-preset" src="<%# Eval("Image") %>" />
                                        </div>
                                    </asp:PlaceHolder>

                                    <div class="card-body d-flex flex-column align-items-center justify-content-center">
                                        <p class="card-text"><%# Eval("CategoryName") %></p>
                                        <h2 class="card-title"><%# Eval("BookName") %></h2>
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
