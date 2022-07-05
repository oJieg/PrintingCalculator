namespace printing_calculator.DataBase
{
    public class PricePaper
    {
        public int Id { get; set; }
        public float Price { get; set; }
        public PaperCatalog? Catalog { get; set; }
    }
}
