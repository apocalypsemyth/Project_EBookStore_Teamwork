<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AboutEHon.aspx.cs" Inherits="EBookStore.AboutEHon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Css/AboutEHon.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row align-items-center justify-content-center height-preset mx-2 mx-md-0 pt-5">
        <div class="col-md-10">
            <h3 class="text-muted">關於我們：</h3>

            <div class="d-flex flex-column align-content-center justify-content-center gap-3 about__introduction-container">
                <p class="fs-3 p-4">
                    自從2007年，蘋果公司發佈全世界第一部智慧型手機為開端，我們的生活就不斷朝著輕量、便攜等概念前進。
                </p>

                <p class="fs-3 p-4">
                    E本就是因此思潮而孕育而生的，一個致力於推廣知識數位化的電子書平台。
                </p>

                <p class="fs-3 p-4">
                    不需要任何閱讀器，只要一機在手，世界擁有，隨想隨讀，暢遊在資訊的江河裡。
                </p>

                <p class="fs-3 p-4">
                    E本在這裡，盡量蒐羅一切書籍，滿足日益多元化的需求，也望在未來眼見可及的數位盛宴裡，與您再相會。
                </p>
            </div>
        </div>
    </div>
</asp:Content>
