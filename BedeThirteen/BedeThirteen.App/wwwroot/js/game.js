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
        UpdateBalance(data.newBalance);
        UpdateSlots(data.rolledValues);
        AddToGameLog(data.logHistory);

        // Add result to bet history       
    });
});

function UpdateBalance(number) {
    $("#gameBalanceAccount").text(number);
    $("#balanceValue").text(number);
}


$(function () {
    $("#gameBalanceAccount").text($("#balanceValue").text());
});

function AddToGameLog(rolledValues) {

    let log = $("#gameBetHistory");
    

    rolledValues.forEach(function (msg) {
        $("#gameBetHistory").append(`<label>${msg}</label>`);
    });

    let numberOfLogs = $(log).children().length;

    if (numberOfLogs > 3) { 
        for (var i = 0; i < numberOfLogs - 3; i++) {
            $("#gameBetHistory").children().first("label").remove(); 
        }
    }
     
}

function UpdateSlots(values) {


     
    let rols = $("#gameSlotsValues").children();
    for (let y = 0; y < rols.length; y++) {
        let foo = $(rols[y]);
        let line = foo.children();
        for (let x = 0; x < line.length; x++) {

            line.text(values[y][x].type);
        }
    };
}

