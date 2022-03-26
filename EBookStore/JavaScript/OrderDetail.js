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
    BuildShoppingCartBadge();

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
    function DeleteOrderBookInShoppingCart(strCheckedBookID) {
        $.ajax({
            url: "/API/OrderDetailDataHandler.ashx?Action=DELETE",
            method: "POST",
            data: { "checkedBookID": strCheckedBookID },
            success: function (remainedOrderBookList) {
                if (remainedOrderBookList === "NULL") {
                    alert("請選擇要刪除的書籍");
                }
                else {
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
        DeleteOrderBookInShoppingCart(strCheckedBookID);
        BuildShoppingCartBadge();
    });

    function GetSelectedPaymentID() {
        var strSelectedPaymentID = "";
        strSelectedPaymentID = $("select[id*=ddlPaymentList] option").filter(":selected").val();

        return strSelectedPaymentID;
    }
    function CompleteOrder(strSelectedPaymentID, numOrderStatus) {
        var objPostOrderDetail = {
            "selectedPaymentID": strSelectedPaymentID,
            "orderStatus": numOrderStatus,
        };

        $.ajax({
            url: "/API/OrderDetailDataHandler.ashx?Action=UPDATE",
            method: "POST",
            data: objPostOrderDetail,
            success: function (orderedBookList) {
                var colNameList = ["封面圖", "書名", "價格"];

                var completedOrderDetailTableHead = "";
                for (var colName of colNameList) {
                    completedOrderDetailTableHead +=
                        `
                            <th scope="col">${colName}</th>
                        `;
                }

                var completedOrderDetailTableBody = "";
                for (var orderedBook of orderedBookList) {
                    completedOrderDetailTableBody +=
                        `
                            <tr>
                                <td>
                                    <img src="${orderedBook.Image}" />
                                </td>
                                <td>
                                    ${orderedBook.BookName}
                                </td>
                                <td>
                                    ${orderedBook.Price}
                                </td>
                            </tr>
                        `;
                }

                $("#divOrderDetailTable")
                    .html(
                        "<table class='table'>" +
                            "<thead>" +
                                "<tr>" +
                                    completedOrderDetailTableHead +
                                "</tr>" +
                            "</thead>" +
                            "<tbody>" +
                                completedOrderDetailTableBody +
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
    $("#btnCompleteOrder").click(function (e) {
        e.preventDefault();

        var strSelectedPaymentID = GetSelectedPaymentID();
        CompleteOrder(strSelectedPaymentID, numOrderStatus = 1);
    })
})