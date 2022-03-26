<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPager.ascx.cs" Inherits="EBookStore.ShareControls.ucPager" %>

<div class="Pager">
    <a runat="server" id="aLinkFirst" href="BookList.aspx?Index=1"> 首頁 </a>
    <a runat="server" id="aLinkPrev" href="BookList.aspx?Index=1"> 上一頁 </a>

    <a runat="server" id="aLinkPage1" href="BookList.aspx?Index=1"> 1 </a>
    <a runat="server" id="aLinkPage2" href=""> 2 </a>
    <a runat="server" id="aLinkPage3" href="BookList.aspx?Index=3"> 3 </a>
    <a runat="server" id="aLinkPage4" href="BookList.aspx?Index=4"> 4 </a>
    <a runat="server" id="aLinkPage5" href="BookList.aspx?Index=5"> 5 </a>

    <a runat="server" id="aLinkNext" href="BookList.aspx?Index=3"> 下一頁 </a>
    <a runat="server" id="aLinkLast" href="BookList.aspx?Index=10"> 末頁 </a>
</div>
