var ToMyBookList = function () {
    $.ajax({
        url: "/API/ucNavbarDataHandler.ashx",
        method: "GET",
        success: function (userID) {
            if (userID === "NULL")
                window.location = "BookList.aspx";
            else
                $("#divNavbarCollapseContainer #ulNavbarList #aLinkMyBookList")
                    .attr("href", "MyBookList.aspx?ID=" + userID);
        },
        error: function (msg) {
            console.log(msg);
            alert("通訊失敗，請聯絡管理員。");
        }
    });
}