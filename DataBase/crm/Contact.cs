﻿namespace printing_calculator.DataBase.crm
{
    public class Contact
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<Mail>? Mails { get; set; }
        public List<PhoneNumber>? PhoneNmbers { get; set;}
        public List<Order> Orders { get; set;} = new List<Order>();
    }
}
