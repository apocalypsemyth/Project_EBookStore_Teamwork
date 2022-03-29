var carouselItem = function (objBook, boolActiveClass = false) {
    let carouselItemActiveClass = "active";
    let carouselItemHtml =
        `
            <div class="carousel-item ${boolActiveClass ? carouselItemActiveClass : ""}" data-bs-interval="2500">
                <a href="BookDetail.aspx?ID=${objBook.BookID}" title="前往查看：${objBook.BookName}">
                    <img style="max-height: 100%; object-fit: contain;" class="w-100" src="${objBook.Image}" alt="${objBook.BookName}">
                </a>
                <div class="carousel-caption d-none d-md-block">
                    <h5>
                        ${objBook.BookName}
                    </h5>
                    <p>
                        ${objBook.Description}
                    </p>
                </div>
            </div>
        `;

    return carouselItemHtml;
}

var BuildCarouselList = function (strArrBookList) {
    let carouselListHtml = "";

    for (let idx = 0; idx < strArrBookList.length; idx++) {
        if (idx === 0) 
            carouselListHtml += carouselItem(strArrBookList[idx], true);
        else
            carouselListHtml += carouselItem(strArrBookList[idx]);
    }

    return carouselListHtml;
}

var ShowCarousel = function () {
    $.ajax({
        url: "/API/BookListDataHandler.ashx",
        method: "GET",
        success: function (bookList) {
            if (bookList === "NULL")
                $("#divControlCarousel").parent().hide();
            else {
                let carouselListHtml = BuildCarouselList(bookList);

                $("#divControlCarousel div.carousel-inner").empty();
                $("#divControlCarousel div.carousel-inner").append(carouselListHtml);
            }
        },
        error: function (msg) {
            console.log(msg);
            alert("通訊失敗，請聯絡管理員。");
        }
    });
}