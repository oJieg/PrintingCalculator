let height = 297;
let width = 420;

let heightStaple = 210;
let widthStaple = 297;

const classFastHover = "FastHover";
const classDefaultHover = "FastSize";

const elementHeight = document.getElementById("Height");
const elenentWidth = document.getElementById("Width");

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

let isBrochureSpring = false;
let isBrochureStaple = false;
function loadPage() {
    brochure();
    calkSizeStaple();
}

function newSize(size, side) {
    if (side == "Height") {
        height = size;
    }

    if (side == "Width") {
        width = size;
    }
    activeButtonSize();
}


function editSize(heightPaper, widthPaper) {
    elementHeight.value = heightPaper;
    elenentWidth.value = widthPaper;
    height = heightPaper;
    width = widthPaper;
    activeButtonSize();
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

function newSizeStaple(size, side) {
    if (side == "Height") {
        heightStaple = size;
    }

    if (side == "Width") {
        widthStaple = size;
    }
    activeButtonSize();
}

function editSizeStaple(heightPaper, widthPaper) {
    HeightBrochureStapleId.value = heightPaper;
    WidthBrochureStapleId.value = widthPaper
    heightStaple = heightPaper;
    widthStaple = widthPaper;
    activeButtonSizeStaple();
}

function activeButtonSizeStaple() {
    let elementFastSize = document.querySelectorAll("div.FastSizeBlockStaple > input");

    let idSize;
    if (heightStaple > widthStaple) {
        idSize = `${widthStaple}${heightStaple}`;
    }
    else {
        idSize = `${heightStaple}${widthStaple}`;
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
        Height: parseInt(elementHeight.value),
        Whidth: parseInt(elenentWidth.value),
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
        SpringBrochure: springBrochure(),
        StapleBrochure: isBrochureStaple
    }
    console.log(Input);
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
const stapleElem = document.getElementById("Staple");
const invizibleFastHover = document.getElementById("invizibleFastHover");
const opacitySizeElem = document.getElementById("opacitySize");
const DiaphanousElem = document.getElementsByClassName("Diaphanous");

const polosId = document.getElementById("Polos");
const brochureAmoutId = document.getElementById("BrochureAmout");

const polosStapleId = document.getElementById("PolosStaple");
const brochureAmoutStapleId = document.getElementById("BrochureAmoutStaple");

const tooltiptextId = document.getElementById("tooltiptext");



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

function editPolosStaple() {
    if (isBrochureStaple) {
        AmountId.value = brochureAmoutStapleId.value
        if (DuplexId.checked) {
            KindstId.value = Math.ceil(polosStapleId.value / 4);
        }
        else {
            KindstId.value = Math.ceil(polosStapleId.value / 2);
        }
        if (polosStapleId.value % 4 != 0) {
            tooltiptextId.className = "tooltiptextVisibility";
        }
        else {
            tooltiptextId.className = "tooltiptext";
        }
    }
}

const HeightBrochureStapleId = document.getElementById("HeightBrochureStaple");
const WidthBrochureStapleId = document.getElementById("WidthBrochureStaple");

function brochure() {
    let value = BrochureValue.value;
    if (value == "spring") {
        isBrochureSpring = true;
        springElem.style.cssText = "";
        for (let i = 0; i < DiaphanousElem.length; i++) {
            DiaphanousElem[i].style.cssText = "opacity: 0.2; ";
        }
    }
    else if (value == "staple") {
        CreasingId.value = 1;
        isBrochureStaple = true;
        stapleElem.style.cssText = "";
        springElem.style.cssText = "display: none;";
        invizibleFastHover.style.cssText = "display: none;";
        for (let i = 0; i < DiaphanousElem.length; i++) {
            DiaphanousElem[i].style.cssText = "opacity: 0.2; ";
        }
        opacitySizeElem.style.cssText = "opacity: 0.2; ";
    }
    else {
        CreasingId.value = 0;
        isBrochureSpring = false;
        isBrochureStaple = false;
        stapleElem.style.cssText = "display: none;";
        springElem.style.cssText = "display: none;";
        invizibleFastHover.style.cssText = "";
        for (let i = 0; i < DiaphanousElem.length; i++) {
            DiaphanousElem[i].style.cssText = "";
        }
        opacitySizeElem.style.cssText = "";
    }
}


function calkSizeStaple() {
    if (isBrochureStaple) {
        elementHeight.value = HeightBrochureStapleId.value * 2;
        elenentWidth.value = WidthBrochureStapleId.value;
    }
}