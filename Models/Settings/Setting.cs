using printing_calculator.Models.Settings;

namespace printing_calculator.Models
{
    public class Setting
    {
        public MarkupPaper MarkupPaper { get; set; } = null!;
        public SettingPrinter SettingPrinter { get; set; } = null!;
        public CutSetting CutSetting { get; set; } = null!;
        public Consumable Consumable { get; set; } = null!;
        public Lamination Lamination { get; set; } = null!;
        public Pos Pos { get; set; } = null!;
    }
}