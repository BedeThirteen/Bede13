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