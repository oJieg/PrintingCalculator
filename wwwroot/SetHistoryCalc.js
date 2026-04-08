// SetHistoryCalc.js
(function () {
    // Конфигурация
    const CONFIG = {
        containerSelector: '#presetButtonsContainer',
        autoCreateContainer: true,
        containerId: 'presetButtonsContainer',
        buttonClass: 'btn btn-sm btn-outline-secondary preset-btn',
        activeButtonClass: 'btn-primary', // класс для активной кнопки
        titleHtml: '<h5>Быстрая загрузка расчётов</h5>'
    };

    // Текущий выбранный ID пресета (-1 = не выбран)
    let currentPresetId = -1;

    // Функция подсветки активной кнопки
    function updateActiveButtonHighlight() {
        $(`.${CONFIG.buttonClass.split(' ').pop()}`).removeClass(CONFIG.activeButtonClass);
        if (currentPresetId !== -1) {
            $(`[data-preset-id="${currentPresetId}"]`).addClass(CONFIG.activeButtonClass);
        }
    }

    // Сброс выбранного пресета
    window.resetPresetActive = function () {
        currentPresetId = -1;
        updateActiveButtonHighlight();
        console.log('Выбор пресета сброшен');
    };

    // Получение текущего активного ID (для внешних нужд)
    window.getPresetActive = function () {
        return currentPresetId;
    };

    // Заполнение формы данными пресета
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

        // Обновление визуальных состояний
        if (typeof window.activeButtonSize === 'function') window.activeButtonSize();
        if (typeof window.activeButtonSizeStaple === 'function') window.activeButtonSizeStaple();

        // Синхронизация глобальных переменных
        if (preset.height !== undefined) window.height = preset.height;
        if (preset.width !== undefined) window.width = preset.width;
    }

    // Заполнение по id пресета с установкой активного
    window.fillFormByPresetId = function (id) {
        if (!window.presets || !window.presets.length) {
            console.warn('Массив window.presets не определён или пуст');
            return;
        }
        var preset = window.presets.find(function (p) { return p.id == id; });
        if (preset) {
            fillForm(preset);
            currentPresetId = id;
            updateActiveButtonHighlight();
            console.log('Пресет загружен:', preset.name, '(id:', id, ')');
        } else {
            console.warn('Пресет с id =', id, 'не найден');
        }
    };

    // Автоматическая установка обработчиков на существующие кнопки
    $(document).ready(function () {
        $('[data-preset-id]').each(function () {
            var $btn = $(this);
            $btn.off('click.preset');
            $btn.on('click.preset', function () {
                var id = $(this).data('preset-id');
                window.fillFormByPresetId(id);
            });
        });
        console.log('Обработчики для кнопок пресетов установлены');
    });

    // Генерация кнопок из window.presets
    function generateButtons() {
        if (!window.presets || !window.presets.length) {
            // Если пресетов нет, показываем сообщение или очищаем контейнер
            let $container = $(CONFIG.containerSelector);
            if ($container.length) {
                $container.empty();
                if (CONFIG.titleHtml) {
                    $container.append($('<div>').addClass('preset-title').html(CONFIG.titleHtml));
                }
                $container.append($('<div>').addClass('no-presets-msg').text('Нет сохранённых пресетов'));
            }
            return;
        }

        let $container = $(CONFIG.containerSelector);
        if (!$container.length) {
            if (CONFIG.autoCreateContainer) {
                $container = $('<div>', {
                    id: CONFIG.containerId,
                    class: 'preset-buttons-container'
                });
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

        // Очищаем контейнер
        $container.empty();
        if (CONFIG.titleHtml) {
            $container.append($('<div>').addClass('preset-title').html(CONFIG.titleHtml));
        }

        // Создаём обёртку для гибкого размещения
        const $buttonsWrapper = $('<div>').addClass('preset-buttons-wrapper');
        $container.append($buttonsWrapper);

        // Создаём кнопки пресетов с крестиком удаления
        window.presets.forEach(function (preset) {
            const $presetGroup = $('<div>').addClass('preset-group');

            // Основная кнопка загрузки
            const $loadBtn = $('<button>', {
                type: 'button',
                class: CONFIG.buttonClass,
                'data-preset-id': preset.id,
                text: preset.name || ('Пресет ' + preset.id)
            });

            // Кнопка удаления
            const $deleteBtn = $('<button>', {
                type: 'button',
                class: 'preset-delete-btn',
                html: '✕',
                title: 'Удалить пресет'
            });
            $deleteBtn.on('click', function (e) {
                e.stopPropagation(); // Чтобы не сработала загрузка пресета
                window.deletePreset(preset.id);
            });

            $presetGroup.append($loadBtn, $deleteBtn);
            $buttonsWrapper.append($presetGroup);
        });

        // Навешиваем обработчики загрузки на основные кнопки
        $buttonsWrapper.find(`.${CONFIG.buttonClass.split(' ').pop()}`).off('click.preset').on('click.preset', function () {
            const id = $(this).data('preset-id');
            window.fillFormByPresetId(id);
        });

        // Восстанавливаем подсветку активного пресета
        updateActiveButtonHighlight();
    }

    // Обновлённая функция отправки в CRM с поддержкой замены выбранного пресета
    window.setFieldCrm = async function (input) {
        if (!input) {
            console.error('Нет данных input для сохранения');
            return;
        }

        // Формируем массив для отправки на сервер
        let updatedInputs = [];
        const oldPresets = window.presets || [];

        if (currentPresetId !== -1) {
            // Ищем индекс пресета с текущим ID
            const indexToReplace = oldPresets.findIndex(p => p.id == currentPresetId);
            if (indexToReplace !== -1) {
                // Заменяем выбранный пресет новыми данными
                updatedInputs = [...oldPresets];
                // Сохраняем старый ID, чтобы сохранить идентификатор
                const newPreset = { ...input, id: oldPresets[indexToReplace].id };
                updatedInputs[indexToReplace] = newPreset;
                console.log(`Замена пресета id=${currentPresetId} на новые данные`);
            } else {
                // Если ID не найден (маловероятно) - добавляем в конец
                updatedInputs = [...oldPresets, input];
                console.warn(`Пресет с id=${currentPresetId} не найден, добавляем новый`);
                // Сбрасываем активный, так как замены не произошло
                currentPresetId = -1;
            }
        } else {
            // Нет активного пресета – добавляем новый в конец
            updatedInputs = [...oldPresets, input];
            console.log('Добавление нового пресета');
        }

        const resultFull = {
            inputs: updatedInputs,
            DealId: document.getElementById("DealId").value,
            Token: document.getElementById("Token").value
        };

        try {
            let response = await fetch('/api/set-field-crm', {
                method: "Put",
                headers: { "Accept": "application/json", "Content-Type": "application/json" },
                body: JSON.stringify(resultFull)
            });
            if (!response.ok) {
                console.error('Ошибка отправки в CRM:', response.status);
                return;
            }
            let responseData = await response.json();

            // Преобразуем ответ сервера в формат пресетов
            const newPresets = responseData.map(function (input, idx) {
                return {
                    id: input.id || (idx + 1),
                    name: input.name || ('Пресет ' + (idx + 1)),
                    brochureType: input.stapleBrochure ? 'staple' : (input.springBrochure ? 'spring' : 'none'),
                    height: input.height,
                    whidth: input.whidth,
                    paper: input.paper,
                    amount: input.amount,
                    kinds: input.kinds,
                    duplex: input.duplex,
                    laminationName: input.laminationName,
                    creasing: input.creasing,
                    drilling: input.drilling,
                    rounding: input.rounding,
                    commonToAllMarkup: input.commonToAllMarkup || [],
                    polos: input.polos,
                    brochureAmout: input.amount,
                    springBrochureType: (input.springBrochure === 1 ? 'noCover' : (input.springBrochure === 2 ? 'CoverPlasticAndCardboard' : (input.springBrochure === 3 ? 'CoverTwoPlastics' : null))),
                    heightStaple: input.heightStaple,
                    widthStaple: input.widthStaple,
                    polosStaple: input.polosStaple,
                    brochureAmoutStaple: input.brochureAmoutStaple,
                    price: input.price
                };
            });

            window.presets = newPresets;
            console.log('Обновлённые пресеты:', window.presets);

            // Перегенерируем кнопки
            generateButtons();

            // Проверяем, существует ли наш активный пресет в новом списке (по id)
            if (currentPresetId !== -1) {
                const stillExists = window.presets.some(p => p.id == currentPresetId);
                if (!stillExists) {
                    console.log(`Пресет id=${currentPresetId} больше не существует, сбрасываем выбор`);
                    currentPresetId = -1;
                    updateActiveButtonHighlight();
                } else {
                    // Обновляем подсветку (кнопки пересозданы)
                    updateActiveButtonHighlight();
                }
            }
        } catch (error) {
            console.error('Ошибка при вызове setFieldCrm:', error);
        }
    };

    // Инициализация: ожидаем window.presets
    function init() {
        if (window.presets && window.presets.length) {
            generateButtons();
        } else {
            const checkInterval = setInterval(function () {
                if (window.presets && window.presets.length) {
                    clearInterval(checkInterval);
                    generateButtons();
                }
            }, 100);
            setTimeout(function () {
                clearInterval(checkInterval);
                if (!window.presets || !window.presets.length) {
                    console.warn('window.presets не определён в течение 5 секунд');
                }
            }, 5000);
        }
    }



    window.deletePreset = async function (presetId) {
        if (!window.presets || !window.presets.length) return;

        // Проверяем, что пресетов больше одного
        if (window.presets.length <= 1) {
            alert('Нельзя удалить единственный пресет. Должен оставаться хотя бы один пресет.');
            console.warn('Попытка удалить последний пресет, операция отклонена');
            return;
        }

        // Находим индекс удаляемого пресета
        const index = window.presets.findIndex(p => p.id == presetId);
        if (index === -1) {
            console.warn('Пресет для удаления не найден:', presetId);
            return;
        }

        const presetName = window.presets[index].name;

        // Подтверждение удаления
        if (!confirm(`Удалить пресет "${presetName}"?`)) return;

        // Удаляем из массива
        const updatedPresets = [...window.presets];
        updatedPresets.splice(index, 1);

        // Если удаляемый пресет был активным, сбрасываем выбор
        if (currentPresetId == presetId) {
            currentPresetId = -1;
            updateActiveButtonHighlight();
        }

        // Отправляем обновлённый список на сервер
        const resultFull = {
            inputs: updatedPresets,
            DealId: document.getElementById("DealId").value,
            Token: document.getElementById("Token").value
        };

        try {
            let response = await fetch('/api/set-field-crm', {
                method: "Put",
                headers: { "Accept": "application/json", "Content-Type": "application/json" },
                body: JSON.stringify(resultFull)
            });
            if (!response.ok) {
                console.error('Ошибка удаления пресета в CRM:', response.status);
                alert('Не удалось удалить пресет. Ошибка сервера.');
                return;
            }
            let responseData = await response.json();

            // Обновляем window.presets из ответа сервера
            window.presets = responseData.map(function (input, idx) {
                return {
                    id: input.id || (idx + 1),
                    name: input.name || ('Пресет ' + (idx + 1)),
                    brochureType: input.stapleBrochure ? 'staple' : (input.springBrochure ? 'spring' : 'none'),
                    height: input.height,
                    width: input.whidth,
                    paper: input.paper,
                    amount: input.amount,
                    kinds: input.kinds,
                    duplex: input.duplex,
                    laminationName: input.laminationName,
                    creasing: input.creasing,
                    drilling: input.drilling,
                    rounding: input.rounding,
                    commonToAllMarkup: input.commonToAllMarkup || [],
                    polos: input.polos,
                    brochureAmout: input.amount,
                    springBrochureType: (input.springBrochure === 1 ? 'noCover' : (input.springBrochure === 2 ? 'CoverPlasticAndCardboard' : (input.springBrochure === 3 ? 'CoverTwoPlastics' : null))),
                    heightStaple: input.heightStaple,
                    widthStaple: input.widthStaple,
                    polosStaple: input.polosStaple,
                    brochureAmoutStaple: input.brochureAmoutStaple,
                    price: input.price
                };
            });

            // Перегенерируем кнопки
            generateButtons();
            console.log(`Пресет "${presetName}" удалён`);
        } catch (error) {
            console.error('Ошибка при удалении пресета:', error);
            alert('Произошла ошибка при удалении пресета.');
        }
    };


    document.getElementById('resetPresetBtn')?.addEventListener('click', function () {
        if (typeof window.resetPresetActive === 'function') {
            window.resetPresetActive();
            console.log('Выбор пресета сброшен по кнопке');
        } else {
            console.warn('window.resetPresetActive не определена');
        }
    });

    $(document).ready(init);
})();