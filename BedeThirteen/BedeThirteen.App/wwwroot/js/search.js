$("#filterByDdl").change(function () {
    $(".criteria").hide();
    $("#" + $(this).val()).show();
    if ($(this).val() === "date" || $(this).val() === "amount") {
        $("#" + $(this).val() + "A").show();
    }
});

$("#searchBtn").click(function () {
    var filterBy = $("#filterByDdl").val();
    var filterCriteria = $("#" + $("#filterByDdl").val()).val();
    var aditionalCriteria = "";
    if (filterBy === "date" || filterBy === "amount") {
        aditionalCriteria = $("#" + filterBy + "A").val();
    }
    var pageSize = $("#pageSizeDdl").val();
    var sortBy = $("#sortByDdl").val();
    var url = "/Transaction/GetSearchResultsAsync?filterBy=" + filterBy + "&filterCriteria=" + filterCriteria + "&aditionalCriteria=" + aditionalCriteria + "&pageSize=" + pageSize + "&pageNumber=" + "0" + "&sortBy=" + sortBy;
    $.get({
        url: url
    }).done(function (response) {
        $("#searchResult").html(response);
    });
});

$(document).on("click", "#pageNumberBtn", function () {
    var filterBy = $("#filterByDdl").val();
    var filterCriteria = "";
    if (filterBy !== "all") { filterCriteria = $("#" + $("#filterByDdl").val()).val(); }
    var pageSize = $("#pageSizeDdl").val();
    var sortBy = $("#sortByDdl").val();
    var pageN = $(this).val();
    var aditionalCriteria = "";
    if (filterBy === "date" || filterBy === "amount") {
        aditionalCriteria = $("#" + filterBy + "A").val();
    }
    var url = "/Transaction/GetSearchResultsAsync?filterBy=" + filterBy
        + "&filterCriteria=" + filterCriteria
        + "&aditionalCriteria=" + aditionalCriteria
        + "&pageSize=" + pageSize
        + "&pageNumber=" + pageN
        + "&sortBy=" + sortBy;
    $.get({
        url: url
    }).done(function (response) {
        $("#searchResult").html(response);
    });
});