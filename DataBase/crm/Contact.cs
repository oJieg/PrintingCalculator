namespace printing_calculator.DataBase.crm
{
    public class Contact
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<Mail> Mails { get; set; }
        public List<PhoneNmber> PhoneNmbers { get; set;}
    }
}
