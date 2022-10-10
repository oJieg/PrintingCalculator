const input = document.getElementById("newAnount");
const table = document.getElementById("table");

async function GetNewAmount(historyId) {
    let inputValut = input.value;
    if (inputValut > 0) {
        let result = await GetResult(historyId, inputValut);
        CreateRow(inputValut, result);
    }
}
async function GetNewConstAmount(historyId, newAmount) {
    let inputValut = newAmount;
    if (inputValut > 0) {
        let result = await GetResult(historyId, inputValut);
        CreateRow(newAmount, result);
    }
}

async function GetResult(historyId, amount) {
    const response = await fetch("/api/VerySimpleCalculation/" + historyId + "/" + amount, {
        method: "GET",
        headers: { "Accept": "application/json" }
    })
    let test = await response.json();
    return test;
}

function CreateRow(amout, result){
    let newRow = table.insertRow(0);
    let newCell = newRow.insertCell(0);
    let newCell2 = newRow.insertCell(1);
    newCell.innerHTML = amout +" шт.";
    newCell2.innerHTML = result+" руб.";
    newCell.setAttribute("class", "textTableHistory");
    newCell2.setAttribute("class", "textTableHistory");
}