namespace printing_calculator.DataBase
{
    public class PaperPrice
    {
        public int Id { get; set; }
        public float Price { get; set; }
        public PaperCatalog Catalog { get; set; } = null!;
    }
}