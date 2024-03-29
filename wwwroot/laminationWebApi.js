﻿async function editLamination(id, paperStatus) {
    let newPrice = document.getElementById(id).value;
    let p = document.getElementById(id + "p");
    const respone1 = await fetch("/api/LaminationEdit", {
        method: "Put",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            id: parseInt(id),
            newPrice: parseFloat(newPrice),
            status: parseInt(paperStatus)
        })
    })

    let bools = await respone1.json();
    if (bools != true) {
        alert("ошибка изменения");
        return;
    }
    if (paperStatus == -99) {
        p.innerHTML = newPrice;
        return;
    }

    let tr = document.getElementById(id + "tr");
    if (tr.matches('.none')) {

        tr.setAttribute("class", "");
    }
    else {
        tr.setAttribute("class", "none");
    }
}

async function deleteLamination(id) {
    if (!confirm("вы уверенны? Стоит удалять только если эту бумагу больше не продается или не планируется ее закупать")) {
        return;
    }
    const respone = await fetch("/api/LaminationEdit/" + id, {
        method: "Delete",
        headers: { "Accept": "application/json" }
    })
    let bools = await respone.json();
    if (bools == true) {
        let tr = document.getElementById(id + "tr");
        tr.remove();
    }
}

async function addLamination() {
    let name = document.getElementById("addPaperNeme").value;
    let price = document.getElementById("addPaperPrice").value;

    const respone = await fetch("/api/LaminationEdit", {
        method: "Post",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            Name: name,
            Price: parseFloat(price)
        })
    })
    let bools = await respone.json();
    if (bools == true) {
        location.reload();
    }
    ;
}