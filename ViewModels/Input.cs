using printing_calculator.DataBase;

namespace printing_calculator.ViewModels
{
    public class Input
    {
        public int Height { get; set; }
        public int Whidth { get; set; }
        public string Paper { get; set; } = null!;
        public int Amount { get; set; }
        public int Kinds { get; set; }
        public bool Duplex { get; set; }
        
        public string? LaminationName { get; set; }

        public int Creasing { get; set; }
        public int Drilling { get; set; }
        public bool Rounding { get; set; }
        public List<string> CommonToAllMarkup { get; set; }

		public SpringBrochure SpringBrochure { get; set; }
		public bool StapleBrochure { get; set; }

		public bool NoSaveDB { get; set; }
    }

}