

namespace printing_calculator.ViewModels
{
    public class Input
    {
        public int Height { get; set; }
        public int Whidth { get; set; }
        public string Paper { get; set; }
        public int Amount { get; set; }
        public int Kinds { get; set; }
        public bool Duplex { get; set; }
        
        public string? LaminationName { get; set; }

        public int Creasing { get; set; }
        public int Drilling { get; set; }
        public bool Rounding { get; set; }

        public bool SaveDB { get; set; }
    }
}
