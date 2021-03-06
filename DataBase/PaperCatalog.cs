using System.ComponentModel.DataAnnotations.Schema;
namespace printing_calculator.DataBase
{
    public class PaperCatalog
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SizePaper Size { get; set; }

        public List<PricePaper> Prices { get; set; } =  new();
    }
}
