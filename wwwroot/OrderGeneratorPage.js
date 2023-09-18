let maxCountInPage = 10;
let countOpen = 0;
let countClose = 0;

let thisPage = 0;
let maxPage = 0;

let maxCountButton = 4;

let skip = 0;

let first = true;
let remainder = 0;
let tableNameId = "table";

async function newLoadPage(){
    location. reload();
}

async function loadPage() {
    $(".mainTableRow").empty();

    if (first) {
        countOpen = await countProuction(1);
        countClose = await countProuction(0);

        remainder = maxCountInPage - countOpen;

        if (countOpen + countClose > maxCountInPage) {
            maxPage = Math.ceil(countClose / maxCountInPage);
        }

        if (remainder <= 0) {
            remainder = 0;
            maxPage++;
        }
        first = false;
    }
    console.log(remainder);
    skip = ((thisPage-1) * maxCountInPage)+remainder;

    if (thisPage == 0) {
        await generatorTable(await getListOrder(1, 0, countOpen), tableNameId);
        await generatorTable(await getListOrder(0, 0, remainder), tableNameId);
    }
    else {
        //remainder = maxCountInPage; 
        //skip = (thisPage * maxCountInPage)-countOpen;
        await generatorTable(await getListOrder(0, skip, maxCountInPage), tableNameId);
    }

    generatorPageCounter();
}

function generatorPageCounter() {
    $('#pageBar').empty()
    if (thisPage == 0) {
        $('#pageBar').append('<input class="buttonNextActiv" type="button" disabled="disabled" value="&lt;&lt;"></input>');
    }
    else {
        $('#pageBar').append('<input class="buttonNext" type="button" onclick="toPage(0)" value="&lt;&lt;"></input>');
    }
    $('#pageBar').append('<input class="buttonNextActiv" type="button" disabled="disabled" value="' + (thisPage + 1) + '">');

    let countButton = 0;
    if (maxPage <= thisPage + maxCountButton) {
        countButton = maxPage-1;
    }
    else {
        countButton = maxCountButton+thisPage;
    }
    for (let i = thisPage + 1; i <= countButton; i++) {
        $('#pageBar').append('<input class="buttonNext" type="button" onclick="toPage(' + i + ')" value="' + (i + 1) + '"></input>');
    }

    if (thisPage < maxPage-maxCountButton) {
        $('#pageBar').append('<input class="buttonNext" type="button" onclick="toPage(' + (thisPage + maxCountButton) + ')" value=">>"></input>');
    }

}

function toPage(page) {
    thisPage = page;
    loadPage();
}

async function countProuction(status) {
    let respone1 = await fetch('/api/order/get-count-order?statusOrder=' + status, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
    });
    responseOrder = await respone1.json();
    if (responseOrder.status != 0) {
        alert("не получилось получить Product!" + responseOrder.status.errorMassage)
    }
    return responseOrder.result;
}

async function editStatus(orderId, newStatus) {
    let respone1 = await fetch('/api/order/edit-status-order' + orderId + "?status=" + newStatus, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
    });
    responseOrder = await respone1.json();
    if (responseOrder.status != 0) {
        alert("не получилось изменить статус!" + responseOrder.status.errorMassage)
    }

    return responseOrder.result;
}

let arrayOrder;
async function getListOrder(status, skip, take) {
    let respone1 = await fetch('https://localhost:7181/api/order/get-list-open-order?statusOrder=' + status + '&skip=' + skip + '&take=' + take, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify()
    });
    responseOrder = await respone1.json();
    if (responseOrder.status != 0) {
        alert("не получилось получить список заказов!" + responseOrder.status.errorMassage)
    }
    arrayOrder = responseOrder.result.$values;
    return responseOrder.result.$values;
}

async function getOrderForId(id) {
    let respone1 = await fetch('/api/order/get-order' + id, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
    });
    responseOrder = await respone1.json();

    if (responseOrder.status != 0) {
        alert("не получилось получить получить заказ по id!" + responseOrder.status.errorMassage)
    }

    arrayOrder.push(responseOrder.result);

    let lengthArray = arrayOrder.length;

    for (let i = 0; i < lengthArray - 1; i++) {
        arrayOrder.shift();
    }
    return arrayOrder;
}

async function getOrderForData(data) {
    let respone1 = await fetch('/api/order/get-order-data?data=' + data, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
    });
    responseOrder = await respone1.json();
    if (responseOrder.status != 0) {
        alert("не получилось получить заказы по дате!" + responseOrder.status.errorMassage)
    }
    return responseOrder.result.$values;
}