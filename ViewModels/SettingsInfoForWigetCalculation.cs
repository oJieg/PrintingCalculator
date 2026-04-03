using printing_calculator.DataBase;

namespace printing_calculator.ViewModels
{
    public class SettingsInfoForWigetCalculation:SettingsInfoForCalculation
    {
        public int DealId { get; set; }
        public string Token { get; set; }
        public InputForWiget[] Inputs { get; set; }
    }
}
