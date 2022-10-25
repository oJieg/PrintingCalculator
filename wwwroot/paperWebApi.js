
async function delitePaper(id) {

    const respone = await fetch("/api/PaperEdit/" + id, {
        method: "Delete",
        headers: { "Accept": "application/json" }
    });
    let bools = await respone.json();
        alert(bools);
}
async function editPaper(id, paperStatus) {
    let newPrice = document.getElementById(id);
    let tr = document.getElementById(id + "tr");
    let p = document.getElementById(id + "p");
    const respone1 = await fetch("/api/PaperEdit", {
        method: "Put",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            id: parseInt(id),
            newPrice: parseFloat(newPrice),
            status: parseInt(paperStatus)
        })
    });

    let bools = await respone1.json();
    p.innerHTML = "dfdf";
    if (bools == trye && paperStatus == -99) {

        
    }

}