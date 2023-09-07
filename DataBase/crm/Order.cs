namespace printing_calculator.DataBase.crm
{
    public class Order
    {
        public int Id { get; set; }
        public List<Contact> Contacts { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; }

    }
}
