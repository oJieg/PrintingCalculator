using printing_calculator.DataBase;

namespace printing_calculator.ViewModels
{
    public class PaperAndHistoryInput
    {
        public List<PaperCatalog> Paper { get; set; }
        public List<Lamination> Lamination { get; set; }
        public HistoryInput? Input { get; set; }
    }
}