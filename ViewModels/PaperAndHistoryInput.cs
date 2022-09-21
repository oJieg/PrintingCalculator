using printing_calculator.DataBase;

namespace printing_calculator.ViewModels
{
    public class PaperAndHistoryInput
    {
        public List<PaperCatalog> Paper { get; set; } = null!;
        public List<Lamination> Lamination { get; set; } = null!;
        public InputHistory? Input { get; set; }
    }
}