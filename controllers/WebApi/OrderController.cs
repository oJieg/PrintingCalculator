using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using printing_calculator.DataBase.crm;
using printing_calculator.ViewModels;
using System.Globalization;

namespace printing_calculator.controllers.WebApi
{
    [ApiController]
    public class OrderController : Controller
    {
        private readonly ApplicationContext _applicationContext;


        public OrderController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        [HttpGet("api/order/add-new-order")]
        public async Task<Answer<int>> AddNewOrder()
        {
            Order order = new() { DateTime = DateTime.UtcNow, status = StatusOrder.NotAgreed };

            try
            {
                _applicationContext.Add(order);
                await _applicationContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new Answer<int>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
            }

            return new Answer<int> { Result = order.Id };
        }

        [HttpGet("api/order/get-order{Id}")]
        public async Task<Answer<Order>> GetOrder(int Id)
        {
            try
            {
                Order order = await _applicationContext.Orders
                    .AsNoTracking()
                    .Include(x => x.Products)
                        .ThenInclude(x => x.Histories)
                    .Include(x => x.Contacts)
                        .ThenInclude(x => x.PhoneNmbers)
                    .Include(x => x.Contacts)
                        .ThenInclude(x => x.Mails)
                    .FirstAsync(x => x.Id == Id);
                return new Answer<Order>() { Status = StatusAnswer.Ok, Result = order };
            }
            catch (InvalidOperationException)
            {
                return new Answer<Order>() { Status = StatusAnswer.NotFaund, ErrorMassage = "Не найден order с таким ID" };
            }

        }

		[HttpGet("api/order/get-order-data")]
		public async Task<Answer<List<Order>>> GetOrdersForData( 
           string data)
		{
			try
			{
                DateTime dateTime = DateTime.Parse(data, CultureInfo.CreateSpecificCulture("en-US"), DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal);
				List<Order> order = await _applicationContext.Orders
					 .AsNoTracking()
					 .Where(x => x.DateTime >= dateTime && x.DateTime <= dateTime.AddDays(1))
					 .Include(x => x.Products)
						 .ThenInclude(x => x.Histories)
					 .Include(x => x.Contacts)
						 .ThenInclude(x => x.PhoneNmbers)
					 .Include(x => x.Contacts)
						 .ThenInclude(x => x.Mails)
					 .OrderBy(x => x.DateTime)
					 .ToListAsync();
				return new Answer<List<Order>>() { Result = order };
			}
			catch (InvalidOperationException)
			{
				return new Answer<List<Order>>() { Status = StatusAnswer.NotFaund, ErrorMassage = "Не найден order с таким ID" };
			}
		}

		[HttpGet("api/order/get-list-order")]
        public async Task<Answer<List<Order>>> GetOpenOrders(StatusOrder statusOrder,int skip =0, int take=5)
        {
            try
            {
                List<Order> order = await _applicationContext.Orders
                     .AsNoTracking()
                     .Include(x => x.Products)
                         .ThenInclude(x => x.Histories)
                     .Include(x => x.Contacts)
                         .ThenInclude(x => x.PhoneNmbers)
                     .Include(x => x.Contacts)
                         .ThenInclude(x => x.Mails)
                     .Where(x => x.status == statusOrder)
                     .OrderByDescending(x=>x.DateTime)
                     .Skip(skip)
                     .Take(take)
                     .ToListAsync();
                return new Answer<List<Order>>() { Result = order };
            }
            catch (InvalidOperationException)
            {
                return new Answer<List<Order>>() { Status = StatusAnswer.NotFaund, ErrorMassage = "Не найден order с таким ID" };
            }
        }

        [HttpGet("api/order/get-list-close-order")]
        public async Task<Answer<List<Order>>> GetCloseOrders(int skip = 0, int take = 5)
        {
            try
            {
                List<Order> order = await _applicationContext.Orders
                     .AsNoTracking()
                     .Include(x => x.Products)
                         .ThenInclude(x => x.Histories)
                     .Include(x => x.Contacts)
                         .ThenInclude(x => x.PhoneNmbers)
                     .Include(x => x.Contacts)
                         .ThenInclude(x => x.Mails)
                     .Where(x => x.status == StatusOrder.Canceled || x.status == StatusOrder.Done)
                     .OrderByDescending(x => x.DateTime)
                     .Skip(skip)
                     .Take(take)
                     .ToListAsync();
                return new Answer<List<Order>>() { Result = order };
            }
            catch (InvalidOperationException)
            {
                return new Answer<List<Order>>() { Status = StatusAnswer.NotFaund, ErrorMassage = "Не найден order с таким ID" };
            }
        }

