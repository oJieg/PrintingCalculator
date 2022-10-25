async function deletePaper(id) {
    if (!confirm("вы уверенны? Стоит удалять только если эту бумагу больше не продается или не планируется ее закупать")) {
        return;
    }
    const respone = await fetch("/api/PaperEdit/" + id, {
        method: "Delete",
        headers: { "Accept": "application/json" }
    })
    let bools = await respone.json();
    if (bools == true) {
        let tr = document.getElementById(id + "tr");
        tr.remove();
    }
}

async function editPaper(id, paperStatus) {
    let newPrice = document.getElementById(id).value;
    let p = document.getElementById(id + "p");
    const respone1 = await fetch("/api/PaperEdit", {
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