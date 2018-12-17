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


$("#dateOfBirth").change(function () {
    if (checkDate()) {
        $("#birthDateError").hide();
    }
    else {
        $("#birthDateError").show();
    }
});

$("#registerBtn").click(function (e) {
    if (!checkDate()) {
        e.preventDefault();
        $("#birthDateError").show();
    }
    else {
        $("#birthDateError").hide();
    }
});

function checkDate() {
    var isValid = true;
    var birthday = new Date($("#dateOfBirth").val());
    var ageDifMs = Date.now() - birthday.getTime();
    var ageDate = new Date(ageDifMs); // miliseconds from epoch
    var result = Math.abs(ageDate.getFullYear() - 1970);
    if (result < 18) {
        isValid = false;
    }
    return isValid;
}
