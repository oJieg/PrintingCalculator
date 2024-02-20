namespace printing_calculator.DataBase.setting
{
    public class PrintingMachineSetting : MachineSetting
    {
        /// <summary>
        /// не печатные поля по короткой стороне
        /// </summary>
        public float WhiteFieldWidth { get; set; }

        /// <summary>
        /// не печатные поля по длинной стороне
        /// </summary>
        public float WhiteFieldHeight { get; set;}

        /// <summary>
        /// максимальная длинна листа(по длинной стороне)
        /// </summary>
        public int MaximumSizeLength { get; set; }

        /// <summary>
        /// максимальная ширина листа(по короткой стороне)
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
        /// количество отпечатков для основного печатующего расходника(фотоборабаны(х4))
        /// </summary>
        public int MainConsumableForDrawing { get; set; } 


    }
}