
let canvasMoneyInSystemChart = document.getElementById('adminMoneyInSystemChart');
let ctxMoneyInSystem = canvasMoneyInSystemChart.getContext('2d');

let withdraw = canvasMoneyInSystemChart.getAttribute('data-withdraw');
let deposit = canvasMoneyInSystemChart.getAttribute('data-deposit');

var chartMoneyInSystem = new Chart(ctxMoneyInSystem, {




    // The type of chart we want to create
    type: 'bar',

    // The data for our dataset
    data: {
        labels: ["Deposited", "Withdrawn", "Difference"],
        datasets: [{

            backgroundColor: ['rgb(0, 255, 0)', 'rgb(255, 0, 0)', 'rgb(255, 255, 0)'],
            borderColor: 'rgb(255, 99, 132)',
            data: [Math.round(deposit), Math.round( -withdraw), Math.round( deposit - withdraw)],
        }]
    },

    // Configuration options go here
    options: {
        legend: {
            display: false
        },
        title: {

            display: true,
            text: 'Amount In System (USD)'
        }
    }
});

let canvasMoneyPlayedInSystem = document.getElementById('adminMoneyPlayedInSystemChart');
let ctxMoneyPlayedInSystem = canvasMoneyPlayedInSystem.getContext('2d');

let stake = canvasMoneyPlayedInSystem.getAttribute('data-stake');
let win = canvasMoneyPlayedInSystem.getAttribute('data-win');


var chartMoneyPlayedInSystem = new Chart(ctxMoneyPlayedInSystem, {




    // The type of chart we want to create
    type: 'bar',

    // The data for our dataset
    data: {
        labels: ["Staked", "Won", "Difference"],
        datasets: [{

            backgroundColor: ['rgb(00, 255, 0)', 'rgb(255, 0, 0)', 'rgb(255, 255, 0)'],
            borderColor: 'rgb(255, 99, 132)',
            data: [Math.round(stake), Math.round(-win), Math.round(stake - win)],
        }]
    },

    // Configuration options go here
    options: {
        legend: {
            display: false
        },
        title: {

            display: true,
            text: 'Amounts Played (USD)'
        }
    }
});
