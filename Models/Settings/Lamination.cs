namespace printing_calculator.Models.Settings
{
    public class Lamination
    {
        public List<Markup> Markups { get; set; } = null!;
        public int Adjustment { get; set; }
        public int Job { get; set; }
    }
}