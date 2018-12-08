var modalConfirm = function (callback) {

    $("#btn-confirm").on("click", function (e) {
        e.preventDefault();
        $("#conf-modal").modal('show');
    });

    $("#modal-btn-yes").on("click", function (e) {
        e.preventDefault();
        callback(true);
        $("#conf-modal").modal('hide');
    });

    $("#modal-btn-no").on("click", function (e) {
        e.preventDefault();
        callback(false);
        $("#conf-modal").modal('hide');
    });
};

modalConfirm(function (confirm) {
    if (confirm) {
        //emailInput
        var emailInput = $("#emailInput").val();

        var token = $("input[name='__RequestVerificationToken']").val();
        var url = "/Administration/Home/Promote";
        $.post(url,
            {
                email: emailInput,
                "__RequestVerificationToken": token
            },
            function () {
                var alert = $("#successAlert");
                alert.show();
                alert.fadeOut(4000);

            }).fail(function () {
                var alert = $("#failAlert");
                alert.show();
                alert.fadeOut(6000);
            });
    }
});




$(function () {
    var getData = function (request, response) {
        $.getJSON(
            "/Administration/Home/AutoCompleteAsync/?term=" + request.term,
            function (data) {
                console.log('gyz');
                response(data.map(x => x.email));
            });
    };

    var selectItem = function (event, ui) {
        $("#emailInput").val(ui.item.value);
        return false;
    };

    $("#emailInput").autocomplete({
        source: getData,
        select: selectItem,
        minLength: 3,
        change: function () {
            $("#emailInput").val("").css("display");
        }
    });
});




//$("input#search-textbox").autocomplete({
//    source: ["c++", "java", "php", "coldfusion", "javascript", "asp", "ruby"]
//});

//$(document).ready(function () {
//    $("#emailInput").autocomplete({
//        source: function (request, response) {
//            $.ajax({
//                url: '/Administration/Home/AutoCompleteAsync',
//                type: 'GET',
//                cache: false,
//                data: request,
//                dataType: 'json',
//                success: function (data) {
//                    response($.map(data, function (item) {
//                        return {
//                            label: item,
//                            value: item + ""
//                        };
//                    }));
//                }
//            });
//        },
//        minLength: 2
//    });
//});


//$("#emailInput").on('focusout', function () {
//    $.ajax({
//        url: "/Administration/Home/AutoComplete",
//        type: "GET",
//        dataType: "json",
//        data: { fetch: $(this).val() },
//        success: function (query) {
//            $("#searchProvinceOfBirth").val(query[0]);
//        },
//    });
//});



//$("#userName").autocomplete({
//    source: function (request, response) {
//        $.ajax({
//            url: '/Administration/Home/AutoComplete',
//            type: "POST",
//            dataType: "json",
//            data: { prefix: request.term },
//            //         contentType: "application/json; charset=utf-8",
//            success: function (data) {
//                response($.map(data, function (item) {
//                    $("#userNameAuto").val(item.email);
//                    //return { label: item.Email, value: item.Email };
//                }));
//            }
//        });
//    }
//});

//$(function () {
//       $("#birds").autocomplete({
//        source: function (request, response) {
//            $.ajax({
//                url: "search.php",
//                dataType: "jsonp",
//                data: {
//                    term: request.term
//                },
//                success: function (data) {
//                    response(data);
//                }
//            });
//        },
//        minLength: 2,
//        select: function (event, ui) {
//            log("Selected: " + ui.item.value + " aka " + ui.item.id);
//        }
//    });
//});