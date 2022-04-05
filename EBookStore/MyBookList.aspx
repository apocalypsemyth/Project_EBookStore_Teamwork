<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="MyBookList.aspx.cs" Inherits="EBookStore.MyBookList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divMyBookList" class="container height-preset pt-5">
        <div class="row align-items-center justify-content-center mx-2 mx-md-0">
            <div class="col-md-9">
                <h3 id="h3MyBookListTitle" class="text-muted" runat="server">我的藏書：</h3>

                <ul class="list-group list-group-flush">
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <li class="list-group-item mb-3">
                                <div class="row align-items-center gy-4 py-4 pe-0 pe-md-4">
                                    <div class="col-md-4">
                                        <asp:PlaceHolder runat="server" Visible='<%#
                                !string.IsNullOrWhiteSpace(Eval("Image") as string)
                                %>'>
                                            <a class="btn d-flex align-items-center justify-content-center ratio ratio-1x1" href="BookDetail.aspx?ID=<%# Eval("BookID") %>" title="前往查看：<%# Eval("BookName") %>">
                                                <img class="image-preset" src="<%# Eval("Image") %>" />
                                            </a>
                                        </asp:PlaceHolder>
                                    </div>

                                    <div class="col-md-6">
                                        <a class="btn d-flex align-items-center justify-content-center" href="BookDetail.aspx?ID=<%# Eval("BookID") %>" title="前往查看：<%# Eval("BookName") %>">
                                            <%# Eval("BookName") %>
                                        </a>
                                    </div>

                                    <div class="col-md-2">
                                        <a class="btn btn-success d-flex align-items-center justify-content-center" href="MyBookDownload.aspx?ID=<%# Eval("BookID") %>" title="下載" style="text-decoration: none">下載
                                        </a>
                                    </div>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>

                <asp:PlaceHolder runat="server" ID="plcEmpty" Visible="false">
                    <div class="d-flex flex-column align-items-center justify-content-center gap-3">
                        <h1>您現在還沒有藏書</h1>
                        <a class="btn btn-success fs-4" href="BookList.aspx">前去看看</a>
                    </div>
                </asp:PlaceHolder>
            </div>
        </div>

    </div>
</asp:Content>
