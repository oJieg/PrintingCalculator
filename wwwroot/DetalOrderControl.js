let clicMiss = true;

async function newContact() {
    let newContact = await addNewContact($('#Name').val(), $('#Phone').val(), $('#Email').val());
    await addContactInOrder(getIdOrder(), newContact.id)
    $('#contactSpace').append(addContactInDetalOrder(newContact));
    console.log(newContact);
}
function clicTable(orderId) {
    if (clicMiss) {
        window.location.href = '/order/detal?id=' + orderId;

    }
    clicMiss = true;
}

async function deleteContact(IdContact) {
    clicMiss = false;
    if (await delContactInOrder(getIdOrder(), IdContact)) {
        $('#contact' + IdContact).remove();
    }
}

async function calk(productId) {
    let order = getOrder();
    let actualHistoruId;
    for (let i = 0; i < order.products.$values.length; i++) {
        if (order.products.$values[i].id == productId) {
            actualHistoruId = order.products.$values[i].activecalculationHistoryId;
        }
    }
    window.location.href = '/Calculator?historyId=' + actualHistoruId + '&productId=' + productId + '&orderId=' + order.id;
}


function contactClick(contactId) {
    if (clicMiss) {
        $("#vignette").fadeIn();
        $("#detalContact").fadeIn();
        detalContact(contactId);
    }
    clicMiss = true;
}

function closeVenetka() {
    $('#vignette').fadeOut();
    // $("#detalProduction").fadeOut();
    $("#detalContact").fadeOut();
    $('#detalHistory').fadeOut();
}

async function ContactInOrder(IdContact) {
    //alert();
    let contact = await addContactInOrder(getIdOrder(), IdContact)
    $('#contactSpace').append(addContactInDetalOrder(contact));
}

async function delContactInOrder(orderId, contactId) {
    let respone = await fetch('/api/order/del-contact' + orderId + '?contactId=' + contactId, {
        method: "Delete",
        headers: { "Accept": "application/json", "Content-Type": "application/json" }
    });

    let awner = await respone.json();

    console.log(awner);

    if (awner.status != 0) {
        alert("не получилось удалить контакт из ордера!" + awner.status.errorMassage)
        return false;
    }

    return true;
}

async function addNewProduct() {
    let product = await addNewProductInOrder(await NewProduct());
    window.location.href = '/Calculator?historyId=' + 0 + '&productId=' + product.id + '&orderId=' + order.id;
    //console.log(product);
    //$('#product').append(addProductInDetalOrder(product));
}

async function deleteProduct(productId) {
    if (apiDeleteProduct(productId)) {
        $('#productSpace' + productId).remove();
    }
}


async function apiDeleteProduct(productId) {
    let respone = await fetch('/api/order/del-product' + getIdOrder() + '?productId=' + productId, {
        method: "Delete",
        headers: { "Accept": "application/json", "Content-Type": "application/json" }
    });

    let awner = await respone.json();

    console.log(awner);

    if (awner.status != 0) {
        alert("не получилось добавить контакт в ордер!" + awner.status.errorMassage)
        return false;
    }

    return awner.result;
}

async function NewProduct() {
    let respone = await fetch('/api/product/add-new-product', {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" }
    });

    let awner = await respone.json();

    console.log(awner);

    if (awner.status != 0) {
        alert("не получилось добавить контакт в ордер!" + awner.status.errorMassage)
        return false;
    }
    //addNewProduct();
    return awner.result.id;
}

async function addNewProductInOrder(productId) {
    let respone = await fetch('/api/order/add-product' + getIdOrder() + '?productId=' + productId, {
        method: "Post",
        headers: { "Accept": "application/json", "Content-Type": "application/json" }
    });

    let awner = await respone.json();

    console.log(awner);

    if (awner.status != 0) {
        alert("не получилось добавить контакт в ордер!" + awner.status.errorMassage)
        return false;
    }

    return awner.result;
}

async function editNemaProduction(productId) {
    console.log('https://localhost:7181/api/product/edit-product?productId=' + productId +
        '&name=' + $('#name' + productId).val() +
        '&description=' + $('#description' + productId).val());
    let name = $('#name' + productId).val();
    if (name == null) {
        name = '';
    }

    let respone = await fetch('https://localhost:7181/api/product/edit-product?productId=' + productId +
        '&name=' + name +
        '&description=' + $('#description' + productId).val(), {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" }
    });

    let awner = await respone.json();

    console.log(awner);

    if (awner.status != 0) {
        alert("не получилось добавить контакт в ордер!" + awner.status.errorMassage)
        return false;
    }

    return awner.result;
}
async function addNewContact(name, phone, mail) {
    if (name != "") {
        name = 'name=' + name + '&';
    }
    if (phone != "") {
        phone = 'phone=' + phone;
    }
    if (mail != "") {
        mail = 'eMail=' + mail + '&';
    }

    let respone = await fetch('/api/contact/add-new-contact?' + name + mail + phone, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" }
    });

    let awner = await respone.json();

    //console.log(awner);

    if (awner.status != 0) {
        alert("не получилось изменить статус!" + awner.status.errorMassage)
    }

    return awner.result;
}


async function addContactInOrder(orderId, contactId) {
    let respone = await fetch('/api/order/add-contact' + orderId + '?contactId=' + contactId, {
        method: "Post",
        headers: { "Accept": "application/json", "Content-Type": "application/json" }
    });

    let awner = await respone.json();

    // console.log(awner);

    if (awner.status != 0) {
        alert("не получилось добавить контакт в ордер!" + awner.status.errorMassage)
        return false;
    }

    return awner.result;
}

async function editActivecalculationHistoryId(historyId, productId) {
    //  console.log(historyId, productId)
    let respone = await fetch('/api/product/edit-active-history?productId=' + productId + '&historyId=' + historyId, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" }
    });

    let awner = await respone.json();

    // console.log(awner);

    if (awner.status != 0) {
        alert("не получилось изменить статус!" + awner.status.errorMassage)
        return false;
    }

    return true;
}
//