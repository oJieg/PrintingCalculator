async function detalContact(contactId) {
    $('#TableContactOrder > > .mainTableRow').empty();
    let contact = await getContact(contactId);
    console.log(contact);
    addInformatioContact(contact);
    await generatorTable(await listOrderForId(contact), 'TableContactOrder')
}

async function editNemaContact(contactId){
    await apiEditContact(contactId,$('#nameContact'+contactId).val(),$('#descriptionContact'+contactId).val());
}

async function addPhoneContact(contactId){
   await apiAddNewPhoneInContact(contactId, $('#newPhoneContact'+ contactId).val());
   await detalContact(contactId);
}

async function addMailContact(contactId){
    await apiAddNewMailInContact(contactId, $('#newMailContact'+ contactId).val());
    await detalContact(contactId);
}

async function delPhoneContact(contactId, number){
    await apiDelPhoneContact(contactId, number);
    await detalContact(contactId);
}

async function delMailContact(contactId, number){
    await apiDelMailContact(contactId,number);
    await detalContact(contactId);
}

function addInformatioContact(contact) {
    // '<input class="inputNumber" id="name' + product.id + '" value="'+name+'" type="text" onchange="editNemaProduction(' + product.id + ');">' +
    $('#contactName').html('<input class="inputNumberContact" id="nameContact' + contact.id + '" value="' + contact.name + '" type="text" onchange="editNemaContact(' + contact.id + ');">');
    $('#contactDetal').html('<input class="inputNumberContact" id="descriptionContact' + contact.id + '" value="' + contact.description + '" type="text" onchange="editNemaContact(' + contact.id + ');">');
    //contact.description);
    let allPhone = "";
    if (contact.phoneNmbers.$values.length > 0) {

        for (let i = 0; i < contact.phoneNmbers.$values.length; i++) {
            allPhone += '<div class="horizontalFlex" id="opacitySize">' +
                '<p class="contactText">' + contact.phoneNmbers.$values[i].number + '</p>' +
                '<p class="contactText" onclick="delPhoneContact('+contact.id+',\''+contact.phoneNmbers.$values[i].number+'\');">[del]</p>' +
                '</div>';
        }
    }
    allPhone += '<p>добавить новый:</p>' +
        '<input class="inputNumberContact" id="newPhoneContact' + contact.id + '" type="text" onchange="addPhoneContact(' + contact.id + ');">'
    $('#contactPhone').html(allPhone);

    let allMail = "";
    if (contact.mails.$values.length > 0) {

        for (let i = 0; i < contact.mails.$values.length; i++) {
            allMail += '<div class="horizontalFlex" id="opacitySize">' +
                '<p class="contactText">' + contact.mails.$values[i].email + '</p>' +
                '<p class="contactText" onclick="delMailContact('+contact.id+',\''+contact.mails.$values[i].email+'\');">[del]</p>' +
                '</div>';
        }
    }
    allMail += '<p>добавить новый:</p>' +
        '<input class="inputNumberContact" id="newMailContact' + contact.id + '" type="text" onchange="addMailContact(' + contact.id + ');">'
    $('#contactEmail').html(allMail);
}

async function listOrderForId(contact) {
    let listOrder = [];
    for (let i = 0; i < contact.orders.$values.length; i++) {
        listOrder[i] = await getOrderForId2(contact.orders.$values[i].id);
    }
    console.log(listOrder)
    return (listOrder);
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

async function apiEditContact(contactId, name, description){
    let respone1 = await fetch('/api/contact/edit-contact'+contactId+'?name='+name+'&description='+ description, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
    });
    responseOrder = await respone1.json();

    if (responseOrder.status != 0) {
        alert("не получилось получить получить заказ по id!" + responseOrder.status.errorMassage)
    }


    return responseOrder.result;
}

async function apiAddNewPhoneInContact(contactId, newPhone){
    let respone1 = await fetch('/api/contact/add-phone'+contactId+'?phone='+ newPhone, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
    });
    responseOrder = await respone1.json();

    if (responseOrder.status != 0) {
        alert("не получилось получить получить заказ по id!" + responseOrder.status.errorMassage)
    }


    return responseOrder.result;
}


async function apiAddNewMailInContact(contactId, newMail){
    let respone1 = await fetch('/api/contact/add-mail'+contactId+'?eMail='+ newMail, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
    });
    responseOrder = await respone1.json();

    if (responseOrder.status != 0) {
        alert("не получилось получить получить заказ по id!" + responseOrder.status.errorMassage)
    }


    return responseOrder.result;
}

async function apiDelMailContact(contactId, mail){
    let respone1 = await fetch('https://localhost:7181/api/contact/del-mail'+contactId+'?eMail='+ mail, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
    });
    responseOrder = await respone1.json();

    if (responseOrder.status != 0) {
        alert("не получилось получить получить заказ по id!" + responseOrder.status.errorMassage)
    }


    return responseOrder.result;
}
async function apiDelPhoneContact(contactId, phone){
    let respone1 = await fetch('/api/contact/del-phone'+contactId+'?phone='+ phone, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
    });
    responseOrder = await respone1.json();

    if (responseOrder.status != 0) {
        alert("не получилось получить получить заказ по id!" + responseOrder.status.errorMassage)
    }


    return responseOrder.result;
}