<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="MyBookList.aspx.cs" Inherits="EBookStore.MyBookList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divMyBookList">        
        <asp:Repeater ID="rptList" runat="server">
            <ItemTemplate>
                <div class="row align-items-center">
                    <div class="col-3 gy-5 text-center">
                        <asp:PlaceHolder runat="server" Visible='<%#
                    !string.IsNullOrWhiteSpace(Eval("Image") as string)
                    %>'>
                            <a href="BookDetail.aspx?ID=<%# Eval("BookID") %>" title="前往查看：<%# Eval("BookName") %>">
                                <img src="<%# Eval("Image") %>" height="160" class="img-responsive center-block" />
                            </a>
                        </asp:PlaceHolder>
                    </div>
                    <div class="col-2">
                        <%# Eval("BookName") %>
                    </div>
                    <div class="col-2">
                        <%# Eval("CategoryName") %>
                    </div>
                    <div class="col-2">
                        <%# Eval("AuthorName") %>
                    </div>
                    <div class="col-3">
                        <a href="MyBookDownload.aspx?ID=<%# Eval("BookID") %>" title="下載" style="text-decoration: none">下載
                        </a>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>


        <asp:PlaceHolder runat="server" ID="plcEmpty" Visible="false">
            <p>查無結果 </p>
        </asp:PlaceHolder>
    </div>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/jquery.min.js"></script>
</asp:Content>

