namespace printing_calculator.Models.Settings
{
    public class SettingPrinter
    {
        public int WhiteFieldWidth { get; set; }
        public int WhiteFieldHeight { get; set; }
        public int FieldForLabels { get; set; }
        public float Bleed { get; set; }
        public int MaximumSize { get; set; }
    }
}