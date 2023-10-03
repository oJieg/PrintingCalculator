let responseOrder;
let totalPrice = 0;


async function generatorTable(inResponseOrder, tableNameId) {
    console.log(inResponseOrder);
    responseOrder = inResponseOrder;

    for (let i = 0; i < responseOrder.length; i++) {
        console.log($('#' + tableNameId));

        let text = addId(i) + addStatus(i) + addContact(i) + await addProduct(i) + addTotalPrice(i) + addData(i);
        $('#' + tableNameId + ' tr[name="mainTableRow"]:last').after('<tr name ="mainTableRow" class="mainTableRow" id="mainRow'  + responseOrder[i].id + '" onclick="clicTable(' + responseOrder[i].id + ')">' +
            text + '</tr>');
        backgroundColor(i);
    }
    
}

function backgroundColor(i) {
   // console.log(responseOrder[i].status);
    if (responseOrder[i].status == 0) {
        $('#mainRow' + responseOrder[i].id).attr('style', 'background-color: #eedeeb;');
       // status = '<p style="background-color: #e390d6;width: 238px;">На утв-е</p>';
    }
    else if (responseOrder[i].status == 1) {
        $('#mainRow' + responseOrder[i].id).attr('style', 'background-color: #ddb5b5;');
       // status = '<p style="background-color: #eb6e6e;width: 238px;">В работе</p>';
    }
    else if (responseOrder[i].status == 2) {
        $('#mainRow' + responseOrder[i].id).attr('style', 'background-color: #9dbdcc;');
       // status = '<p style="background-color: #3e697d;width: 238px;">Отмененый</p>';
    }
    else if (responseOrder[i].status == 3) {
        $('#mainRow' + responseOrder[i].id).attr('style', 'background-color: #cde3ce;');
      //  status = '<p style="background-color: #9ee19f;width: 238px;">Выполнено</p>';
    }
    else if (responseOrder[i].status == 4) {
        $('#mainRow' + responseOrder[i].id).attr('style', 'background-color: #e2e3cd;');
        //  status = '<p style="background-color: #9ee19f;width: 238px;">Выполнено</p>';
    }
}

function addId(i) {
    return '<td><div class="textTableHistory">' + responseOrder[i].id + '</div></td>';
}

function addStatus(i) {
    let status;
    if (responseOrder[i].status == 0) {
        status = '<p style="background-color: #e390d6;width: 238px;">На утв</p>';
    }
    else if (responseOrder[i].status == 1) {
        status = '<p style="background-color: #eb6e6e;width: 238px;">В работе</p>';
    }
    else if (responseOrder[i].status == 2) {
        status = '<p style="background-color: #3e697d;width: 238px;">Отмененый</p>';
    }
    else if (responseOrder[i].status == 3) {
        status = '<p style="background-color: #9ee19f;width: 238px;">Выполнено</p>';
    }
    else if (responseOrder[i].status == 4) {
        status = '<p style="background-color: #ccce7e;width: 238px;">Не отгруженно</p>';
    }

    

    return  '<td style="width: 250px;"><div class="textTableHistory">' + status +
        '<div class="horizontalInput">'+
        '<p class="boxText">'+ '<button onclick="statusOrder(' + responseOrder[i].id + ', 0)" class="buttonStatus">На утв </button></p>' +
            '<p class="boxText">'+ '<button onclick="statusOrder(' + responseOrder[i].id + ', 1)" class="buttonStatus">В работу </button></p>' +
           ' </div>'+
        '<div class="horizontalInput">'+
        '<p class="boxText">' + '<button onclick="statusOrder(' + responseOrder[i].id + ', 3)" class="buttonStatus">Готово</button></p>' +
        '<p class="boxText">' + '<button onclick="statusOrder(' + responseOrder[i].id + ', 4)" class="buttonStatus">Не отгруженно </button></p>' +
        '</div>'+
    '</p></div></td>';
}


