<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" Inherits="EBookStore.OrderDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divOrderDetailTable" class="container height-preset">
        <div class="row align-items-center justify-content-center mx-2 mx-md-0 gy-5">
            <div class="col-md-9">
                <asp:DropDownList ID="ddlPaymentList" runat="server"></asp:DropDownList>
            </div>

            <div class="col-md-9">
                <div id="divOrderBookList" class="list-group">
                    <asp:Repeater ID="rptOrderBookList" runat="server">
                        <ItemTemplate>
                            <label class="list-group-item">
                                <div class="row align-items-center">
                                    <div class="col-1">
                                        <input class="form-check-input me-1" type="checkbox" />
                                    </div>

                                    <div class="col-11">
                                        <a class="d-block btn" href="BookDetail.aspx?ID=<%# Eval("BookID") %>" title="前往查看：<%# Eval("BookName") %>">
                                            <div class="card border-0">
                                                <div class="row g-0">
                                                    <div class="col-12 col-md-4">
                                                        <div class="d-flex align-items-center justify-content-center ratio ratio-1x1">
                                                            <img class="image-preset" src="<%# Eval("Image") %>" />
                                                        </div>
                                                    </div>

                                                    <div class="col-12 col-md-8 align-self-center">
                                                        <div class="card-body">
                                                            <h2 class="card-title">書名：<%# Eval("BookName") %></h2>
                                                            <p class="card-text">價格：<%# Eval("Price", "{0:0.#}") %>元</p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                            </label>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <asp:PlaceHolder runat="server" ID="plcOrderBookEmpty" Visible="false">
                    <div class="d-flex flex-column align-items-center justify-content-center gap-3">
                        <h1>您現在沒有選購的書籍</h1>
                        <a class="btn btn-success fs-4" href="BookList.aspx">前去看看</a>
                    </div>
                </asp:PlaceHolder>
            </div>

            <div class="col-md-9">
                <div class="d-flex align-items-center justify-content-md-end">
                    <button id="btnDeleteOrderBook" class="btn btn-danger fs-5 visibility-preset">刪除選購書籍</button>
                </div>
            </div>
            
            <div class="col-md-9">
                <div class="d-flex align-items-center justify-content-md-end">
                    <button id="btnFinishOrder" class="btn btn-success fs-5 visibility-preset">完成交易</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
