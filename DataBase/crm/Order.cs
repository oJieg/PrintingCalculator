namespace printing_calculator.DataBase.crm
{
    public class Order
    {
        public int Id { get; set; }
        public List<Contact> Contacts { get; set; } = new List<Contact>();
        public string? Description { get; set; }
        public List<Product> Products { get; set; }
        public DateTime DateTime { get; set; }
        public StatusOrder status { get; set; }

    }
    public enum StatusOrder
    {
        NotAgreed = 0,
        AtWork = 1,
        Canceled = 2,
        Done = 3
    }
}
