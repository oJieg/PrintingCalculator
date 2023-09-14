//let idOrder;


async function loadPage() {
    $('#contactSpace').empty();
    let idOrder = $('#idOrder').attr("name")
    if (idOrder == 0) {
        /*------------*/
        idOrder = 1; //---
    }

    let order = await getOrderForId(idOrder);
    console.log(order);
    for (let i = 0; i < order.contacts.$values.length; i++) {
        $('#contactSpace').append(addContact(order.contacts.$values[i]));
    }
    for (let i = 0; i < order.products.$values.length; i++) {
        $('#product').append(addProduct(order.products.$values[i]));
        for (let j = 0; j < order.products.$values[i].histories.$values.length; j++) {
            $('#table' + order.products.$values[i].id).append(await addProductionTable(order.products.$values[i].histories.$values[j].id))
        }
    }


}




function addContact(contact) {

    console.log(contact);
    let text = '<div class="boxContact" onclick="clickContact(' + contact.id + ')">' +
        '<div class="close" onclick="deleteContact(' + contact.id + ');"> [x]</div>' +
        '<p class="boxTextContact">тел: ' + contact.name + '</p>';

    if (contact.mails.$values.length > 0) {
        text += '<p class="boxText">mail:' + contact.mails.$values[0].email + '</p>';
    }
    if (contact.phoneNmbers.$values.length > 0) {
        text += '<p class="boxText">тел:' + contact.phoneNmbers.$values[0].number + '</p>';
    }


    text += '</div>';
    return text;
}

function addProduct(product) {
    return '<div class="productSpace">' +
        ' <div class="horizontalInput">' +

        '  <div>' +
        '<div class="productionDetal">' +
        '<label class="labletext" for="Height">Название</label>' +
        '<input class="inputNumber" id="' + product.name + '" name="' + product.id + '" type="text" onchange="editNemaProduction(' + product.id + ');">' +
        ' </div>' +
        ' <div class="productionDetal">' +
        '<label class="labletext" for="Height">Описание</label>' +
        ' <input class="inputNumber" id="' + product.description + '" name="' + product.id + '" type="text" onchange="editDescriptionProduction(' + product.id + ');">' +
        '  </div>' +
        '        </div>' +

        '<div>' +
        '<p class="Price" id="productPrice" name="' + product.id + '">' + product.price + ' руб.</p>' +
        '</div>' +

        '<div class="centrButton">' +
        '<div class="button">' +
        '<input class="buttonNext" type="button" onclick="calk(' + product.id + ');" value="Посчитать">' +
        '</div>' +
        '</div>' +

        '</div>' +

        '<div class="tableFlex" >' +
        '<div style="overflow-x:auto;">' +
        '<table >' +
        '<tbody id="table' + product.id + '">' +
        '<tr>' +
        ' <th>' +
        ' <div class="textTableHistory">N</div>' +
        ' </th>' +
        '<th>' +
        '<div class="textTableHistory">Дата</div>' +
        '</th>' +
        ' <th>' +
        '<div class="textTableHistory">размер</div>' +
        '</th>' +
        '<th>' +
        '<div class="textTableHistory">тираж</div>' +
        '</th>' +
        '<th>' +
        '<div class="textTableHistory">бумага</div>' +
        '</th>' +
        '<th>' +
        '<div class="textTableHistory">постпечатная</div>' +
        '</th>' +
        '<th>' +
        '<div class="textTableHistory">цена</div>' +
        '</th>' +
        '<th>' +
        '<div class="textTableHistory">коментарий</div>' +
        ' </th>' +
        '<th>' +
        '<div class="textTableHistory">активировать</div>' +
        '</th>' +
        '</tr>' +
        '</tbody>' +
        '</table>' +
        '</div>' +
        '</div>' +

        '</div>';

}

function generatorLogo(history) {
    let PosLogo = "";
    if (history.lamination) {
        PosLogo += "<img class='imagPos' src='lamination.svg'/>";
    }
    if (history.creasing) {
        PosLogo += "<img class='imagPos' src='creesing.svg'/>";
    }
    if (history.drilling) {
        PosLogo += "<img class='imagPos' src='driling.svg'/>";
    }
    if (history.rounding) {
        PosLogo += "<img class='imagPos' src='rolling.svg'/>";
    }
    if (history.springBrochure) {
        PosLogo += "<img class='imagPos' src='SpringBrochure.svg'/>";
    }
    return PosLogo
}

async function addProductionTable(historyId) {
    let simplResult = await getSimolHistory(historyId);

    text = '<tr>'+
    '<td>' +
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
        '<td>' +
        '<div class="textTableHistoryComment">' + simplResult.comment + '</div>' +
        '</td>' +
        '<td>'+
        '<div class="button">'+
            '<input class="buttonNext" type="button" onclick="resultActive('+simplResult.historyId+');" value="-">'+
        '</div>'+
    '</td>'+
        '</tr>';
    return text;
}

async function getSimolHistory(id) {
    let respone = await fetch('https://localhost:7181/api/get-simpl-result' + id, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" }
    });

    let awner = await respone.json();

    if (awner.status.status != 0) {
        alert("не получилось получить SimplHistory222!" + awner.status.errorMassage)
    }

    return awner.simplResult;
}

async function getOrderForId(id) {
    let respone1 = await fetch('https://localhost:7181/api/order/get-order' + id, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
    });
    responseOrder = await respone1.json();

    if (responseOrder.status != 0) {
        alert("не получилось получить получить заказ по id!" + responseOrder.status.errorMassage)
    }


    return responseOrder.result;
}