
let maxCountClu = 5;
$('#Name').blur(async function () {
      if ($(this).val() == '') {
        fillTableContact(await apiGetListContact());
      }
  });
  $('#Phone').blur( async function () {
      if ($(this).val() == '') {
        fillTableContact(await apiGetListContact());
      }
  });
  $('#Email').blur(async function () {
      if ($(this).val() == '') {
        fillTableContact(await apiGetListContact());
      }
  });
  
$('#Email').on('input', async function () {
    if ($(this).val() != '') {
        fillTableContact(await findContactForEmail($(this).val()));
    }
});
$('#Phone').on('input', async function () {
    if ($(this).val() != '') {
       // console.log(await findContactForPhone($(this).val()));
       fillTableContact(await findContactForPhone($(this).val()));
    }
});
$('#Name').on('input', async function () {
    if ($(this).val() != '') {
       fillTableContact(await findContactForName($(this).val()));
    }
});

async function findContactForPhone(phone) {
    let respone1 = await fetch('/api/contact/searth-contact-by-phone?phone=' + phone, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
    });
    responseOrder = await respone1.json();
    if (responseOrder.status != 0 && responseOrder.status != 4) {
        alert("не получилось получить Product!" + responseOrder.status.errorMassage)
    }
    return responseOrder.result;
}

async function findContactForEmail(Email) {
    let respone1 = await fetch('/api/contact/searth-contact-by-email?email=' + Email, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
    });
    responseOrder = await respone1.json();
    if (responseOrder.status != 0 && responseOrder.status != 4) {
        alert("не получилось получить Product!" + responseOrder.status.errorMassage)
    }
    return responseOrder.result;
}

async function findContactForName(name) {
    let respone1 = await fetch('/api/contact/searth-contact-by-name?name=' + name, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
    });
    responseOrder = await respone1.json();
    if (responseOrder.status != 0 && responseOrder.status != 4) {
        alert("не получилось получить Product!" + responseOrder.status.errorMassage)
    }
    return responseOrder.result;
}

