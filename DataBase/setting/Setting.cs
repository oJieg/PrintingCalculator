namespace printing_calculator.DataBase.setting
{
    public class Setting
    {
        public int Id { get; set; }
        public List<PrintingMachineSetting> PrintingsMachines { get; set; }
        public List<MachineSetting> Machines { get; set; }
        public List<PosMachinesSetting> PosMachines { get; set;}
    }
}
