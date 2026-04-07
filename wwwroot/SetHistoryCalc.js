// SetHistoryCalc.js
(function () {
    // Функция заполнения формы данными пресета
    const CONFIG = {
        containerSelector: '#presetButtonsContainer',   // Селектор контейнера для кнопок
        autoCreateContainer: true,                      // Создавать контейнер, если его нет
        containerId: 'presetButtonsContainer',          // ID создаваемого контейнера
        buttonClass: 'btn btn-sm btn-outline-secondary preset-btn',
        titleHtml: '<h5>Быстрая загрузка расчётов</h5>'
    };

    function fillForm(preset) {
        if (!preset) return;

        // Основные поля
        if (preset.height !== undefined) $('#Height').val(preset.height);
        if (preset.width !== undefined) $('#Width').val(preset.width);
        if (preset.paper !== undefined) $('#Paper').val(preset.paper);
        if (preset.amount !== undefined) $('#Amount').val(preset.amount);
        if (preset.kinds !== undefined) $('#Kinds').val(preset.kinds);
        if (preset.duplex !== undefined) $('#Duplex').prop('checked', preset.duplex);
        if (preset.laminationName !== undefined) $('#LaminationName').val(preset.laminationName);
        if (preset.creasing !== undefined) $('#Creasing').val(preset.creasing);
        if (preset.drilling !== undefined) $('#Drilling').val(preset.drilling);
        if (preset.rounding !== undefined) $('#Rounding').prop('checked', preset.rounding);

        // Радиокнопки CommonToAllMarkup
        if (preset.commonToAllMarkup && preset.commonToAllMarkup.length) {
            $('input[name="CommonToAllMarkup"]').each(function () {
                $(this).prop('checked', preset.commonToAllMarkup.includes($(this).val()));
            });
        }

        // Тип брошюры
        if (preset.brochureType) {
            $('#Brochure').val(preset.brochureType);
            if (typeof window.brochure === 'function') window.brochure();
        }

        // Пружина
        if (preset.brochureType === 'spring') {
            if (preset.polos !== undefined) $('#Polos').val(preset.polos);
            if (preset.brochureAmout !== undefined) $('#BrochureAmout').val(preset.brochureAmout);
            if (preset.springBrochureType) $('#BrochureSpring').val(preset.springBrochureType);
            if (typeof window.editPolos === 'function') window.editPolos();
        }

        // Скрепка
        if (preset.brochureType === 'staple') {
            if (preset.heightStaple !== undefined) {
                $('#HeightBrochureStaple').val(preset.heightStaple);
                window.heightStaple = preset.heightStaple;
            }
            if (preset.widthStaple !== undefined) {
                $('#WidthBrochureStaple').val(preset.widthStaple);
                window.widthStaple = preset.widthStaple;
            }
            if (preset.polosStaple !== undefined) $('#PolosStaple').val(preset.polosStaple);
            if (preset.brochureAmoutStaple !== undefined) $('#BrochureAmoutStaple').val(preset.brochureAmoutStaple);
            if (typeof window.editPolosStaple === 'function') window.editPolosStaple();
            if (typeof window.calkSizeStaple === 'function') window.calkSizeStaple();
        }

        // Обновление визуальных состояний (подсветка быстрых размеров)
        if (typeof window.activeButtonSize === 'function') window.activeButtonSize();
        if (typeof window.activeButtonSizeStaple === 'function') window.activeButtonSizeStaple();

        // Синхронизация глобальных переменных (если используются)
        if (preset.height !== undefined) window.height = preset.height;
        if (preset.width !== undefined) window.width = preset.width;
    }

    // Заполнение по id пресета
    window.fillFormByPresetId = function (id) {
        if (!window.presets || !window.presets.length) {
            console.warn('Массив window.presets не определён или пуст');
            return;
        }
        var preset = window.presets.find(function (p) { return p.id == id; });
        if (preset) {
            fillForm(preset);
            console.log('Пресет загружен:', preset.name);
        } else {
            console.warn('Пресет с id =', id, 'не найден');
        }
    };

    // Автоматически находим все кнопки с data-preset-id и навешиваем обработчики
    $(document).ready(function () {
        $('[data-preset-id]').each(function () {
            var $btn = $(this);
            // Чтобы не вешать несколько раз, удалим старые обработчики (если были)
            $btn.off('click.preset');
            $btn.on('click.preset', function () {
                var id = $(this).data('preset-id');
                window.fillFormByPresetId(id);
            });
        });
        console.log('Обработчики для кнопок пресетов установлены');
    });

    function generateButtons() {
        if (!window.presets || !window.presets.length) return;

        // Получаем или создаём контейнер
        let $container = $(CONFIG.containerSelector);
        console.log($container)
        if (!$container.length) {
            if (CONFIG.autoCreateContainer) {
                $container = $('<div>', {
                    id: CONFIG.containerId,
                    class: 'preset-buttons-container'
                });
                // Вставляем контейнер перед формой (можно изменить логику)
                // Для примера: вставляем после элемента с классом 'FastSizeBlock' или в начало body
                const $target = $('.FastSizeBlock').first();
                if ($target.length) {
                    $target.after($container);
                } else {
                    $('body').prepend($container);
                }
            } else {
                console.warn('Контейнер для кнопок пресетов не найден');
                return;
            }
        }

        // Очищаем контейнер, но оставляем заголовок (если он уже был создан)
        const hasTitle = $container.children('.preset-title').length;
        $container.empty();
        if (!hasTitle && CONFIG.titleHtml) {
            $container.append($('<div>').addClass('preset-title').html(CONFIG.titleHtml));
        }

        // Создаём кнопки
        window.presets.forEach(function (preset) {
            const button = $('<button>', {
                type: 'button',
                class: CONFIG.buttonClass,
                'data-preset-id': preset.id,
                text: preset.name || ('Пресет ' + preset.id)
            });
            $container.append(button);
        });

        // Навешиваем обработчики на новые кнопки
        $container.find('[data-preset-id]').off('click.preset').on('click.preset', function () {
            const id = $(this).data('preset-id');
            window.fillFormByPresetId(id);
        });

        console.log('Сгенерировано кнопок:', window.presets.length);
    }

    // Инициализация: если presets уже определён – генерируем кнопки, иначе ждём
    function init() {
        if (window.presets && window.presets.length) {
            generateButtons();
        } else {
            // Ждём, когда window.presets появится (можно использовать MutationObserver или просто подождать)
            const checkInterval = setInterval(function () {
                if (window.presets && window.presets.length) {
                    clearInterval(checkInterval);
                    generateButtons();
                }
            }, 100);
            // Таймаут на случай, если presets никогда не появится
            setTimeout(function () {
                clearInterval(checkInterval);
                if (!window.presets || !window.presets.length) {
                    console.warn('window.presets не определён в течение 5 секунд');
                }
            }, 5000);
        }
    }

    $(document).ready(init);

    window.setFieldCrm = async function (input) {
        let resultFull = {
            inputs: window.presets??[],  // Обратите внимание: в оригинале дважды пушится один input
            DealId: document.getElementById("DealId").value,
            Token: document.getElementById("Token").value
        };
        resultFull.inputs.push(input);

        try {
            let response = await fetch('/api/set-field-crm', {
                method: "Put",
                headers: { "Accept": "application/json", "Content-Type": "application/json" },
                body: JSON.stringify(resultFull)
            });
            if (!response.ok) {
                console.error('Ошибка отправки в CRM:', response.status);
            }
            let responseData = await response.json();


            window.presets = responseData.map(function (input, idx) {
                return {
                    id: input.id || (idx + 1),
                    name: input.name || ('Пресет ' + (idx + 1)),
                    // Тип брошюры
                    brochureType: input.stapleBrochure ? 'staple' : (input.springBrochure ? 'spring' : 'none'),
                    // Основные поля
                    height: input.height,
                    whidth: input.whidth,        // внимание: в исходном Input поле называется width
                    paper: input.paper,
                    amount: input.amount,
                    kinds: input.kinds,
                    duplex: input.duplex,
                    laminationName: input.laminationName,
                    creasing: input.creasing,
                    drilling: input.drilling,
                    rounding: input.rounding,
                    commonToAllMarkup: input.commonToAllMarkup || [],
                    // Поля для пружины (если есть)
                    polos: input.polos,
                    brochureAmout: input.amount,
                    springBrochureType: (input.springBrochure === 1 ? 'noCover' : (input.springBrochure === 2 ? 'CoverPlasticAndCardboard' : (input.springBrochure === 3 ? 'CoverTwoPlastics' : null))),
                    // Поля для скрепки (если есть)
                    heightStaple: input.heightStaple,
                    widthStaple: input.widthStaple,
                    polosStaple: input.polosStaple,
                    brochureAmoutStaple: input.brochureAmoutStaple
                };
            });
            console.log(window.presets);
            console.log(responseData);

            generateButtons();
        } catch (error) {
            console.error('Ошибка при вызове setFieldCrm:', error);
        }
    };

})();

