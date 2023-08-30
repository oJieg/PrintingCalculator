let height = 297;
let width = 420;

const classFastHover = "FastHover";
const classDefaultHover = "FastSize";

const elementHeight = document.getElementById("Height");
const elenentWidth = document.getElementById("Width");

const elementHeightId = document.getElementById("Height");
const elementWidthtId = document.getElementById("Width");
const PaperId = document.getElementById("Paper");
const AmountId = document.getElementById("Amount");
const KindstId = document.getElementById("Kinds");
const DuplexId = document.getElementById("Duplex");
const LaminationNameId = document.getElementById("LaminationName");
const CreasingId = document.getElementById("Creasing");
const DrillingId = document.getElementById("Drilling");
const RoundingId = document.getElementById("Rounding");
const CommonToAllMarkupName = document.getElementsByName("CommonToAllMarkup");

const BrochureValue = document.getElementById("Brochure");
function loadPage() {
brochure();
}

function newSize(size, side) {
    if (side == "Height") {
        height = size;
    }

    if (side == "Width") {
        width = size;
    }
    activeButtonSize()
}

function editSize(heightPaper, widthPaper) {
    elementHeight.value = heightPaper;
    elenentWidth.value = widthPaper;
    height = heightPaper;
    width = widthPaper;
    activeButtonSize()
}

function activeButtonSize() {
    let elementFastSize = document.querySelectorAll("div.FastSizeBlock > input");

    let idSize;
    if (height > width) {
        idSize = `${width}${height}`;
    }
    else {
        idSize = `${height}${width}`;
    }

    for (let item of elementFastSize) {
        if (item.id == idSize) {
            item.className = classFastHover;
        }
        else {
            if (item.className != classDefaultHover) {
                item.className = classDefaultHover;
            }
        }
    }
}
const brochureSpringId = document.getElementById("BrochureSpring");

function springBrochure() {
    if (isBrochureSpring) {
        switch (brochureSpringId.value) {
            case "noCover": return 1;
            case "CoverPlasticAndCardboard": return 2;
            case "CoverTwoPlastics": return 3;
        }
    }
    return 0;
}


async function calk() {
    let list = CommonToAllMarkupName;
    let CommonToAllMarkupsValue = [];
    for (let i = 0; i < list.length; i++) {
        if (list[i].checked) {
            CommonToAllMarkupsValue.push(list[i].value);
        }
    }

    let Input = {
        Height: parseInt(elementHeightId.value),
        Whidth: parseInt(elementWidthtId.value),
        Paper: PaperId.value,
        Amount: parseInt(AmountId.value),
        Kinds: parseInt(KindstId.value),
        Duplex: Boolean(DuplexId.checked),
        LaminationName: LaminationNameId.value,
        Creasing: parseInt(CreasingId.value),
        Drilling: parseInt(DrillingId.value),
        Rounding: Boolean(RoundingId.checked),
        CommonToAllMarkup: CommonToAllMarkupsValue,
        NoSaveDB: false,
        SpringBrochure: springBrochure()
    }

    let respone1 = await fetch('/api/Calculation', {
        method: "Post",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify(Input)
    });
    let historyId = await respone1.json();
    if (historyId < 0) {
        alert("�������������� ������1111111111!")
    }
    else {
        window.location.href = 'CalculatorResult?id=' + historyId;
    }

}
const springElem = document.getElementById("spring");
const invizibleFastHover = document.getElementById("invizibleFastHover");
const DiaphanousElem = document.getElementsByClassName("Diaphanous");
const polosId = document.getElementById("Polos");
const brochureAmoutId = document.getElementById("BrochureAmout");
let isBrochureSpring = false;

function editPolos() {
    if (isBrochureSpring) {
        AmountId.value = brochureAmoutId.value
        if (DuplexId.checked) {
            KindstId.value = Math.ceil(polosId.value / 2);
        }
        else {
            KindstId.value = polosId.value;
        }
    }
}

function editSizePolos(heightPaper, widthPaper) {

}


function brochure() {
    let value = BrochureValue.value;
    if (value == "spring") {
        isBrochureSpring = true;
        springElem.style.cssText = "";
        // invizibleFastHover.style.cssText= "display: none;";
        for (let i = 0; i < DiaphanousElem.length; i++) {
            DiaphanousElem[i].style.cssText = "opacity: 0.2; ";
        }
    }
    else {
        isBrochureSpring = false;
        springElem.style.cssText = "display: none;";
        invizibleFastHover.style.cssText = "";
        for (let i = 0; i < DiaphanousElem.length; i++) {
            DiaphanousElem[i].style.cssText = "";
        }
    }
}
function calkSize() {

}