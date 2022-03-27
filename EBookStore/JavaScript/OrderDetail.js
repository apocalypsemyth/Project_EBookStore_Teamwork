$(document).ready(function () {
    function BuildShoppingCartBadge() {
        $.ajax({
            url: "/API/OrderDetailDataHandler.ashx",
            method: "GET",
            success: function (orderBookAmount) {
                if (orderBookAmount === "0" || orderBookAmount === "NULL")
                    return;
                else {
                    var shoppingCartBadgeHtml =
                        `
                            購物車
                            <span id="spnshoppingcartbadge" class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger">
                                ${orderBookAmount}
                                <span class="visually-hidden">unread messages</span>
                            </span>
                        `;

                    $("#btnShoppingCart").empty();
                    $("#btnShoppingCart").append(shoppingCartBadgeHtml);
                }
            },
            error: function (msg) {
                console.log(msg);
                alert("通訊失敗，請聯絡管理員。");
            }
        });
    }
    if (window.location.href.indexOf("OrderDetail") === -1) {
        BuildShoppingCartBadge();
    }

    $("#btnShoppingCart").click(function (e) {
        e.preventDefault();

        window.location = "OrderDetail.aspx";
    })

    function GetUrlParameter(strParam) {
        var strPageUrl = window.location.search.substring(1),
            strUrlVariables = strPageUrl.split('&'),
            strParameterName

        for (var i = 0; i < strUrlVariables.length; i++) {
            strParameterName = strUrlVariables[i].split('=');

            if (strParameterName[0] === strParam) {
                return strParameterName[1] === undefined
                    ? true
                    : decodeURIComponent(strParameterName[1]);
            }
        }

        return false;
    }
    function AddShoppingCart(strBookID) {
        $.ajax({
            url: "/API/OrderDetailDataHandler.ashx?Action=CREATE",
            method: "POST",
            data: { "bookID": strBookID },
            success: function (orderBookAmount) {
                if (orderBookAmount === "0" || orderBookAmount === "NULL")
                    alert("此書籍您已選購，請選擇其他書籍");
                else {
                    var shoppingCartBadgeHtml =
                        `
                            購物車
                            <span id="spnshoppingcartbadge" class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger">
                                ${orderBookAmount}
                                <span class="visually-hidden">unread messages</span>
                            </span>
                        `;

                    $("#btnShoppingCart").empty();
                    $("#btnShoppingCart").append(shoppingCartBadgeHtml);
                }
            },
            error: function (msg) {
                console.log(msg);
                alert("通訊失敗，請聯絡管理員。");
            }
        });
    }
    $("#btnAddShoppingCart").click(function (e) {
        e.preventDefault();

        var strOrBolBookID = GetUrlParameter("ID");
        if (typeof strOrBolBookID === "boolean")
            return;
        else
            AddShoppingCart(strOrBolBookID);
    });

    function ToggleControlDisabled(strSelector, boolDisabled) {
        $(strSelector).attr("disabled", boolDisabled);
    }
    function ToggleControlDisabledByCheckOrderBookAmount() {
        $.ajax({
            url: "/API/OrderDetailDataHandler.ashx",
            method: "GET",
            success: function (orderBookAmount) {
                if (orderBookAmount === "0" || orderBookAmount === "NULL") {
                    ToggleControlDisabled("select[id$=ddlPaymentList]", true);
                    ToggleControlDisabled("#btnDeleteOrderBook", true);
                    ToggleControlDisabled("#btnFinishOrder", true);
                }
                else {
                    ToggleControlDisabled("select[id$=ddlPaymentList]", false);
                    ToggleControlDisabled("#btnDeleteOrderBook", false);
                    ToggleControlDisabled("#btnFinishOrder", false);
                }
            },
            error: function (msg) {
                console.log(msg);
                alert("通訊失敗，請聯絡管理員。");
            }
        });
    }
    ToggleControlDisabledByCheckOrderBookAmount();

    function GetCheckedBookID() {
        var checkedBookID = [];

        $("#divOrderBookList label.list-group-item input:checked").each(function () {
            var aHref = $(this).siblings("a.btn").attr("href");
            var strKeyValue = aHref.substring(aHref.indexOf("?") + 1);
            var strValue = strKeyValue.split("=")[1];

            checkedBookID.push(strValue);
        });

        return checkedBookID.join();
    }
    function DeleteOrderBook(strCheckedBookID) {
        $.ajax({
            url: "/API/OrderDetailDataHandler.ashx?Action=DELETE",
            method: "POST",
            data: { "checkedBookID": strCheckedBookID },
            success: function (remainedOrderBookList) {
                if (remainedOrderBookList === "NULL") 
                    alert("請選擇要刪除的書籍");
                else if (!remainedOrderBookList.length) {
                    $("#divOrderBookList").empty();

                    ToggleControlDisabled("select[id$=ddlPaymentList]", true);
                    ToggleControlDisabled("#btnDeleteOrderBook", true);
                    ToggleControlDisabled("#btnFinishOrder", true);
                }
                else {
                    ToggleControlDisabled("select[id$=ddlPaymentList]", false);
                    ToggleControlDisabled("#btnDeleteOrderBook", false);
                    ToggleControlDisabled("#btnFinishOrder", false);

                    var orderBookInShoppingCartHtml = "";
                    for (var orderBook of remainedOrderBookList) {
                        orderBookInShoppingCartHtml +=
                            `
                                <label class="list-group-item">
                                    <input class="form-check-input me-1" type="checkbox" />
                                    <a class="btn" href="BookDetail.aspx?ID=${orderBook.BookID}" title="前往查看：${orderBook.BookName}">
                                        <img class="card-img-top" src="${orderBook.Image}" />
                                        <div class="card-body">
                                            <h5 class="card-title">書名:${orderBook.BookName}
                                            </h5>
                                            <p class="card-text">${orderBook.Price}</p>
                                        </div>
                                    </a>
                                </label>
                            `;
                    }

                    $("#divOrderBookList").html(orderBookInShoppingCartHtml);
                }
            },
            error: function (msg) {
                console.log(msg);
                alert("通訊失敗，請聯絡管理員。");
            }
        });
    }
    $("#btnDeleteOrderBook").click(function (e) {
        e.preventDefault();

        var strCheckedBookID = GetCheckedBookID();
        DeleteOrderBook(strCheckedBookID);
    });

    function GetSelectedPaymentID() {
        var strSelectedPaymentID = "";
        strSelectedPaymentID = $("select[id*=ddlPaymentList] option").filter(":selected").val();

        return strSelectedPaymentID;
    }
    function FinishOrder(strSelectedPaymentID, numOrderStatus) {
        var objPostOrderDetail = {
            "selectedPaymentID": strSelectedPaymentID,
            "orderStatus": numOrderStatus,
        };

        $.ajax({
            url: "/API/OrderDetailDataHandler.ashx?Action=UPDATE",
            method: "POST",
            data: objPostOrderDetail,
            success: function (finishedOrderBookList) {
                var colNameList = ["封面圖", "書名", "價格"];

                var finishedOrderDetailTableHead = "";
                for (var colName of colNameList) {
                    finishedOrderDetailTableHead +=
                        `
                            <th scope="col">${colName}</th>
                        `;
                }

                var finishedOrderDetailTableBody = "";
                for (var finishedOrderBook of finishedOrderBookList) {
                    finishedOrderDetailTableBody +=
                        `
                            <tr>
                                <td>
                                    <img src="${finishedOrderBook.Image}" />
                                </td>
                                <td>
                                    ${finishedOrderBook.BookName}
                                </td>
                                <td>
                                    ${finishedOrderBook.Price}
                                </td>
                            </tr>
                        `;
                }

                $("#divOrderDetailTable")
                    .html(
                        "<table class='table'>" +
                            "<thead>" +
                                "<tr>" +
                                    finishedOrderDetailTableHead +
                                "</tr>" +
                            "</thead>" +
                            "<tbody>" +
                                finishedOrderDetailTableBody +
                            "</tbody>" +
                        "</table>"
                    );
            },
            error: function (msg) {
                console.log(msg);
                alert("通訊失敗，請聯絡管理員。");
            }
        });
    }
    $("#btnFinishOrder").click(function (e) {
        e.preventDefault();

        var strSelectedPaymentID = GetSelectedPaymentID();
        FinishOrder(strSelectedPaymentID, numOrderStatus = 1);
    })
})