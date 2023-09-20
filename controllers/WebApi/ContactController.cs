using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using printing_calculator.DataBase.crm;
using printing_calculator.ViewModels;
using System.Collections.Generic;

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

		[HttpGet("api/contact/add-new-contact")]
		public async Task<Answer<Contact>> AddNewOrder(string? name, string? eMail, string? phone)
		{
			try
			{
				Contact contact = new()
				{
					Name = name
				};
				Mail mail;
				PhoneNumber phoneNumber;

				if (eMail != null)
				{
					mail = new() { Email = eMail };
					_applicationContext.Mails.Add(mail);
					contact.Mails = new List<Mail>() { mail };
				}
				if (phone != null)
				{
					phoneNumber = new() { Number = phone };
					_applicationContext.PhoneNmbers.Add(phoneNumber);
					contact.PhoneNmbers = new List<PhoneNumber>() { phoneNumber };
				}

				_applicationContext.Contacts.Add(contact);
				await _applicationContext.SaveChangesAsync();
				return new Answer<Contact>() { Result = contact };
			}
			catch (Exception ex)
			{
				return new Answer<Contact>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
			}
		}
		[HttpGet("api/contact/edit-contact{contactId}")]
		public async Task<Answer<Contact>> AddPhone(int contactId, string name, string description)
		{
			try
			{
				Contact contact = await _applicationContext.Contacts
					.FirstAsync(x => x.Id == contactId);

				contact.Name = name;
				contact.Description = description;

				await _applicationContext.SaveChangesAsync();
				return new Answer<Contact>() { Result = contact };
			}
			catch (InvalidOperationException)
			{
				return new Answer<Contact>() { Status = StatusAnswer.NotFaund, ErrorMassage = "Не найден contact с таким ID" };
			}
			catch (Exception ex)
			{
				return new Answer<Contact>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
			}
		}

		[HttpGet("api/contact/add-phone{contactId}")]
		public async Task<Answer<Contact>> AddPhone(int contactId, string phone)
		{
			try
			{
				Contact contact = await _applicationContext.Contacts
					.Include(x => x.PhoneNmbers)
					.Include(x => x.Mails)
					.FirstAsync(x => x.Id == contactId);
				PhoneNumber phoneNumber = new() { Number = phone };

				contact.PhoneNmbers.Add(phoneNumber);

				_applicationContext.PhoneNmbers.Add(phoneNumber);
				await _applicationContext.SaveChangesAsync();
				return new Answer<Contact>() { Result = contact };
			}
			catch (InvalidOperationException)
			{
				return new Answer<Contact>() { Status = StatusAnswer.NotFaund, ErrorMassage = "Не найден contact с таким ID" };
			}
			catch (Exception ex)
			{
				return new Answer<Contact>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
			}
		}

		[HttpGet("api/contact/add-mail{contactId}")]
		public async Task<Answer<Contact>> AddMail(int contactId, string eMail)
		{
			try
			{
				Contact contact = await _applicationContext.Contacts
					.Include(x => x.Mails)
					.Include(x => x.PhoneNmbers)
					.FirstAsync(x => x.Id == contactId);
				Mail mail = new() { Email = eMail };

				contact.Mails.Add(mail);

				_applicationContext.Mails.Add(mail);
				await _applicationContext.SaveChangesAsync();
				return new Answer<Contact>() { Result = contact };
			}
			catch (InvalidOperationException)
			{
				return new Answer<Contact>() { Status = StatusAnswer.NotFaund, ErrorMassage = "Не найден contact с таким ID" };
			}
			catch (Exception ex)
			{
				return new Answer<Contact>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
			}
		}


		[HttpGet("api/contact/del-phone{contactId}")]
		public async Task<Answer<Contact>> DelPhone(int contactId, string phone)
		{
			try
			{
				Contact contact = await _applicationContext.Contacts
					.Include(x => x.PhoneNmbers)
					.Include(x => x.Mails)
					.FirstAsync(x => x.Id == contactId);
				//PhoneNumber phoneNumber = await _applicationContext.PhoneNmbers.FirstAsync(x=>x.Number== phone);
				//contact.PhoneNmbers[2].Number.Trim() == phone;
				contact.PhoneNmbers.Remove(contact.PhoneNmbers.First(x => x.Number == phone));

				//_applicationContext.PhoneNmbers.Remove(phoneNumber);
				await _applicationContext.SaveChangesAsync();
				return new Answer<Contact>() { Result = contact };
			}
			catch (InvalidOperationException)
			{
				return new Answer<Contact>() { Status = StatusAnswer.NotFaund, ErrorMassage = "Не найден contact с таким ID" };
			}
			catch (Exception ex)
			{
				return new Answer<Contact>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
			}
		}

		[HttpGet("api/contact/del-mail{contactId}")]
		public async Task<Answer<Contact>> DelMail(int contactId, string eMail)
		{
			try
			{
				Contact contact = await _applicationContext.Contacts
					.Include(x => x.Mails)
					.Include(x => x.PhoneNmbers)
					.FirstAsync(x => x.Id == contactId);

				contact.Mails.Remove(contact.Mails.First(x => x.Email.Replace(" ", "") == eMail));

				await _applicationContext.SaveChangesAsync();
				return new Answer<Contact>() { Result = contact };
			}
			catch (InvalidOperationException)
			{
				return new Answer<Contact>() { Status = StatusAnswer.NotFaund, ErrorMassage = "Не найден contact с таким ID" };
			}
			catch (Exception ex)
			{
				return new Answer<Contact>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
			}
		}



		[HttpGet("api/contact/get-contact{contactId}")]
		public async Task<Answer<Contact>> GetContact(int contactId)
		{
			try
			{
				Contact contact = await _applicationContext.Contacts
					.AsNoTracking()
					.Include(x => x.Mails)
					.Include(x => x.PhoneNmbers)
					.Include(x => x.Orders)
					.FirstAsync(x => x.Id == contactId);
				return new Answer<Contact>() { Result = contact };
			}
			catch (InvalidOperationException)
			{
				return new Answer<Contact>() { Status = StatusAnswer.NotFaund, ErrorMassage = "Не найден contact с таким ID" };
			}
			catch (Exception ex)
			{
				return new Answer<Contact>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
			}
		}

		[HttpGet("api/contact/get-first-list-contact")]
		public async Task<Answer<List<Contact>>> GetFirstListContact()
		{
			try
			{
				List<Contact> contact = await _applicationContext.Contacts
				   .AsNoTracking()
				   .Include(x => x.Mails)
				   .Include(x => x.PhoneNmbers)
				   .Take(10)
				   .ToListAsync();
				return new Answer<List<Contact>>() { Result = contact };
			}
			catch (InvalidOperationException)
			{
				return new Answer<List<Contact>>() { Status = StatusAnswer.NotFaund, ErrorMassage = "Не найден contact с таким ID" };
			}
			catch (Exception ex)
			{
				return new Answer<List<Contact>>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
			}
		}

		[HttpGet("api/contact/get-all-order-for-contact{contactId}")]
		public async Task<Answer<List<Order>>> GetAllOrderForContact(int contactId)
		{
			try
			{
				return new Answer<List<Order>>()
				{
					Result = await _applicationContext.Orders
					.AsNoTracking()
					.Where(x => x.Contacts.Any(x => x.Id == contactId))
					.ToListAsync()
				};
			}
			catch (InvalidOperationException)
			{
				return new Answer<List<Order>>() { Status = StatusAnswer.NotFaund, ErrorMassage = "Не найден contact с таким ID" };
			}
			catch (Exception ex)
			{
				return new Answer<List<Order>>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
			}
		}


		[HttpGet("api/contact/searth-contact-by-phone")]
		public async Task<Answer<List<Contact>>> SearthContactByPhone(string phone)
		{
			try
			{
				List<int>? contactIds = await _applicationContext.PhoneNmbers
					.Where(x => x.Number.Contains(phone))
					.Select(x => x.ContactId).ToListAsync();

				if (contactIds.Count == 0)
				{
					return new Answer<List<Contact>>() { Status = StatusAnswer.NotFaund };
				}

				List<Contact> contacts = await _applicationContext.Contacts
					.Include(x => x.Mails)
					.Include(x => x.PhoneNmbers)
					.Where(x => contactIds.Any(y => y == x.Id))
					.ToListAsync();
				return new Answer<List<Contact>>() { Result = contacts };

			}
			catch (InvalidOperationException)
			{
				return new Answer<List<Contact>>() { Status = StatusAnswer.NotFaund, ErrorMassage = "Не найден contact с таким ID" };
			}
			catch (Exception ex)
			{
				return new Answer<List<Contact>>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
			}
		}

		[HttpGet("api/contact/searth-contact-by-email")]
		public async Task<Answer<List<Contact>>> SearthContactByEMail(string email)
		{
			try
			{
				List<int>? contactIds = await _applicationContext.Mails
					.Where(x => x.Email.Contains(email))
					.Select(x => x.ContactId).ToListAsync();

				if (contactIds.Count == 0)
				{
					return new Answer<List<Contact>>() { Status = StatusAnswer.NotFaund };
				}

				List<Contact> contacts = await _applicationContext.Contacts
					.Include(x => x.Mails)
					.Include(x => x.PhoneNmbers)
					.Where(x => contactIds.Any(y => y == x.Id))
					.ToListAsync();
				return new Answer<List<Contact>>() { Result = contacts };

			}
			catch (InvalidOperationException)
			{
				return new Answer<List<Contact>>() { Status = StatusAnswer.NotFaund, ErrorMassage = "Не найден contact с таким ID" };
			}
			catch (Exception ex)
			{
				return new Answer<List<Contact>>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
			}
		}

		[HttpGet("api/contact/searth-contact-by-name")]
		public async Task<Answer<List<Contact>>> SearthContactByName(string name)
		{
			try
			{
				List<Contact>? contact = await _applicationContext.Contacts
					.Where(x => x.Name.Contains(name))
					.ToListAsync();

				if (contact.Count == 0)
				{
					return new Answer<List<Contact>>() { Status = StatusAnswer.NotFaund };
				}

				return new Answer<List<Contact>>() { Result = contact };

			}
			catch (InvalidOperationException)
			{
				return new Answer<List<Contact>>() { Status = StatusAnswer.NotFaund, ErrorMassage = "Не найден contact с таким ID" };
			}
			catch (Exception ex)
			{
				return new Answer<List<Contact>>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
			}
		}
	}
}