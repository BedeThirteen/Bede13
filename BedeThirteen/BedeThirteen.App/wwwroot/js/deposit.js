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
            creditCardsDdl.append($("<option />").val(currentElement.id).text(currentElement.number).addClass("dropdown-item"));
            cardsWithdrawDdl.append($("<option />").val(currentElement.id).text(currentElement.number).addClass("dropdown-item"));
        });
    });
});

$("#addCardBtn").click(function (e) {
    e.preventDefault();
    if (validateDate()) {
        $("#dateError").hide();
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
                $("#expandDepositAnchor").click();
            });
    }
    else { $("#dateError").show(); }
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

            var gameBalanceElement = $("#gameBalanceAccount");
            if (gameBalanceElement.length) {
                gameBalanceElement.text(data.result);
            }

            var betInputField = $("#gameStakeForm > input");
            if (betInputField.length) {
                //Sets form's maximum betable amount
                $("#gameStakeForm > input").first("input").attr("max", data.result);
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

            var gameBalanceElement = $("#gameBalanceAccount");
            if (gameBalanceElement.length) {
                gameBalanceElement.text(data.result);
            }


            var betInputField = $("#gameStakeForm > input");
            if (betInputField.length) {
                //Sets form's maximum betable amount
                $("#gameStakeForm > input").first("input").attr("max", data.result);
            }
            $("#closeBtn").click();
        });
});

$("#expirationYear").change(function () {
    if (validateDate()) { $("#dateError").hide(); }
    else { $("#dateError").show(); }
});

$("#expirationMonth").change(function () {
    if (validateDate()) { $("#dateError").hide(); }
    else { $("#dateError").show(); }
});

function validateDate() {
    var date = new Date();
    if (Number($("#expirationYear").val()) === date.getFullYear()
        && Number($("#expirationMonth").val()) <= date.getMonth() + 1) {
        return false;
    }
    else { return true; }
}
 
$("#addCreditCardForm").validate({
    rules: {
        cardNumber: {
            required: true,
            creditcard: true           
        },
        cvv: {
            required: true,
            digits: true,
            minlength: 4,
            maxlength: 4
        }
    }
});
