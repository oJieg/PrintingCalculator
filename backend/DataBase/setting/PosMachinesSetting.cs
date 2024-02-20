namespace printing_calculator.DataBase.setting
{
    public class PosMachinesSetting :MachineSetting
    {
        /// <summary>
        /// количество листов в одной привертке при резке
        /// </summary>
        public int CountOfPapersInOneAdjustmentCut { get; set; }

        /// <summary>
        /// цена за доп удар по тому же изделию 
        /// (2 биговки на листе или 3 дырки на листе)
        /// </summary>
        public float AddMoreHit { get; set; }
    }
}
