using printing_calculator.DataBase;
using printing_calculator.ViewModels;
namespace printing_calculator.ViewModels
{
    public class PaperAndHistoryInput
    {
        public List<PaperCatalog> Paper { get; set; }
        public HistoryInput? Input { get; set; }
    }
}
