$(function () {
    var url = "/Account/PopulateCurrenciesDdlOnRegister";
    $.get({
        url: url
    }).done(function (data) {
        var currencyDropDown = $("#register-currencies");
        currencyDropDown.empty();
        data.forEach(function (currentElement) {
            var currencieOption = $("<option />").val(currentElement.id).text(currentElement.name);
            currencyDropDown.append(currencieOption);
        });
    });
});