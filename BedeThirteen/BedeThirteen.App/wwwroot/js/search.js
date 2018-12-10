$("#filterByDdl").change(function () {
    $(".criteria").hide();
    $("#" + $(this).val()).show();
    if ($(this).val() === "date" || $(this).val() === "amount") {
        $("#" + $(this).val() + "A").show();
    }
});







$("#searchBtn").click(function () {
    var pageN = 0;
    var isValid = true;
    var value = $("#amount").val();
    if (!$.isNumeric(value)) {
        isValid = false;
        $("#amount").popover();
        $("#amount").popover('show');
        setTimeout(function () {
            $("#amount").popover('hide');
        }, 2000);

    }

    var valueA = $("#amountA").val();
    if (!$.isNumeric(valueA)) {
        isValid = false;
        $("#amountA").popover();
        $("#amountA").popover('show');
        setTimeout(function () {
            $("#amountA").popover('hide');
        }, 2000);

    }

    if (isValid === true) {
        GetSearchResult(pageN);
    }
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

    if (filterBy === "date") {
        aditionalCriteria = $("#dateA").val();
        if (filterCriteria === '') {
            filterCriteria = new Date(2000, 1, 1);
        }
        if (aditionalCriteria === '') {
            aditionalCriteria = new Date();
        }
    }
    else if (filterBy === "amount") {
        aditionalCriteria = $("#amountA").val();
        if (filterCriteria === '') {
            filterCriteria = 0;
        }
        if (aditionalCriteria === '') {
            aditionalCriteria = 100000;
        }
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


$(function () {
    var getData = function (request, response) {
        $.getJSON(
            "/Administration/Home/AutoCompleteAsync/?term=" + request.term,
            function (data) {
                response(data.map(x => x.email));
            });
    };

    var selectItem = function (event, ui) {
        $("#user").val(ui.item.value);
        return false;
    };

    $("#user").autocomplete({
        source: getData,
        select: selectItem,
        minLength: 3,
        change: function () {
            $("#user").val("").css("display");
        }
    });
});
