namespace printing_calculator.DataBase.setting
{
    public class PrintingMachineSetting : MachineSetting
    {
        /// <summary>
        /// не печатные поля ширина
        /// </summary>
        public float WhiteFieldWidth { get; set; }

        /// <summary>
        /// не печатные поля высота
        /// </summary>
        public float WhiteFieldHeight { get; set;}

        /// <summary>
        /// максимальная длинна листа
        /// </summary>
        public int MaximumSizeLength { get; set; }

        /// <summary>
        /// максимальная ширина листа
        /// </summary>
        public int MaximumSizeWidth { get; set;}

        /// <summary>
        /// дистанция расположения меток от всех изделий
        /// </summary>
        public int FieldForLabels { get; set; }

        /// <summary>
        /// растояние между изделиями при раскладки
        /// </summary>
        public int Bleed { get; set; }

        /// <summary>
        /// количество отпечатков для  на полном комплетке красок
        /// </summary>
        public int ConsumableDye { get; set; }

        /// <summary>
        /// количество отпечатков для основного печатующего расходника(фотоборабаны(х4) или голова)
        /// </summary>
        public int MainConsumableForDrawing { get; set; } 


    }
}