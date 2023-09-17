//let idOrder;
let opacity = 30;
let order;
let idOrder = $('#idOrder').attr("name");
let activeClick = true;
function getIdOrder(){
    return idOrder;
}
function getOrder(){
    return order;
}

async function loadPage() {
    $('#contactSpace').empty();
   
    if (idOrder == 0) {
        /*------------*/
        idOrder = 1; //---
    }

    order = await getOrderForId(idOrder);
    console.log(order);
    for (let i = 0; i < order.contacts.$values.length; i++) {
        $('#contactSpace').append(addContactInDetalOrder(order.contacts.$values[i]));
    }
    for (let i = 0; i < order.products.$values.length; i++) {
        $('#product').append(addProductInDetalOrder(order.products.$values[i]));
        for (let j = 0; j < order.products.$values[i].histories.$values.length; j++) {
            $('#table' + order.products.$values[i].id).append(await addProductionTableInDetalOrder(order.products.$values[i].histories.$values[j].id, order.products.$values[i]))
        }
            $('#history'+order.products.$values[i].activecalculationHistoryId + order.products.$values[i].id).css('opacity', '100%');
    }
}
async function getResult(historyId){
    if(activeClick){
    $('#vignette').fadeIn();
    await result(historyId);
    }
    else{ activeClick = true}
}

async function ActivecalculationHistoryId(historyId, productId  ){
    activeClick = false;
   if( await editActivecalculationHistoryId(historyId, productId)){
    $('#history'+historyId + productId).css('opacity', '100%');
    $('#history'+order.products.$values[getacAtivecalculationHistoryId(productId)].activecalculationHistoryId + productId).css('opacity', opacity+'%');

    order = await getOrderForId(idOrder);
   }
}

function getacAtivecalculationHistoryId(productId){
    for (let i = 0; i < order.products.$values.length; i++) {
        if(order.products.$values[i].id==productId){
            return i;
        }
    }
}

function addContactInDetalOrder(contact) {

   // console.log(contact);
    let text = '<div class="boxContact" id="contact' + contact.id + '" onclick="contactClick(' + contact.id + ')">' +
        '<div class="close" onclick="deleteContact(' + contact.id + ');"> [x]</div>' +
        '<p class="boxTextContact">тел: ' + contact.name + '</p>';

    if (contact.mails!=null && contact.mails.$values.length > 0) {
        text += '<p class="boxTextContact">mail:' + contact.mails.$values[0].email + '</p>';
    }
    if (contact.phoneNmbers != null && contact.phoneNmbers.$values.length > 0) {
        text += '<p class="boxTextContact">тел:' + contact.phoneNmbers.$values[0].number + '</p>';
    }


    text += '</div>';
    return text;
}

function addProductInDetalOrder(product) {
    let name= product.name;
    let description=product.description;
if(name  == null){
    name = '';
}
if(description==null){
    description='';
}
    //console.log(product);
    return '<div class="productSpace" id="productSpace'+product.id+'">' +
        ' <div class="horizontalInput">' +

        '  <div>' +
        '<div class="productionDetal">' +
        '<label class="labletext" for="Height">Название</label>' +
        '<input class="inputNumber" id="name' + product.id + '" value="'+name+'" type="text" onchange="editNemaProduction(' + product.id + ');">' +
        ' </div>' +
        ' <div class="productionDetal">' +
        '<label class="labletext" for="Height">Описание</label>' +
        ' <input class="inputNumber" id="description' + product.id + '" value="'+description+'" type="text" onchange="editNemaProduction(' + product.id + ');">' +
        '  </div>' +
        '        </div>' +

        '<div>' +
        '<p class="Price" id="productPrice" name="' + product.id + '">' + product.price + ' руб.</p>' +
        '</div>' +

        '<div class="centrButton">' +
        '<div class="button">' +
        '<input class="buttonNext" type="button" onclick="calk(' + product.id + ');" value="Посчитать">' +
        
        '</div>' +
        '<div class="button">' +
        '<input class="buttonNext" type="button" onclick="deleteProduct(' + product.id + ');" value="Удалить">' +
        
        '</div>' +
        
        '</div>' +

        '</div>' +

        '<div class="tableFlex" >' +
        '<div style="overflow-x:auto;margin: auto;">' +
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

async function addProductionTableInDetalOrder(historyId, product) {
    let simplResult = await getSimolHistory(historyId);
    
    text = '<tr id="history'+historyId+product.id+'" onclick="getResult('+historyId+');" style="opacity:'+opacity+'%">'+
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
        '<td>'+
        '<div class="button">'+
            '<input class="buttonNext" type="button" onclick="ActivecalculationHistoryId('+simplResult.historyId+', '+product.id+', );" value="-">'+
        '</div>'+
    '</td>'+
        '</tr>';
    return text;
}

async function getSimolHistory(id) {
    let respone = await fetch('/api/get-simpl-result' + id, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" }
    });

    let awner = await respone.json();

    if (awner.status.status != 0) {
        alert("не получилось получить SimplHistory!" + awner.status.errorMassage)
    }

    return awner.simplResult;
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

    return responseOrder.result;
}