using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using printing_calculator.DataBase.crm;
using printing_calculator.Models.Calculating;
using printing_calculator.ViewModels;

namespace printing_calculator.controllers.WebApi
{
    [ApiController]
    public class OrderController : Controller
    {
        private readonly ApplicationContext _applicationContext;
        private readonly ILogger<CalculatorResultController> _logger;

        public OrderController(ApplicationContext applicationContext,
            ILogger<CalculatorResultController> loggerFactory)
        {
            _applicationContext = applicationContext;
            _logger = loggerFactory;
        }

        [HttpGet("api/order/add-new-order")]
        public async Task<Answer<int>> AddNewOrder()
        {
            Order order = new() { DateTime = DateTime.UtcNow, status = StatusOrder.Open };

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
            Order order;
            try
            {
                order = await _applicationContext.Orders
                    .Include(x => x.Products)
                    .Include(x => x.Contacts)
                        .ThenInclude(x => x.PhoneNmbers)
                    .Include(x => x.Contacts)
                        .ThenInclude(x => x.Mails)
                    .FirstAsync(x => x.Id == Id);
            }
            catch (InvalidOperationException)
            {
                return new Answer<Order>() { Status = StatusAnswer.NotFaund, ErrorMassage = "Не найден order с таким ID" };
            }
            return new Answer<Order>() { Status = StatusAnswer.Ok, Result = order };
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
                _logger.LogError($"не удалось изменить статус заказа: {Id}", ex);
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
                _logger.LogError($"не удалось изменить описание заказа: {Id}", ex);
                return new Answer<bool>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.ToString(), Result = false };
            }
        }

        //[HttpPost("api/order/add-new-contact{orderId}")]
        //public async Task<Answer<Contact>> AddNewContact(int orderId, string? name, string? eMail, string? phone)
        //{
        //    Contact contact = new()
        //    {
        //        Name = name
        //    };

        //    try
        //    {
        //        Mail mail;
        //        PhoneNumber phoneNumber;

        //        Order order = await _applicationContext.Orders
        //             .Include(x => x.Products)
        //             .Include(x => x.Contacts)
        //                 .ThenInclude(x => x.PhoneNmbers)
        //             .Include(x => x.Contacts)
        //                 .ThenInclude(x => x.Mails)
        //             .FirstAsync(x => x.Id == orderId);

        //        if (eMail != null)
        //        {
        //            mail = new() { Email = eMail };
        //            _applicationContext.Mails.Add(mail);
        //            contact.Mails = new List<Mail>() { mail };
        //        }
        //        if (phone != null)
        //        {
        //            phoneNumber = new() { Number = phone };
        //            _applicationContext.PhoneNmbers.Add(phoneNumber);
        //            contact.PhoneNmbers = new List<PhoneNumber>() { phoneNumber };
        //        }

        //        _applicationContext.Contacts.Add(contact);
        //        order.Contacts.Add(contact);
        //        await _applicationContext.SaveChangesAsync();
        //    }
        //    catch (InvalidOperationException)
        //    {
        //        return new Answer<Contact>() { Status = StatusAnswer.NotFaund, ErrorMassage = "Не найден order с таким ID" };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Answer<Contact>() { Status = StatusAnswer.Other, ErrorMassage = ex.Message };
        //    }
        //    return new Answer<Contact>() { Result = contact };
        //}

        [HttpPost("api/order/add-contact{orderId}")]
        public async Task<Answer<Contact>> AddContact(int orderId, int contactId)
        {
            try
            {
                Order order = await _applicationContext.Orders
                     .Include(x => x.Contacts)
                     .FirstAsync(x => x.Id == orderId);
                Contact contact = await _applicationContext.Contacts
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
