const input = document.getElementById("newAnount");
const outLine = document.getElementById("test");

async function GetNewAmount(historyId) {
    let inputValut = input.value;
    if (inputValut > 0) {
        let result = await GetResult(historyId, inputValut);
        outLine.append(result);
    }
}
async function GetNewConstAmount(historyId, newAmount) {
    let inputValut = newAmount;
    if (inputValut > 0) {
        let result = await GetResult(historyId, inputValut);
        outLine.append(result);
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