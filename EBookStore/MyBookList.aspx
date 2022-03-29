<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="MyBookList.aspx.cs" Inherits="EBookStore.MyBookList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divMyBookList">
        <asp:Repeater ID="rptList" runat="server">
            <ItemTemplate>
                <div>
                    <p>
                        <a href="BookDetail.aspx?ID=<%# Eval("BookID") %>" title="前往查看：<%# Eval("BookName") %>">
                            <%# Eval("BookName") %>
                        </a>
                        <%# Eval("CategoryName") %> <%# Eval("AuthorName") %>
                    </p>

                    <asp:PlaceHolder runat="server" Visible='<%#
                    !string.IsNullOrWhiteSpace(Eval("Image") as string)
                    %>'>
                        <a href="BookDetail.aspx?ID=<%# Eval("BookID") %>" title="前往查看：<%# Eval("BookName") %>">
                            <img src="<%# Eval("Image") %>" width="200" height="160" />
                        </a>
                    </asp:PlaceHolder>

                    <a href="MyBookDownload.aspx?ID=<%# Eval("BookID") %>" title="下載" style="text-decoration: none">下載
                    </a>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <asp:PlaceHolder runat="server" ID="plcEmpty" Visible="false">
            <p>查無結果 </p>
        </asp:PlaceHolder>
    </div>
</asp:Content>
