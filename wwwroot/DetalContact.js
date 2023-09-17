async function detalContact(contactId) {
    $('#TableContactOrder > > .mainTableRow').empty();
    let contact = await getContact(contactId);
    console.log(contact);
    addInformatioContact(contact);
    await generatorTable( await listOrderForId(contact), 'TableContactOrder')
}

function addInformatioContact(contact) {
    $('#contactName').html(contact.name);
    $('#contactDetal').html(contact.description);

    if (contact.phoneNmbers.$values.length > 0) {
        let allPhone = "";
        for (let i = 0; i < contact.phoneNmbers.$values.length; i++) {
            allPhone += '<h2 class="contactMail">' + contact.phoneNmbers.$values[i].number + '</h2>'
        }
        $('#contactPhone').html(allPhone);
    }
    else{$('#contactPhone').empty();}

    if (contact.mails.$values.length > 0) {
        let allMail = "";
        for (let i = 0; i < contact.mails.$values.length; i++) {
            allMail += '<h2 class="contactMail">' + contact.mails.$values[i].email + '</h2>'
        }
        $('#contactEmail').html(allMail);
    }
    else{$('#contactEmail').empty();}
}

async function listOrderForId(contact){
    let listOrder = [];
    for(let i=0; i<contact.orders.$values.length; i++){
        listOrder[i] = await getOrderForId2( contact.orders.$values[i].id);
    }
    console.log(listOrder)
    return(listOrder);
}

async function getContact(contactId) {
    let respone = await fetch('/api/contact/get-contact' + contactId, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" }
    });

    let awner = await respone.json();
    if (awner.status != 0) {
        alert("не получилось получить product!" + awner.status.errorMassage)
    }

    return awner.result;
}

async function getOrderForId2(id) {
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
