$(document).ready(function () {
    if (window.location.href.indexOf("OrderDetail") === -1) {
        BuildShoppingCartBadge();
    }

    $("#btnShoppingCart").click(function (e) {
        e.preventDefault();

        window.location = "OrderDetail.aspx";
    })

    $("#btnAddShoppingCart").click(function (e) {
        e.preventDefault();

        var strOrBolBookID = GetUrlParameter("ID");
        if (typeof strOrBolBookID === "boolean")
            return;
        else
            AddShoppingCart(strOrBolBookID);
    });

    ToggleControlDisabledByCheckOrderBookAmount();

    $("#btnDeleteOrderBook").click(function (e) {
        e.preventDefault();

        var strCheckedBookID = GetCheckedBookID();
        DeleteOrderBook(strCheckedBookID);
    });

    $("#btnFinishOrder").click(function (e) {
        e.preventDefault();

        var strSelectedPaymentID = GetSelectedPaymentID();
        FinishOrder(strSelectedPaymentID, numOrderStatus = 1);
    })
})