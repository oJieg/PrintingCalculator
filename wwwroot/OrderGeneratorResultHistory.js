async function result(historyId){
    $('#detalHistory').empty();
    $('#detalHistory').fadeIn();
 let result = await getHistory(historyId);
 console.log(result);
 $('#detalHistory').append( generatorResult(result));
}

function closeHistory(){
    $('#detalHistory').fadeOut();
}

async function getHistory(id) {
    let respone = await fetch('/api/get-result' + id, {
        method: "Get",
        headers: { "Accept": "application/json", "Content-Type": "application/json" }
    });

    let awner = await respone.json();

    if (awner.status.status != 0) {
        alert("не получилось получить SimplHistory222!" + awner.status.errorMassage)
    }

    return awner.result;
}

function generatorResult(result){
    let size = result.height+'x'+result.whidth;
    let amoutKids = result.amount+'x'+result.kinds;
    let duplex='4+0';
    if(result.paperResult.Duplex){
        duplex ="4+4";
    }
    let PriceForOne = Math.round(result.price / (result.amount * result.kinds), 2); //округлить до 3х знаков
    let pricePos = result.posResult.creasingPrice + result.posResult.drillingPrice + result.posResult.roundingPrice;
    let dor ='';
    if (result.commonToAllMarkupName != null && result.commonToAllMarkupName.$values.length){
        for(let i=0;i<result.commonToAllMarkupName.$value.length;i++ ){
            dor += '<p class="margin">'+result.commonToAllMarkupName.$value[i].sescription+'</p>';
        }
    }

    return		'<div class="close" onclick="closeHistory();"> [x]</div>'+
    '<div class="twoColumnsFlex">'+

    '<div class="priceFlex">'+

        '<div class="totalPrice" onclick="CopyPrice('+result.price+')">'+
            '<span class="tooltiptext" id="tooltiptext">Цена в буфере обмена</span>'+
            '<p class="totalPrice">Итого</p>'+
            '<h1 class="totalPrice" >'+result.price+' руб.</h1>'+
            '<h2 class="totalPrice">'+PriceForOne+' руб/шт</h2>'+
            '<h4 class="totalPrice">'+result.dateTime+'</h4>'+
        '</div>'+

        '<div class="detalPriceBox">'+
            '<div class="grayBackground">'+
                '<p class="leftTextDetalPrice">Бумага</p>'+
                '<p class="rightTextDetalPrice">'+result.paperResult.price+' руб.</p>'+
            '</div>'+
            '<div class="detal">'+
                '<details>'+
                    '<summary class="detal">подробнее</summary>'+
                    '<p class="detal">себестоимость расходников - '+result.paperResult.costConsumablePrise+' руб.</p>'+
                    '<p class="detal">цена резки - '+result.paperResult.cutPrics+' руб.</p>'+
                '</details>'+
            '</div>'+
        '</div>'+
        '<div class="detalPriceBox">'+
            '<div class="grayBackground">'+
                '<p class="leftTextDetalPrice">Ламинация</p>'+
                '<p class="rightTextDetalPrice">'+result.laminationResult.price+' руб.</p>'+
            '</div>'+
           ' <div class="detal">'+
                '<details>'+
                    '<summary class="detal">подробнее</summary>'+
                    '<p class="detal">'+result.laminationResult.name+'</p>'+
                '</details>'+
            '</div>'+
        '</div>'+

        '<div class="detalPriceBox">'+
            '<div class="grayBackground">'+
                '<p class="leftTextDetalPrice">Постпечатная</p>'+
                '<p class="rightTextDetalPrice">'+pricePos+' руб.</p>'+
            '</div>'+
            '<div class="detal">'+
                '<details>'+
                    '<summary class="detal">подробнее</summary>'+
                    '<p class="detal">биговка - '+result.posResult.creasingPrice+' руб.('+result.posResult.creasingAmount+' шт.)</p>'+
                    '<p class="detal">сверление - '+result.posResult.drillingPrice+' руб. ('+result.posResult.drillingAmount+' шт.)</p>'+
                    '<p class="detal">скругление углов - '+result.posResult.roundingPrice+' руб.</p>'+
                '</details>'+
           ' </div>'+
        '</div>'+
        '<div class="detalPriceBox">'+
            '<div class="detalPriceBox">'+
                '<div class="grayBackground">'+
                    '<p class="leftTextDetalPrice">Брошюра</p>'+

                    '<p class="rightTextDetalPrice">'+(result.springBrochure.price + result.posResult.stapleBrochurePrice)+' руб.</p>'+
                '</div>'+
                '<div class="detal">'+
                    '<details>'+
                        '<summary class="detal">подробнее</summary>'+

                            '<p class="detal">'+result.springBrochure.springBrochure+'</p>'+

                            '<p class="detal">На пружину '+result.springBrochure.price+' руб</p>'+
                            '<p class="detal"> '+(result.springBrochure.price / result.amount)+' руб/шт</p>'+

                            '<p class="detal">На скрепку '+result.posResult.stapleBrochurePrice+' руб</p>'+
                            '<p class="detal"> '+(result.posResult.stapleBrochurePrice / result.amount)+' руб/шт</p>'+
                    '</details>'+
                '</div>'+
           ' </div>'+

        '</div>'+
    '</div>'+

    '<div class="rowInputFlex">'+
        '<div class="paperInput">'+
            '<div class="green">'+
                '<p class="margin2"><b>Бумага</b></p>'+
            '</div>'+
            '<div class="grey">'+
                '<p class="margin">'+result.paperResult.namePaper+'</p>'+
            '</div>'+
        '</div>'+

        '<div class="columnInputFlex">'+
            '<div class="rowInputLitleFlex">'+

                '<div class="smallInput">'+
                    '<div class="green">'+
                        '<p class="margin">Размер изделия</p>'+
                    '</div>'+
                    '<div class="grey">'+
                        '<p class="margin">'+size+'</p>'+
                    '</div>'+
                '</div>'+
                '<div class="smallInput">'+
                    '<div class="green">'+
                        '<p class="margin">Кол-во/Видов</p>'+
                    '</div>'+
                    '<div class="grey">'+
                        '<p class="margin">'+amoutKids+' шт.</p>'+
                    '</div>'+
                '</div>'+

            '</div>'+
            '<div class="rowInputLitleFlex">'+

                '<div class="smallInput">'+
                    '<div class="green">'+
                        '<p class="margin">Цветность</p>'+
                    '</div>'+
                    '<div class="grey">'+
                        '<p class="margin">'+duplex+'</p>'+
                    '</div>'+
                '</div>'+
                '<div class="smallInput">'+
                    '<div class="green">'+
                        '<p class="margin">Листов</p>'+
                    '</div>'+
                    '<div class="grey">'+
                        '<p class="margin">'+result.paperResult.sheets+' л.</p>'+
                    '</div>'+
                '</div>'+

            '</div>'+

            '<div class="rowInputLitleFlex">'+

                '<div class="smallInput">'+
                    '<div class="green">'+
                        '<p class="margin">доп опции</p>'+
                    '</div>'+
                    '<div class="grey">'+ 
                    dor+
                    '</div>'+
                '</div>'+
                '<div class="smallInput">'+
                    '<div class="green">'+
                        '<p class="margin">шт/лист</p>'+
                    '</div>'+
                    '<div class="grey">'+
                        '<p class="margin">'+result.paperResult.piecesPerSheet+' шт на л.</p>'+
                    '</div>'+
                '</div>'+

            '</div>'+

        '</div>'+
    '</div>'+
'</div>'+
'<div class="button">'+
    '<input class="buttonNext" type="button" onclick="closeHistory()" value="Закрыть">'+
'</div>';

}