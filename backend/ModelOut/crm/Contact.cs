using ContactConvertible = printing_calculator.DataBase.crm.Contact;
using Mail = printing_calculator.DataBase.crm.Mail;
using PhoneNumber = printing_calculator.DataBase.crm.PhoneNumber;

namespace printing_calculator.ModelOut.crm
{
    public class Contact
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Mail[] Mails { get; set; } = Array.Empty<Mail>();
        public PhoneNumber[]? PhoneNumbers { get; set; }
        public int[] Orders { get; set; } = Array.Empty<int>();

        public static explicit operator Contact(ContactConvertible сontact)
        {
            int[] order = сontact.Orders.Select(x=>x.Id).ToArray();

            Mail[] mail = Array.Empty<Mail>();
            if (сontact.Mails != null)
            {
                mail = сontact.Mails.ToArray();
            }

            PhoneNumber[] phone = Array.Empty<PhoneNumber>();
            if(сontact.PhoneNumbers!= null)
            {
                phone = сontact.PhoneNumbers.ToArray();
            }

            return new Contact()
            {
                Id = сontact.Id,
                Description = сontact.Description,
                Name = сontact.Name,
                Mails = mail,
                PhoneNumbers = phone,
                Orders = order
            };
        }

        public static explicit operator ContactConvertible(Contact сontact)
        {
            return new ContactConvertible()
            {
                Id = сontact.Id,
                Description = сontact.Description,
                Name = сontact.Name,
                Mails = сontact.Mails.ToList(),
                PhoneNumbers = сontact.PhoneNumbers.ToList()
        };
        }
    }
}
