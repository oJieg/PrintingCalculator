using printing_calculator.Models.Settings;

namespace printing_calculator.Models
{
    public class Setting
    {
        public MarkupPaper MarkupPaper { get; set; }
        public SettingPrinter SettingPrinter { get; set; }
        public CutSetting CutSetting { get; set; }
        public Consumable Consumable { get; set; }
        public Lamination Lamination { get; set; }
        public Pos Pos { get; set; }
    }
}
