// 這是利用Jquery請求API至資料庫抓資料部分實作
$(document).ready(function () {
    $("button[id=btnSearch2]").click(function (event) {
        event.preventDefault();
        var inputText = $("input[name=searchText]").val();

        $.ajax({
            url: `/API/BookListDataHandler.ashx`,
            method: "GET",
            data: { "Name": inputText },
            dataType: "JSON",
            success: function (objData) {
                console.log(objData);
                $("#testInput[name=testInput]").val(objData.BookName);
            },
            error: function (msg) {
                console.log(msg);
                alert("Connect fail, please contact admin");
            }
        });
    });
});