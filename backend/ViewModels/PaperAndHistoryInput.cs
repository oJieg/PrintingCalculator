using printing_calculator.DataBase;
using printing_calculator.DataBase.setting;

namespace printing_calculator.ViewModels
{
    public class PaperAndHistoryInput
    {
        public List<PaperCatalog> Paper { get; set; } = null!;
        public List<Lamination> Lamination { get; set; } = null!;
        public List<CommonToAllMarkup> commonToAllMarkups { get; set; } = null!;
        public InputHistory? Input { get; set; }
        public int ProductId { get; set; }
        public int OpderId { get; set; }
    }
}