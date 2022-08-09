namespace printing_calculator.DataBase
{
    public class Lamination
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<LaminationPrice> Price { get; set; }
    }
}