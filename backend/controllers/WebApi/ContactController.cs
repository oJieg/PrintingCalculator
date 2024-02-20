using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using printing_calculator.DataBase.crm;
using printing_calculator.ViewModels;

using ConvetrotContactArrey = printing_calculator.ModelOut.crm.ArrayModelToOutModel;
using OutContact = printing_calculator.ModelOut.crm.Contact;
using OutOrder = printing_calculator.ModelOut.crm.Order;
using SearchContacts = printing_calculator.ModelOut.crm.SearchContacts;

namespace printing_calculator.controllers.WebApi
{
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ApplicationContext _applicationContext;

        public ContactController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        [HttpPost("api/contact/edit-contact")]
        public async Task<Answer<bool>> EditContact(OutContact newContact)
        {
            try
            {
                Contact contact = await _applicationContext.Contacts
                    .Include(x => x.Mails)
                    .Include(x => x.PhoneNumbers)
                    .FirstAsync(x => x.Id == newContact.Id);

                contact.Name = newContact.Name;
                contact.Description = newContact.Description;
                contact.PhoneNumbers = newContact.PhoneNumbers.ToList();
                contact.Mails = newContact.Mails.ToList();

                await _applicationContext.SaveChangesAsync();

                return new Answer<bool>() { Result = true };
            }
            catch
            {
                return new Answer<bool>() { Result = false };
            }
        }
        [HttpPost("api/contact/add-new-contact")]
        public async Task<Answer<bool>> AddNewContact(OutContact newContact)
        {
            try
            {
                _applicationContext.Contacts.Add((Contact)newContact);
                await _applicationContext.SaveChangesAsync();
                return new Answer<bool>() { Result = true };
            }
            catch (Exception ex)
            {
                return new Answer<bool>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message, Result = false };
            }
        }

        [HttpGet("api/contact/get-contact{contactId}")]
        public async Task<Answer<OutContact>> GetContact(int contactId)
        {
            try
            {
                Contact contact = await _applicationContext.Contacts
                    .AsNoTracking()
                    .Include(x => x.Mails)
                    .Include(x => x.PhoneNumbers)
                    .Include(x => x.Orders)
                    .FirstAsync(x => x.Id == contactId);
                return new Answer<OutContact>() { Result = (OutContact)contact };
            }
            catch (InvalidOperationException)
            {
                return new Answer<OutContact>() { Status = StatusAnswer.NotFound, ErrorMassage = "Не найден contact с таким ID" };
            }
            catch (Exception ex)
            {
                return new Answer<OutContact>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
            }
        }

        [HttpPost("api/contact/get-list-contact")]
        public async Task<Answer<OutContact[]>> GetListContact(int take, int skip)
        {
            try
            {
                Contact[] contacts = await _applicationContext.Contacts
                   .AsNoTracking()
                   .Include(x => x.Mails)
                   .Include(x => x.PhoneNumbers)
                   .OrderByDescending(x=>x.Id)
                   .Skip(skip)
                   .Take(take)
                   .ToArrayAsync<Contact>();

                return new Answer<OutContact[]>() { Result = ConvetrotContactArrey.ContactConvert(contacts) };
            }
            catch (InvalidOperationException)
            {
                return new Answer<OutContact[]>() { Status = StatusAnswer.NotFound, ErrorMassage = "" };
            }
            catch (Exception ex)
            {
                return new Answer<OutContact[]>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
            }
        }

        [HttpGet("api/contact/get-count-contact")]
        public async Task<Answer<int>> GetCountContact()
        {
            try
            {
                int countContact = await _applicationContext.Contacts
                   .CountAsync();

                return new Answer<int>() { Result = countContact };
            }
            catch (InvalidOperationException)
            {
                return new Answer<int>() { Status = StatusAnswer.NotFound, ErrorMassage = "Неудалось получить количество контактов" };
            }
            catch (Exception ex)
            {
                return new Answer<int>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
            }
        }

        [HttpGet("api/contact/get-all-order-for-contact{contactId}")]
        public async Task<Answer<OutOrder[]>> GetAllOrderForContact(int contactId)
        {
            try
            {
                return new Answer<OutOrder[]>()
                {
                    Result = ConvetrotContactArrey.OrderConvert(await _applicationContext.Orders
                    .AsNoTracking()
                    .Where(x => x.Contacts.Any(x => x.Id == contactId))
                    .ToArrayAsync<Order>())
                };
            }
            catch (InvalidOperationException)
            {
                return new Answer<OutOrder[]>() { Status = StatusAnswer.NotFound, ErrorMassage = "Не найден contact с таким ID" };
            }
            catch (Exception ex)
            {
                return new Answer<OutOrder[]>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
            }
        }


        [HttpPost("api/contact/searth-contact")]
        public async Task<Answer<SearchContacts>> SearthContact(string search, int take, int skip)
        {
            try
            {
                List<int>? contactIds = new();
                contactIds.AddRange(await _applicationContext.PhoneNumbers
                    .Where(x => x.Number.Contains(search))
                    .Select(x => x.ContactId).ToListAsync());
                contactIds.AddRange(await _applicationContext.Mails
                    .Where(x => x.Email.Contains(search))
                    .Select(x => x.ContactId).ToListAsync());

                Contact[]? contactSearch = await _applicationContext.Contacts
                .Where(x => x.Name.ToLower().Contains(search.ToLower()))
                    .ToArrayAsync<Contact>();

                if (contactIds.Count == 0 && contactSearch.Length==0)
                {
                    return new Answer<SearchContacts>() { Status = StatusAnswer.NotFound };
                }

                Contact[] contacts = await _applicationContext.Contacts
                    .Include(x => x.Mails)
                    .Include(x => x.PhoneNumbers)
                    .Where(x => contactIds.Any(y => y == x.Id))
                    .ToArrayAsync<Contact>();

                Contact[] outContacts = contactSearch.Concat(contacts).Skip(skip).Take(take).ToArray();

                SearchContacts outContsct = new() {
                    Contacts = ConvetrotContactArrey.ContactConvert(outContacts),
                    TotalCount = contacts.Length + contactSearch.Length
                };

                return new Answer<SearchContacts>() {

                    Result = outContsct
                };

            }
            catch (InvalidOperationException)
            {
                return new Answer<SearchContacts>() { Status = StatusAnswer.NotFound, ErrorMassage = "Не найден contact с таким ID" };
            }
            catch (Exception ex)
            {
                return new Answer<SearchContacts>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
            }
        }

        [HttpGet("api/contact/searth-contact-by-email")]
        public async Task<Answer<OutContact[]>> SearthContactByEMail(string email)
        {
            try
            {
                List<int>? contactIds = await _applicationContext.Mails
                    .Where(x => x.Email.Contains(email))
                    .Select(x => x.ContactId).ToListAsync();

                if (contactIds.Count == 0)
                {
                    return new Answer<OutContact[]>() { Status = StatusAnswer.NotFound };
                }

                Contact[] contacts = await _applicationContext.Contacts
                    .Include(x => x.Mails)
                    .Include(x => x.PhoneNumbers)
                    .Where(x => contactIds.Any(y => y == x.Id))
                    .ToArrayAsync<Contact>();

                return new Answer<OutContact[]>() { Result = ConvetrotContactArrey.ContactConvert(contacts) };

            }
            catch (InvalidOperationException)
            {
                return new Answer<OutContact[]>() { Status = StatusAnswer.NotFound, ErrorMassage = "Не найден contact с таким ID" };
            }
            catch (Exception ex)
            {
                return new Answer<OutContact[]>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
            }
        }

        [HttpGet("api/contact/searth-contact-by-name")]
        public async Task<Answer<OutContact[]>> SearthContactByName(string name)
        {
            try
            {
                Contact[] contact = await _applicationContext.Contacts
                    .Where(x => x.Name.Contains(name))
                    .ToArrayAsync<Contact>();

                if (contact.Length == 0)
                {
                    return new Answer<OutContact[]>() { Status = StatusAnswer.NotFound };
                }

                return new Answer<OutContact[]>() { Result = ConvetrotContactArrey.ContactConvert(contact) };

            }
            catch (InvalidOperationException)
            {
                return new Answer<OutContact[]>() { Status = StatusAnswer.NotFound, ErrorMassage = "Не найден contact с таким ID" };
            }
            catch (Exception ex)
            {
                return new Answer<OutContact[]>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
            }
        }
    }
}