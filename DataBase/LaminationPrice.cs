namespace printing_calculator.DataBase
{
    public class LaminationPrice
    {
        public int Id { get; set; }
        public float Price { get; set; }
        public Lamination? Lamination { get; set; }
    }
}