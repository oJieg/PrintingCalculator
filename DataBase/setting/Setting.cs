namespace printing_calculator.DataBase.setting
{
    public class Setting
    {
        public int Id { get; set; }
        public PrintingMachineSetting PrintingsMachines { get; set; }
        public MachineSetting[] Machines { get; set; }
        public PosMachinesSetting[] PosMachines { get; set;}
        public CommonToAllMarkup[] CommonToAllMarkups { get; set; }
    }
}
