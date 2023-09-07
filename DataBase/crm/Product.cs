namespace printing_calculator.DataBase.crm
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<СalculationHistory> Histories { get; set; }
    }
}
