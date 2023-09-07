namespace printing_calculator.DataBase.crm
{
    public class PhoneNmber
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
