<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SearchedBookList.aspx.cs" Inherits="EBookStore.SearchedBookList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row align-items-center justify-content-center height-preset mx-2 mx-md-0 pt-5">
        <div class="col-md-8">
            <h3 class="text-muted">搜尋結果：</h3>

            <asp:Repeater ID="rptSearchedBookList" runat="server">
                <ItemTemplate>
                    <div class="card mb-3 mx-md-0 rounded-3">
                        <div class="row pt-5 pb-3 py-md-3 px-3">
                            <div class="col-md-4 d-flex align-items-center justify-content-center">
                                <asp:PlaceHolder runat="server" Visible='<%# 
                                    !string.IsNullOrWhiteSpace(Eval("Image") as string) 
                                %>'>
                                    <a class="btn d-flex align-items-center justify-content-center ratio ratio-1x1" href="BookDetail.aspx?ID=<%# Eval("BookID") %>" title="前往查看：<%# Eval("BookName") %>">
                                        <img class="image-preset" src="<%# Eval("Image") %>" alt="<%# Eval("BookName") %>">
                                    </a>
                                </asp:PlaceHolder>
                            </div>

                            <div class="col-md-8">
                                <div class="card-body d-flex flex-column align-items-center justify-content-center align-items-md-start justify-content-md-start">
                                    <a class="text-decoration-none" href="BookDetail.aspx?ID=<%# Eval("BookID") %>" title="前往查看：<%# Eval("BookName") %>">
                                        <h2 class="card-title">書名：<%# Eval("BookName") %></h2>
                                    </a>
                                    <h5 class="card-text">描述：<%# Eval("Description") %></h5>
                                    <p class="card-text">價格：<%# Eval("Price", "{0:0.#}") %>元</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <asp:PlaceHolder runat="server" ID="plcEmpty" Visible="false">
                <p>尚未有資料 </p>
                <img src="TryBook.jpg" />
            </asp:PlaceHolder>
        </div>
    </div>
</asp:Content>