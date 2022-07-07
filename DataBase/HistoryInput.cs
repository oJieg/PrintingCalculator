namespace printing_calculator.DataBase
{
    public class HistoryInput
    {
        public int Id { get; set; }
        public int Height { get; set; }
        public int Whidth { get; set; }
        public PaperCatalog Paper { get; set; }
        public int Amount { get; set; }
        public int Kinds { get; set; }
        public bool Duplex { get; set; }
    }
}
