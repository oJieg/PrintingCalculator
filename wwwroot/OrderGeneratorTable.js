let responseOrder;
let totalPrice = 0;


async function generatorTable(inResponseOrder, tableNameId) {
    console.log(inResponseOrder);
    responseOrder = inResponseOrder;

    for (let i = 0; i < responseOrder.length; i++) {
        $('#'+tableNameId+' tr:last').after('<tr class="mainTableRow" onclick="clicTable(' + responseOrder[i].id + ')">' + addData(i, addTotalPrice(addContact(i, await addProduct(i, addStatus(i, addId(i)))))) + '</tr>');
    }
}


function addId(i) {
    return '<td><div class="textTableHistory">' + responseOrder[i].id + '</div></td>';
}

function addStatus(i, text) {
    let status;
    if (responseOrder[i].status == 0) {
        status = '<p style="background-color: rgb(162, 243, 169);">Закрыто</p>';
    }
    else if (responseOrder[i].status == 1) {
        status = '<p style="background-color: red;">Открыто</p>';
    }

    return text + '<td><div class="textTableHistory">' + status + '<p class="boxText"><button onclick="closeOrder(' + responseOrder[i].id + ')" class="buttonStatus">Закрыть </button></p><p class="boxText"><button onclick="openOrder(' + responseOrder[i].id + ')" class="buttonStatus">Открыть</button></p></div></td>';
}

async function addProduct(i, text) {
    text += '<td>';
    let countProuction = responseOrder[i].products.$values.length;
    if (countProuction > 0) {
        let historysLength = responseOrder[i].products.$values[0].histories.$values.length;


        let actualHistory = responseOrder[i].products.$values[0].activecalculationHistoryId;


        if (historysLength > 0) {
            let history = await getSimolHistory(actualHistory);

            text += '<div class="textTableHistory">' + generatorBoxProduct(history, responseOrder[i].products.$values[0].id) + '</div>';
        }
        else {
            text += '<div class="textTableHistory"><div class="boxProduct" onclick="productionClick(' + responseOrder[i].products.$values[0].id + ')""> </div> </div>';

        }
    }

    if (countProuction > 1) {
        text += '<details class="detal" onclick="detal()"> <summary>Больше</summary>';
        for (let j = 1; j < countProuction; j++) {
            let historysLength = responseOrder[i].products.$values[j].histories.$values.length;
            let actualHistoryJ = responseOrder[i].products.$values[j].activecalculationHistoryId;

            if (historysLength > 0) {

                let history = await getSimolHistory(actualHistoryJ);

                text += '<div class="textTableHistory">' + generatorBoxProduct(history, responseOrder[i].products.$values[j].id) + '</div>';
            }
            else {
                text += '<div class="textTableHistory"><div class="boxProduct" onclick="productionClick(' + responseOrder[i].products.$values[j].id + ')"> </div> </div>';

            }
        }
        text += '</details>';
    }
    text += '</td>'
    return text;
}


function generatorBoxProduct(history, productionId) {
    totalPrice += history.price;
    return '<div class="boxProduct" onclick="productionClick(' + productionId + ')">' +
        '<p class="boxText">' + history.height + 'x' + history.whidth + '|' + history.amount + 'x' + history.kinds + 'шт' + '</p>' +
        '<p class="boxText">' + history.paperName + ' </p>' +
        '<p class="boxText">' + generatorLogo(history) + '</p>' +
        '<p class="boxText">' + history.price + 'руб' + '</p>' +
        '</div>';
}

function addContact(i, text) {
    text += '<td>';
    for (let j = 0; j < responseOrder[i].contacts.$values.length; j++) {
        text += '<div class="textTableHistory" onclick="contactClick(' + responseOrder[i].contacts.$values[j].id + ')">' +
            '<div class="boxProduct">' +
            '<p class="boxText">' + responseOrder[i].contacts.$values[j].name + '</p>';
        if (responseOrder[i].contacts.$values[j].mails.$values.length > 0) {
            text += '<p class="boxText">mail:' + responseOrder[i].contacts.$values[j].mails.$values[0].email + '</p>';
        }
        if (responseOrder[i].contacts.$values[j].phoneNmbers.$values.length > 0) {
            text += '<p class="boxText">тел:' + responseOrder[i].contacts.$values[j].phoneNmbers.$values[0].number + '</p>';
        }

        text += '</div>' +
            '</div>'
    }
    text += '</td>'
    return text;
}

