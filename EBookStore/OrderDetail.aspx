<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" Inherits="EBookStore.OrderDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divOrderDetailTable" class="container">
        <div class="row align-items-center justify-content-center gy-4">
            <div class="col-md-9">
                <asp:DropDownList ID="ddlPaymentList" runat="server"></asp:DropDownList>
            </div>

            <div class="col-md-9">
                <div id="divOrderBookList" class="list-group">
                    <asp:Repeater ID="rptOrderBookList" runat="server">
                        <ItemTemplate>
                            <label class="list-group-item">
                                <input class="form-check-input me-1" type="checkbox" />
                                <a class="btn" href="BookDetail.aspx?ID=<%# Eval("BookID") %>" title="前往查看：<%# Eval("BookName") %>">
                                    <img class="card-img-top" src="<%# Eval("Image") %>" />
                                    <div class="card-body">
                                        <h5 class="card-title">書名:<%# Eval("BookName") %>
                                        </h5>
                                        <p class="card-text"><%# Eval("Price") %></p>
                                    </div>
                                </a>
                            </label>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <asp:PlaceHolder runat="server" ID="plcOrderBookEmpty" Visible="false">
                    <p>尚未有資料 </p>
                </asp:PlaceHolder>
            </div>

            <div class="col-md-9">
                <button id="btnDeleteOrderBook">刪除選購書籍</button>
            </div>
            
            <div class="col-md-9">
                <button id="btnCompleteOrder">完成交易</button>
            </div>
        </div>
    </div>
</asp:Content>
