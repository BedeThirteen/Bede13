﻿$("#modalButtton").click(function () {
    var url = "/User/GetUserCreditCardsAsync";
    $.get({
        url: url
    }).done(function (data) {
        var creditCardsDdl = $("#creditCardsDdl");
        creditCardsDdl.empty();
        data.forEach(function (currentElement) {
            var creditCardOption = $("<option />").val(currentElement.id).text(currentElement.number);
            creditCardsDdl.append(creditCardOption);
        });
    });
});

$("#addCardBtn").click(function () {
    var cardNumber = $("#cardNumber").val();
    var month = $("#expirationMonth").val();
    var year = $("#expirationYear").val();
    var cvv = $("#cvv").val();
    //var token = $("#verfToken").val();
    var url = "/User/AddCreditCardAsync";
    $.post(url,
        {
            cardNumber: cardNumber,
            month: month,
            year: year,
            cvv: cvv
            //__RequestVerificationToken: token
        },
        function (data) {
            var creditCardsDdl = $("#creditCardsDdl");
            var creditCardOption = $("<option />").val(data.id).text(data.number);
            creditCardsDdl.append(creditCardOption);
            creditCardsDdl.val(creditCardOption.val());
        });
});


$("#depositBtn").click(function () {
    var cardId = $("#creditCardsDdl").val();
    var amount = $("#depositAmount").val();

    //var token = $("#verfToken").val();
    var url = "/User/DepositAmountAsync";
    $.post(url,
        {
            cardId: cardId,
            amount: amount
            //__RequestVerificationToken: token
        },
        function (data) {
            var balanceValue = $("#balanceValue");
            balanceValue.text(data.result);
            $("#closeBtn").click();
        });
});