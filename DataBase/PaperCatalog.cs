using System.ComponentModel.DataAnnotations.Schema;
namespace printing_calculator.DataBase
{
    public class PaperCatalog
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public SizePaper Size { get; set; } = null!;
        public List<PaperPrice> Prices { get; set; } = new();
    }
}