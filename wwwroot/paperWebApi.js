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
    let newPrice = document.getElementById(id + "NewPrice").value;
    let oldPrice = document.getElementById(id + "Price");

    let newThickness = document.getElementById(id + "NewThickness").value;
    let oldThickness = document.getElementById(id + "Thickness");
    
    const respone1 = await fetch("/api/PaperEdit", {
        method: "Put",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            id: parseInt(id),
            newPrice: parseFloat(newPrice),
            status: parseInt(paperStatus),
            PaperThickness: parseFloat(newThickness)
        })
    })

    let bools = await respone1.json();
    if (bools != true) {
        alert("ошибка изменения");
        return;
    }
    if (paperStatus == -99) {
        oldPrice.innerHTML = newPrice + " руб";
        oldThickness.innerHTML = newThickness;
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

async function addPaper() {
    let name = document.getElementById("addPaperNeme").value;
    let price = document.getElementById("addPaperPrice").value;

    let selector = document.getElementById("addPaperSize");
    let nameSize = selector.options[selector.selectedIndex].text;;

    const respone = await fetch("/api/PaperEdit", {
        method: "Post",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            Name: name,
            Price: parseFloat(price),
            NameSize: nameSize
        })
    })
    let bools = await respone.json();
    if (bools == true) {
        location.reload();
    }
;}