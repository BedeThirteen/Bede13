$("#filterByDdl").change(function () {
    $(".criteria").hide();
    $("#" + $(this).val()).show();
    if ($(this).val() === "date" || $(this).val() === "amount") {
        $("#" + $(this).val() + "A").show();
    }
});

$("#archiveRangeModal").on("hidden.bs.modal", function () {
    $("#archivedOptionLabel").click();

});

$("#archivedOptionLabel").click(function () {

    $("#archivedOptionLabel").addClass('active');
    $("#transactionOptionLabel").removeClass('active');
    $("#addToArchiveOptionLabel").removeClass('active');

    performCleanSearch();
});


$("#transactionOptionLabel").click(function () {

    $("#archivedOptionLabel").removeClass('active');
    $("#addToArchiveOptionLabel").removeClass('active');
    $("#transactionOptionLabel").addClass('active');
    performCleanSearch();
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
        var isValidAmount = validateAmounts();

        if (isValidAmount === true) {
            getSearchResult(pageN);
        }
    }
    else if (filterBy === "date") {
        var isValidDate = validateDates();

        if (isValidDate === true) {
            getSearchResult(pageN);
        }
    }
    else {
        getSearchResult(pageN);
    }
});

function validateDates() {
    var isValid = true;
    var value = $("#date").val();
    if (value.length === 0) {
        isValid = false;
        $("#date").popover();
        $("#date").popover('show');
        setTimeout(function () {
            $("#date").popover('hide');
        }, 2000);
    }
    var valueA = $("#dateA").val();
    if (valueA.length === 0) {
        isValid = false;
        $("#dateA").popover();
        $("#dateA").popover('show');
        setTimeout(function () {
            $("#dateA").popover('hide');
        }, 2000);
    }
    return isValid;
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


function performCleanSearch() {
    $("#filterByDdl").val("all");
    $("#sortByDdl").val("date_desc");
    $("#pageSizeDdl").val(10);
    getSearchResult(0);
}

function getSearchResult(pageNum) {
    var filterBy = $("#filterByDdl").val();
    var filterCriteria = $("#" + $("#filterByDdl").val()).val();
    var pageSize = $("#pageSizeDdl").val();
    var aditionalCriteria = "";
    var sortBy = $("#sortByDdl").val();
    var archiveKey = 0;
    //if ($('#archivedOptionInput').is(':checked')) {
    if ($("#archivedOptionLabel").is(".active")) {

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
