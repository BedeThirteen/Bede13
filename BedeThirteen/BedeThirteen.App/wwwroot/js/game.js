$("#gameStakeForm").submit(function (event) {
    event.preventDefault(); //prevent default action 
    var post_url = $(this).attr("action"); //get form action url
    var request_method = $(this).attr("method"); //get form GET/POST method
    var form_data = $(this).serialize(); //Encode form elements for submission

    $.ajax({
        url: post_url,
        type: request_method,
        data: form_data
    }).done(function (data) {
        $("#gameBetButton").prop("disabled", true);
        RunSlotMachine(0, 16, UpdateSiteData, data);
      
    });
});

function UpdateSiteData(data) {
    $("#gameBetButton").prop("disabled", false);

    UpdateBalance(data.newBalance, data.currencyName);
    UpdateWinnings(data.gameWinnings, data.currencyName);
    UpdateSlotsImages(data.rolledValues);

    AddToGameLog(data.logHistory);
    FlashWinningLines(data.winningLines)


}

function UpdateBalance(number, currency) {
    let amountAndCurrency = `${number} ${currency}`;
    // Updates Visualized balance
    $("#gameBalanceAccount").text(amountAndCurrency);
    $("#balanceValue").text(amountAndCurrency);

    //Sets form's maximum betable amount
    $("#gameStakeForm > input").first("input").attr("max", number);
}


$(function () {

    $("#gameBalanceAccount").text($("#balanceValue").text());

    $("#gameStakeForm > input").first("input").attr("max", $("#balanceValue").text().split(" ")[0]);
})

function AddToGameLog(rolledValues) {

    let log = $("#gameBetHistory");


    rolledValues.forEach(function (msg) {
        $("#gameBetHistory").append(`<p class="text-dark">${msg}</p>`);
    });

    let numberOfLogs = $(log).children().length;

    if (numberOfLogs > 5) {
        for (var i = 0; i < numberOfLogs - 5; i++) {
            $("#gameBetHistory").children().first("label").remove();
        }
    }

}

function UpdateSlotsImages(values) {

    var tokenToImage = { 0: "wildcard", 1: "apple", 2: "watermelon", 3: "seven" }


    let rols = $("#gameSlotsValues").children();
    for (let y = 0; y < rols.length; y++) {
        let foo = $(rols[y]);
        let line = foo.children();
        for (let x = 0; x < line.length; x++) {
            let foo = $(line[x]).children();
            let s = foo.first("a");

            s.attr("src", `/images/slot/${tokenToImage[values[y][x].type]}.png`);
        }
    };
}



function UpdateWinnings(amount,currency) {
    $("#gameWinnings").text(`${amount} ${currency}`);
}
function RunSlotMachine(currentSteps, stepsToGo, functionOnEnd, data) {
    if (currentSteps < stepsToGo) {


        setTimeout(function () {

            // recalls the parent function to
            // create a recursive loop.
            RandomizeSlots();
            RunSlotMachine(currentSteps + 1, stepsToGo, functionOnEnd, data);

        }, currentSteps * currentSteps);

    }
    else {
        functionOnEnd(data);
    }
}
function RandomizeSlots() {

    var tokenToImage = { 0: "wildcard", 1: "apple", 2: "watermelon", 3: "seven" }

    let rols = $("#gameSlotsValues").children();
    for (let y = 0; y < rols.length; y++) { 

        let line = $(rols[y]).children();
        for (let x = 0; x < line.length; x++) {
             
            let s = $(line[x]).children().first("a");
            s.attr("src", `/images/slot/${tokenToImage[Math.floor((Math.random() * 4))]}.png`);
        }
    }
}

function FlashWinningLines(winningRows) {

    let rows = $("#gameSlotsValues").children();
    for (var i = 0; i < winningRows.length; i++) {
        let el = $(rows[winningRows[i]]);
        el.css("background-color", "green");
        setTimeout(function () {
           
            el.css("background-color", "white");

        }, 2000);
       
    }
}

 