async function addProduct(i) {
    let text = '<td style="width: 500px;">';
    let countProuction = responseOrder[i].products.$values.length;
    if (countProuction > 0) {
        let historysLength = responseOrder[i].products.$values[0].histories.$values.length;


        let actualHistory = responseOrder[i].products.$values[0].activecalculationHistoryId;


        if (historysLength > 0) {
            let history = await getSimolHistory(actualHistory);
            let name = responseOrder[i].products.$values[0].name;
            if (name == null) { name = ""; }

            text += '<div class="textTableHistory">' +
                '<div class="boxProduct" onclick="productionClick(' + responseOrder[i].products.$values[0].id + ')">' +
                '<p class="boxText">' + name + '</p>' +
                generatorBoxProduct(history) +
                '</div>' +
                '</div>';
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

            let name = responseOrder[i].products.$values[j].name;
            if (name == null) { name = ""; }

            if (historysLength > 0) {
                let history = await getSimolHistory(actualHistoryJ);

                text += '<div class="textTableHistory">' +
                    '<div class="boxProduct" onclick="productionClick(' + responseOrder[i].products.$values[0].id + ')">' +
                    '<p class="boxText">' + name + '</p>' +
                    generatorBoxProduct(history) +
                    '</div>' +
                    '</div>';
            }
            else {
                text += '<div class="textTableHistory"><div class="boxProduct" onclick="productionClick(' + responseOrder[i].products.$values[0].id + ')""> </div> </div>';

            }
        }
        text += '</details>';
    }

    text += '</td>'

    return text;
}


function generatorBoxProduct(history) {

    totalPrice += history.price;
    let UnitPrice = history.price / (history.amount * history.kinds); 
    return '<table style="width: 100%;">' +
        ' <tr>' +
        '<td style="width: 16%;">' +
        '<p class="boxText">' + history.height + 'x' + history.whidth + '</p>' +
        '</td>' +
        '<td style="width: 16%;">' +
        '<p class="boxText">' + history.amount + 'x' + history.kinds + 'шт </p>' +
        '</td>' +
        '<td style="width: 36%;">' +
        '<p class="boxText">' + history.paperName + ' </p>' +
        '</td>' +
        '<td style="width: 16%;">' +
        '<p class="boxText">' + UnitPrice.toFixed(1) + ' руб/шт</p>' +
        '</td>' +
        '<td style="width: 16%;">' +
        '<p class="boxText">' + history.price + 'руб </p>' +
        '</td>' +
        '</tr>' +
        '</table>' +
        '<p class="boxText">' + generatorLogo(history) + '</p>';

}

function addContact(i) {
   let text = '<td>';
    for (const element of responseOrder[i].contacts.$values) {
        text += '<div class="textTableHistory" onclick="contactClick(' + element.id + ')">' +
            '<div class="boxProduct">' +
            '<p class="boxText">' + element.name + '</p>';
        if (element.mails.$values.length > 0) {
            text += '<p class="boxText">' + element.mails.$values[0].email + '</p>';
        }
        if (element.phoneNmbers.$values.length > 0) {
            text += '<p class="boxText">' + element.phoneNmbers.$values[0].number + '</p>';
        }

        text += '</div>' +
            '</div>'
    }
    text += '</td>'
    return text;
}

function addTotalPrice(i) {
    let color;
    if (responseOrder[i].stratusPayment == 0) {
        color = 'style="background-color: #9ee19f;"';
    }
    else if (responseOrder[i].stratusPayment == 1) {
        color = 'style="background-color: #eb6e6e;"';
    }
    else if (responseOrder[i].stratusPayment == 2) {
        color = 'style="background-color: #e390d6;"';
    }

    let text = '<td>' +
        '<div class="textTableHistory" ' + color+'>' + totalPrice + 'руб.</div>' +
        '<div>' +
        '<p class="boxText">' + '<button onclick="stratusPayment(' + responseOrder[i].id + ', 0)" class="buttonStatus">Оплачено </button></p>' +
        ' </div>' +
        '<div' +
        '<p class="boxText">' + '<button onclick="stratusPayment(' + responseOrder[i].id + ', 1)" class="buttonStatus">Не оплачено</button></p>' +
        '</div>' +
        '</td>';
    totalPrice = 0;
    return text;
}

function addData(i) {
    var msUTC = Date.parse(responseOrder[i].dateTime);

    return '<td>' +
        '<div class="textTableHistory">' + new Date(msUTC).toLocaleString() + '</div>' +
        '<p class="boxText">' + '<button onclick="statusOrder(' + responseOrder[i].id + ', 2)" class="buttonStatus">Отмена </button></p>' +
        '</td>';
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