        [HttpGet("api/order/get-count-order")]
        public async Task<Answer<int>> GetCountOrder(StatusOrder statusOrder)
        {
            try
            {
                int countOrder = await _applicationContext.Orders.Where(x=>x.status == statusOrder).CountAsync();
                return new Answer<int>() { Result = countOrder };
            }
            catch (Exception ex)
            {
                return new Answer<int>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.ToString()};
            }
        }

        [HttpGet("api/order/edit-status-order{Id}")]
        public async Task<Answer<bool>> EditStatusOrder(int Id, StatusOrder status)
        {
            try
            {
                Order order = await _applicationContext.Orders
                   .FirstAsync(x => x.Id == Id);

                order.status = status;
                await _applicationContext.SaveChangesAsync();
            }
            catch (InvalidOperationException)
            {
                return new Answer<bool>() { Status = StatusAnswer.NotFaund, ErrorMassage = "Не найден order с таким ID" };
            }
            catch (Exception ex)
            {
                return new Answer<bool>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.ToString(), Result = false };
            }
            return new Answer<bool>() { Status = StatusAnswer.Ok, Result = true };
        }

        [HttpGet("api/order/edit-description-order{Id}")]
        public async Task<Answer<bool>> EditDescriptionOrder(int Id, string description)
        {
            try
            {
                Order order = await _applicationContext.Orders
                   .FirstAsync(x => x.Id == Id);

                order.Description = description;
                await _applicationContext.SaveChangesAsync();
                return new Answer<bool>() { Status = StatusAnswer.Ok, Result = true };
            }
            catch (InvalidOperationException)
            {
                return new Answer<bool>() { Status = StatusAnswer.NotFaund, ErrorMassage = "Не найден order с таким ID" };
            }
            catch (Exception ex)
            {
                return new Answer<bool>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.ToString(), Result = false };
            }
        }

        [HttpPost("api/order/add-contact{orderId}")]
        public async Task<Answer<Contact>> AddContact(int orderId, int contactId)
        {
            try
            {
                Order order = await _applicationContext.Orders
                     .Include(x => x.Contacts)
                     .FirstAsync(x => x.Id == orderId);
                Contact contact = await _applicationContext.Contacts
                    .Include(x => x.Mails)
                    .Include(x=>x.PhoneNmbers)
                    .FirstAsync(x => x.Id == contactId);

                order.Contacts.Add(contact);
                await _applicationContext.SaveChangesAsync();

                return new Answer<Contact>() { Result = contact };
            }
            catch
            {
                return new Answer<Contact>() { Status = StatusAnswer.NotFaund };
            }
        }

        [HttpDelete("api/order/del-contact{orderId}")]
        public async Task<Answer<bool>> DelContact(int orderId, int contactId)
        {
            try
            {
                Order order = await _applicationContext.Orders
                     .Include(x => x.Contacts)
                     .FirstAsync(x => x.Id == orderId);
                order.Contacts.Remove(order.Contacts.First(x => x.Id == contactId));

                await _applicationContext.SaveChangesAsync();
                return new Answer<bool> { Status = StatusAnswer.Ok, Result = true };
            }
            catch
            {
                return new Answer<bool>() { Status = StatusAnswer.Other, Result = false };
            }
        }

        [HttpPost("api/order/add-product{orderId}")]
        public async Task<Answer<Product>> AddProduct(int orderId, int productId)
        {
            try
            {
                Order order = await _applicationContext.Orders
                     .Include(x => x.Products)
                     .FirstAsync(x => x.Id == orderId);
                Product product = await _applicationContext.Products
                    .FirstAsync(x => x.Id == productId);

                order.Products.Add(product);
                await _applicationContext.SaveChangesAsync();
                return new Answer<Product> { Status = StatusAnswer.Ok, Result = product };
            }
            catch (InvalidOperationException)
            {
                return new Answer<Product>() { Status = StatusAnswer.NotFaund, ErrorMassage = "Не найден order или продукт с таким ID" };
            }
            catch (Exception ex)
            {
                return new Answer<Product>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.ToString() };
            }
        }

        [HttpDelete("api/order/del-product{orderId}")]
        public async Task<Answer<bool>> DelProduct(int orderId, int productId)
        {
            try
            {
                Order order = await _applicationContext.Orders
                     .Include(x => x.Products)
                     .FirstAsync(x => x.Id == orderId);
                order.Products.Remove(order.Products.First(x => x.Id == productId));

                await _applicationContext.SaveChangesAsync();
                return new Answer<bool> { Status = StatusAnswer.Ok, Result = true };
            }
            catch (InvalidOperationException)
            {
                return new Answer<bool>() { Status = StatusAnswer.NotFaund, ErrorMassage = "Не найден order или продукт с таким ID", Result = false };
            }
            catch (Exception ex)
            {
                return new Answer<bool>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.ToString(), Result = false };
            }
        }
    }
}