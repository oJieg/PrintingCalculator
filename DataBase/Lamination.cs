namespace printing_calculator.DataBase
{
    public class Lamination
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<LaminationPrice> Price { get; set; } = null!;
    }
}