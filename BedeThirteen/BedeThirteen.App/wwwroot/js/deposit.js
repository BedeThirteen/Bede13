$("#modalButtton").click(function () {
    var url = "/User/GetUserCreditCardsAsync";
    $.get({
        url: url
    }).done(function (data) {
        var creditCardsDdl = $("#creditCardsDdl");
        var cardsWithdrawDdl = $("#cardsWithdrawDdl");
        creditCardsDdl.empty();
        cardsWithdrawDdl.empty();
        data.forEach(function (currentElement) {
            creditCardsDdl.append($("<option />").val(currentElement.id).text(currentElement.number));
            cardsWithdrawDdl.append($("<option />").val(currentElement.id).text(currentElement.number));
        });
    });
});

$("#addCardBtn").click(function (e) {
    e.preventDefault();
    var cardNumber = $("#cardNumber").val();
    var month = $("#expirationMonth").val();
    var year = $("#expirationYear").val();
    var cvv = $("#cvv").val();
    var token = $("input[name='__RequestVerificationToken']").val();
    var url = "/User/AddCreditCardAsync";
    $.post(url,
        {
            cardNumber: cardNumber,
            month: month,
            year: year,
            cvv: cvv,
            "__RequestVerificationToken": token
        },
        function (data) {
            var creditCardsDdl = $("#creditCardsDdl");
            var cardsWithdrawDdl = $("#cardsWithdrawDdl");
            creditCardsDdl.append($("<option />").val(data.id).text(data.number));
            cardsWithdrawDdl.append($("<option />").val(data.id).text(data.number));
            creditCardsDdl.val(data.id);
        });
});


$("#depositBtn").click(function (e) {
    e.preventDefault();
    var cardId = $("#creditCardsDdl").val();
    var amount = $("#depositAmount").val();
    var token = $("input[name='__RequestVerificationToken']").val();
    var url = "/User/DepositAmountAsync";
    $.post(url,
        {
            cardId: cardId,
            amount: amount,
            "__RequestVerificationToken": token
        },
        function (data) {
            var balanceValue = $("#balanceValue");
            balanceValue.text(data.result);
            let gameBalanceElement = $(gameBalanceAccount);
            if (gameBalanceElement) {
                gameBalanceElement.text(data.result);
            }
            $("#closeBtn").click();
        });
});

$("#withdrawBtn").click(function (e) {
    e.preventDefault();
    var amount = $("#withdrawAmount").val();
    var cardId = $("#cardsWithdrawDdl").val();
    var token = $("input[name='__RequestVerificationToken']").val();
    var url = "/User/WithdrawAmountAsync";
    $.post(url,
        {
            cardId: cardId,
            amount: amount,
            "__RequestVerificationToken": token
        },
        function (data) {
            var balanceValue = $("#balanceValue");
            balanceValue.text(data.result);
            $("#closeBtn").click();
        });
});