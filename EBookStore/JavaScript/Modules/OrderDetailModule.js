var shoppingCartBadge = function (strOrderBookAmount) {
    let shoppingCartBadgeHtml =
        `
        購物車
        <span id="spnshoppingcartbadge" class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger">
            ${strOrderBookAmount}
            <span class="visually-hidden">unread messages</span>
        </span>
    `;

    return shoppingCartBadgeHtml;
}
var BuildShoppingCartBadge = function () {
    $.ajax({
        url: "/API/OrderDetailDataHandler.ashx",
        method: "GET",
        success: function (orderBookAmount) {
            if (orderBookAmount === "0" || orderBookAmount === "NOUSER")
                return;
            else {
                let shoppingCartBadgeHtml = shoppingCartBadge(orderBookAmount);

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

var GetUrlParameter = function (strParam) {
    let strPageUrl = window.location.search.substring(1),
        strUrlVariables = strPageUrl.split('&'),
        strParameterName

    for (let i = 0; i < strUrlVariables.length; i++) {
        strParameterName = strUrlVariables[i].split('=');

        if (strParameterName[0] === strParam) {
            return strParameterName[1] === undefined
                ? true
                : decodeURIComponent(strParameterName[1]);
        }
    }

    return false;
}
var AddShoppingCart = function (strBookID) {
    $.ajax({
        url: "/API/OrderDetailDataHandler.ashx?Action=CREATE",
        method: "POST",
        data: { "bookID": strBookID },
        success: function (orderBookAmount) {
            if (orderBookAmount === "0" || orderBookAmount === "NULL")
                alert("此書籍您已選購，請選擇其他書籍");
            else if (orderBookAmount === "NOUSER")
                alert("此為會員功能，請登入");
            else {
                let shoppingCartBadgeHtml = shoppingCartBadge(orderBookAmount);

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

var ToggleControlDisabled = function (strSelector, boolDisabled) {
    $(strSelector).attr("disabled", boolDisabled);
}
var ToggleControlDisabledByCheckOrderBookAmount = function () {
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

var GetCheckedBookID = function () {
    let checkedBookID = [];

    $("#divOrderBookList label.list-group-item input:checked").each(function () {
        let aHref = $(this).siblings("a.btn").attr("href");
        let strKeyValue = aHref.substring(aHref.indexOf("?") + 1);
        let strValue = strKeyValue.split("=")[1];

        checkedBookID.push(strValue);
    });

    return checkedBookID.join();
}
var orderBookInShoppingCart = function (strArrRemainedOrderBookList) {
    let orderBookInShoppingCartHtml = "";

    for (let orderBook of strArrRemainedOrderBookList) {
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

    return orderBookInShoppingCartHtml;
}
var DeleteOrderBook = function (strCheckedBookID) {
    $.ajax({
        url: "/API/OrderDetailDataHandler.ashx?Action=DELETE",
        method: "POST",
        data: { "checkedBookID": strCheckedBookID },
        success: function (remainedOrderBookList) {
            if (remainedOrderBookList === "NULL")
                alert("請選擇要刪除的書籍");
            else if (remainedOrderBookList === "NOUSER")
                alert("此為會員功能，請登入");
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

                let orderBookInShoppingCartHtml = orderBookInShoppingCart(remainedOrderBookList);

                $("#divOrderBookList").html(orderBookInShoppingCartHtml);
            }
        },
        error: function (msg) {
            console.log(msg);
            alert("通訊失敗，請聯絡管理員。");
        }
    });
}

var GetSelectedPaymentID = function () {
    let selectedPaymentID = "";
    selectedPaymentID = $("select[id*=ddlPaymentList] option").filter(":selected").val();

    return selectedPaymentID;
}
var finishedOrderDetailTableHead = function (strArrColNameList) {
    let finishedOrderDetailTableHeadHtml = "";

    for (let colName of strArrColNameList) {
        finishedOrderDetailTableHeadHtml +=
            `
                <th scope="col">${colName}</th>
            `;
    }

    return "<tr>" + finishedOrderDetailTableHeadHtml + "</tr>";
}
var finishedOrderDetailTableBody = function (strArrFinishedOrderBookList) {
    let finishedOrderDetailTableBodyHtml = "";

    for (let finishedOrderBook of strArrFinishedOrderBookList) {
        finishedOrderDetailTableBodyHtml +=
            `
                <tr>
                    <td>
                        <img style="max-height: 20vh; object-fit: contain;" src="${finishedOrderBook.Image}" />
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

    return finishedOrderDetailTableBodyHtml;
}
var finishedOrderDetailTable = function (strTableHead, strTableBody) {
    let finishedOrderDetailTableHtml =
        `
            <div class="row align-items-center justify-content-center">
                <div class="col-md-9">
                    <table class="table">
                        <thead>
                            ${strTableHead}
                        </thead>
                        <tbody>
                            ${strTableBody}
                        </tbody>
                    </table>
                </div>
            </div>
        `;

    return finishedOrderDetailTableHtml;
}
var btnBackToHome = `
        <div class="row align-items-center justify-content-center">
            <div class="col-md-9">
                <button id="btnBackToHome">回到首頁</button>
            </div>
        </div>
    `;
var FinishOrder = function (strSelectedPaymentID, numOrderStatus) {
    var objPostOrderDetail = {
        "selectedPaymentID": strSelectedPaymentID,
        "orderStatus": numOrderStatus,
    };

    $.ajax({
        url: "/API/OrderDetailDataHandler.ashx?Action=UPDATE",
        method: "POST",
        data: objPostOrderDetail,
        success: function (finishedOrderBookList) {
            if (finishedOrderBookList === "NULL")
                alert("發生錯誤，請再重試");
            else if (finishedOrderBookList === "NOUSER")
                alert("此為會員功能，請登入");
            else {
                var colNameList = ["封面圖", "書名", "價格"];
                let tableHeadHtml = finishedOrderDetailTableHead(colNameList);
                let tableBodyHtml = finishedOrderDetailTableBody(finishedOrderBookList);

                $("#divOrderDetailTable")
                    .html(
                        finishedOrderDetailTable(tableHeadHtml, tableBodyHtml) +
                        btnBackToHome
                    );

                $("#btnBackToHome").on("click", function (e) {
                    e.preventDefault();

                    window.location = "BookList.aspx";
                });
            }
        },
        error: function (msg) {
            console.log(msg);
            alert("通訊失敗，請聯絡管理員。");
        }
    });
}