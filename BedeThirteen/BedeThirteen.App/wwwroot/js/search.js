$("#filterByDdl").change(function () {
    $(".criteria").hide();
    $("#" + $(this).val()).show();
});

$("#searchBtn").click(function () {
    var filterBy = $("#filterByDdl").val();
    var filterCriteria = $("#" + $("#filterByDdl").val()).val();
    var pageSize = $("#pageSizeDdl").val();
    var sortBy = $("#sortByDdl").val();
    var url = "/Transaction/GetSearchResultsAsync?filterBy=" + filterBy + "&filterCriteria=" + filterCriteria + "&pageSize=" + pageSize + "&pageNumber=" + "0" + "&sortBy=" + sortBy;
    $.get({
        url: url
    }).done(function (response) {
        $("#searchResult").html(response);
    });
});

$(document).on("click", "#pageNumberBtn", function () {
    var filterBy = $("#filterByDdl").val();
    var filterCriteria = $("#" + $("#filterByDdl").val()).val();
    var pageSize = $("#pageSizeDdl").val();
    var sortBy = $("#sortByDdl").val();
    var pageN = $(this).val();
    var url = "/Transaction/GetSearchResultsAsync?filterBy=" + filterBy + "&filterCriteria=" + filterCriteria + "&pageSize=" + pageSize + "&pageNumber=" + pageN + "&sortBy=" + sortBy;
    $.get({
        url: url
    }).done(function (response) {
        $("#searchResult").html(response);
    });
});