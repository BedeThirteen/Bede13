$("#dateBtn").click(function () {
    let value = $(this).val();
    let pageSize = $("#pageSizeDdl").val();
    let url = "/Administration/Home/GetTransactions?sortOrder=" + value + "&selectedPageSize=" + pageSize + "&currentPage=" + "0";

    $.get({
        url: url
    }).done(function (response) {
        if (value === "date") { $("#dateBtn").val("date_desc"); }
        else { $("#dateBtn").val("date"); }
        $("#resultsDiv").html(response);
    });
});

$("#amountBtn").click(function () {
    let value = $(this).val();
    let pageSize = $("#pageSizeDdl").val();
    let url = "/Administration/Home/GetTransactions?sortOrder=" + value + "&selectedPageSize=" + pageSize + "&currentPage=" + "0";

    $.get({
        url: url
    }).done(function (response) {
        if (value === "amount") { $("#amountBtn").val("amount_desc"); }
        else { $("#amountBtn").val("amount"); }
        $("#resultsDiv").html(response);
    });
});

$(document).on("click", ".pageNumberBtn", function () {
    var pageSize = $("#pageSizeDdl").val();
    var pageN = $(this).val();
    var sortOrder = "";
    if ($("#dateBtn").val().includes("desc")) { sortOrder = "date_desc"; }
    else if ($("#amountBtn").val().includes("desc")) { sortOrder = "amount_desc"; }
    var url = "/Administration/Home/GetTransactions?sortOrder=" + sortOrder + "&selectedPageSize=" + pageSize + "&currentPage=" + pageN;
    $.get({
        url: url
    }).done(function (response) {
        $("#resultsDiv").html(response);
    });
});

$("#pageSizeDdl").change(function () {
    let value = $(this).val();
    let url = "/Administration/Home/GetTransactions?sortOrder=" + "" + "&selectedPageSize=" + value + "&currentPage=" + "0";

    $.get({
        url: url
    }).done(function (response) {
        if (value === "amount") { $("#amountBtn").val("amount_desc"); }
        else { $("#amountBtn").val("amount"); }
        $("#resultsDiv").html(response);
    });
});