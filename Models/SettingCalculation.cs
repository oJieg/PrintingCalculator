using printing_calculator.DataBase;
using printing_calculator.DataBase.setting;
using printing_calculator.ViewModels;

namespace printing_calculator.Models
{
    public class SettingCalculation : ISettingCalculation
    {
        public PaperCatalog[] PaperCatalog { get; set; } 
        public SizePaper[] PaperSizes { get; set; }
        public Lamination[] Laminations { get; set; }
        public PrintingMachineSetting PrintingsMachine { get; set; }
        public PosMachinesSetting[] PosMachines { get; set; }
        public CommonToAllMarkup[]  CommonToAllMarkups { get; set; }
        public SpringBrochureSetting[] SpringBrochureSettings { get; set; }
    }
}