function addTotalPrice(text) {
    text += '<td>' +
        '<div class="textTableHistory">' + totalPrice + 'руб.</div>' +
        '</td>';
    totalPrice = 0;
    return text;
}

function addData(i, text) {
    var msUTC = Date.parse(responseOrder[i].dateTime);

    text += '<td>' +
        '<div class="textTableHistory">' + new Date(msUTC).toLocaleString() + '</div>' +
        '</td>';
    return text;
}

function generatorLogo(history) {
    let PosLogo = "";
    if (history.lamination) {
        PosLogo += "<img class='imagPos' src='/lamination.svg'/>";
    }
    if (history.creasing) {
        PosLogo += "<img class='imagPos' src='/creesing.svg'/>";
    }
    if (history.drilling) {
        PosLogo += "<img class='imagPos' src='/driling.svg'/>";
    }
    if (history.rounding) {
        PosLogo += "<img class='imagPos' src='/rolling.svg'/>";
    }
    if (history.springBrochure) {
        PosLogo += "<img class='imagPos' src='/SpringBrochure.svg'/>";
    }
    return PosLogo
}

async function getSimolHistory(id) {
    let respone = await fetch('/api/get-simpl-result' + id, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" }
    });

    let awner = await respone.json();

    if (awner.status.status != 0) {
        alert("не получилось получить SimplHistory222!" + awner.status.errorMassage)
    }

    return awner.simplResult;
}


/*------------------------------*/

async function detalProduction(idProduction) {
    $(".productionTableRow").remove();
    let production = await getProduction(idProduction);
    console.log(production);

    addDetalProduction(production);
    if (production.histories.$values.length > 0) {
        $('#productionTable tr:last').after('<tr class="productionTableRow" id="' + 'production' + production.histories.$values[0].id + '" onclick="result(' + production.histories.$values[0].id + ');">' +
            await addProductionTable(production.histories.$values[0].id) + '</tr>');

        for (let i = 1; i < production.histories.$values.length; i++) {
            $('#productionTable tr:last').after('<tr class="productionTableRow" id="' + 'production' + production.histories.$values[i].id + '" onclick="result(' + production.histories.$values[i].id + ');">' +
                await addProductionTable(production.histories.$values[i].id) + '</tr>');
        }
    }

    $('#production' + production.activecalculationHistoryId).addClass("actualHistoru")

}

async function getProduction(idProduction) {
    let respone = await fetch('/api/product/get-product' + idProduction, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" }
    });

    let awner = await respone.json();
    if (awner.status != 0) {
        alert("не получилось получить product!" + awner.status.errorMassage)
    }

    return awner.result;
}

function addDetalProduction(production) {
    $('#detalProductionName').html(production.name);
    $('#detalProductionDetails').html(production.description);
}

async function addProductionTable(historyId) {
    let simplResult = await getSimolHistory(historyId);

    text = '<td>' +
        '<div class="textTableHistory">' + simplResult.historyId + '</div>' +
        '</td>' +
        '<td>' +
        '<div class="textTableHistory">' + new Date(simplResult.dateTime).toLocaleString() + '</div>' +
        '</td>' +
        '<td>' +
        '<div class="textTableHistory">' + simplResult.height + 'x' + simplResult.whidth + '</div>' +
        '</td>' +
        '<td>' +
        '<div class="textTableHistory">' + simplResult.amount + 'x' + simplResult.kinds + '</div>' +
        '</td>' +
        '<td>' +
        '<div class="textTableHistory">' + simplResult.paperName + '</div>' +
        '</td>' +
        '<td>' +
        '<div>' +
        generatorLogo(simplResult) +
        '</div>' +
        '</td>' +
        '<td>' +
        '<div class="textTableHistory">' + simplResult.price + '</div>' +
        '</td>' +
        '</tr>';
    return text;
}

/*---------------------/*/