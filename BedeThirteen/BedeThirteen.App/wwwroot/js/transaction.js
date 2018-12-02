$("#dateBtn").click(function () {
    let value = $(this).val();
    let url = "/Administration/Home/GetTransactions?sortOrder=" + value;

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
    let url = "/Administration/Home/GetTransactions?sortOrder=" + value;

    $.get({
        url: url
    }).done(function (response) {
        if (value === "amount") { $("#amountBtn").val("amount_desc"); }
        else { $("#amountBtn").val("amount"); }
        $("#resultsDiv").html(response);
    });
});