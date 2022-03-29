<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BookList.aspx.cs" Inherits="EBookStore.BookList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Css/BookList.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row align-items-center justify-content-center">
        <div class="col-md-8">
            <div id="divControlCarousel" class="carousel slide" data-bs-ride="carousel">
                <div class="carousel-indicators">
                    <button type="button" data-bs-target="#divControlCarousel" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
                    <button type="button" data-bs-target="#divControlCarousel" data-bs-slide-to="1" aria-label="Slide 2"></button>
                    <button type="button" data-bs-target="#divControlCarousel" data-bs-slide-to="2" aria-label="Slide 3"></button>
                </div>

                <div class="carousel-inner ratio ratio-16x9"></div>

                <button class="carousel-control-prev" type="button" data-bs-target="#divControlCarousel" data-bs-slide="prev">
                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                    <span class="visually-hidden">Previous</span>
                </button>
                <button class="carousel-control-next" type="button" data-bs-target="#divControlCarousel" data-bs-slide="next">
                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                    <span class="visually-hidden">Next</span>
                </button>
            </div>
        </div>

        <div class="col-md-12">
            <div class="card-list">
        <asp:Repeater ID="rptList" runat="server">
            <ItemTemplate>
                <div class="card">
                    <asp:PlaceHolder runat="server" Visible='<%# 
                    !string.IsNullOrWhiteSpace(Eval("Image") as string) 
                %>'>
                        <p>
                            <a href="BookDetail.aspx?ID=<%# Eval("BookID") %>" title="前往查看：<%# Eval("BookName") %>">
                                <asp:Image ID="ImageFromDB" runat="server" ImageUrl='<%# Eval("Image") %>' Width="250px" Height="180px" />
                            </a>
                        </p>
                    </asp:PlaceHolder>

                    <p>
                        <a href="BookDetail.aspx?ID=<%# Eval("BookID") %>" title="前往查看：<%# Eval("BookName") %>">
                            <%# Eval("BookName") %> 
                        </a>
                    </p>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

            <asp:PlaceHolder runat="server" ID="plcEmpty" Visible="false">
                <p>尚未有資料 </p>
                <img src="TryBook.jpg" />
            </asp:PlaceHolder>
        </div>
    </div>


</asp:Content>
