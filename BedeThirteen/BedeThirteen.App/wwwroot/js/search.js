$("#filterByDdl").change(function () {
    $(".criteria").hide();
    $("#" + $(this).val()).show();
    if ($(this).val() === "date" || $(this).val() === "amount") {
        $("#" + $(this).val() + "A").show();
    }
});

$("#searchBtn").click(function () {
    var pageN = 0;
    GetSearchResult(pageN);
});

$(document).on("click", "#pageNumberBtn", function () {
    var pageN = $(this).val();
    GetSearchResult(pageN);
});

function GetSearchResult(pageNum) {
    var filterBy = $("#filterByDdl").val();
    var filterCriteria = $("#" + $("#filterByDdl").val()).val();
    var pageSize = $("#pageSizeDdl").val();
    var aditionalCriteria = "";
    var sortBy = $("#sortByDdl").val();

    if (filterBy === "date" || filterBy === "amount") {
        aditionalCriteria = $("#" + filterBy + "A").val();
    }
    var url = "/Transaction/GetSearchResultsAsync";
    $.get({
        url: url,
        data: {
            filterBy: filterBy,
            filterCriteria: filterCriteria,
            aditionalCriteria: aditionalCriteria,
            pageSize: pageSize,
            pageNumber: pageNum,
            sortBy: sortBy
        }
    }).done(function (response) {
        $("#searchResult").html(response);
    });
}
