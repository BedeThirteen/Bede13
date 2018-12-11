$("#filterByDdl").change(function () {
    $(".criteria").hide();
    $("#" + $(this).val()).show();
    if ($(this).val() === "date" || $(this).val() === "amount") {
        $("#" + $(this).val() + "A").show();
    }
});

$("#archiveRangeModal").on("hidden.bs.modal", function () {
    //$("#archivedOptionInput").prop("checked", true);
    //   $("#").addClass('className');
    $("#archivedOptionLabel").click();
});

$("#archivedOptionLabel").click(function () {

    $("#filterByDdl").val("all");
    $("#sortByDdl").val("date_desc");
    $("#pageSizeDdl").val(10);

    $("#searchBtn").click();


});


$("#transactionOptionLabel").click(function () {

    $("#filterByDdl").val("all");
    $("#sortByDdl").val("date_desc");
    $("#pageSizeDdl").val(10);

    $("#searchBtn").click();
});


$("#addToArchiveBtn").click(function () {
    var from = $("#archiveFrom").val();
    var to = $("#archiveTo").val();
    var token = $("input[name='__RequestVerificationToken']").val();

    var url = "/Transaction/AddToArchiveAsync";
    $.post(url,
        {
            from: from,
            to: to,
            "__RequestVerificationToken": token
        },
        function (data) {
            var alert = $("#successAlert");
            $("#successMsg").text("Archived " + data.numberOfRecords + " transactions!");
            alert.show();
            alert.fadeOut(4000);

        }).fail(function () {
            var alert = $("#failAlert");
            $("#failMsg").text("Unsuccessful archivation!");
            alert.show();
            alert.fadeOut(6000);
        });
});

$("#searchBtn").click(function () {
    var pageN = 0;
    var filterBy = $("#filterByDdl").val();
    if (filterBy === "amount") {
        var isValid = validateAmounts();

        if (isValid === true) {
            getSearchResult(pageN);
        }
    }
    else {
        getSearchResult(pageN);
    }
});

function performCleanSearch() {
    $("#filterByDdl").val("all");
    $("#sortByDdl").val("date_desc");
    $("#pageSizeDdl").val(10);
    getSearchResult(0);
}

function validateAmounts() {
    var isValid = true;
    var value = $("#amount").val();
    if (!/^\+?\d+$/.test(value)) {
        isValid = false;
        $("#amount").popover();
        $("#amount").popover('show');
        setTimeout(function () {
            $("#amount").popover('hide');
        }, 2000);
    }
    var valueA = $("#amountA").val();
    if (!/^\+?\d+$/.test(valueA)) {
        isValid = false;
        $("#amountA").popover();
        $("#amountA").popover('show');
        setTimeout(function () {
            $("#amountA").popover('hide');
        }, 2000);
    }
    return isValid;
}

$(document).on("click", "#pageNumberBtn", function () {
    var pageN = $(this).val();
    getSearchResult(pageN);
});




function getSearchResult(pageNum) {
    var filterBy = $("#filterByDdl").val();
    var filterCriteria = $("#" + $("#filterByDdl").val()).val();
    var pageSize = $("#pageSizeDdl").val();
    var aditionalCriteria = "";
    var sortBy = $("#sortByDdl").val();
    var archiveKey = 0;
    if ($('#archivedOptionInput').is(':checked')) {
        archiveKey = 1;
    }


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
            sortBy: sortBy,
            archiveKey: archiveKey
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
            $("#user").css("display");
        }
    });
});
