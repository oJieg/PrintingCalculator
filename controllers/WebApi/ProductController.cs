using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using printing_calculator.DataBase.crm;
using printing_calculator.ViewModels;
using printing_calculator.DataBase;

namespace printing_calculator.controllers.WebApi
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationContext _applicationContext;

        public ProductController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        [HttpGet("api/product/add-new-product")]
        public async Task<Answer<Product>> AddNewProduct(string? name, string? description)
        {
            try
            {
                Product product = new() { Name = name, Description = description};
                _applicationContext.Products.Add(product);
                await _applicationContext.SaveChangesAsync();

                return new Answer<Product>() { Result = product };
            }
            catch (Exception ex)
            {
                return new Answer<Product>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
            }
        }

        [HttpGet("api/product/add-history{productId}")]
        public async Task<Answer<Product>> AddHistory(int productId, int histiryId)
        {
            try
            {
                Product product = await _applicationContext.Products
                    .FirstAsync(x => x.Id == productId);
                СalculationHistory history = await _applicationContext.Histories.FirstAsync(x => x.Id == histiryId);

                if (product.Histories == null)
                    product.Histories = new();

                product.Histories.Add(history);

                product.ActivecalculationHistoryId = histiryId;
                product.Price = history.Price;

                await _applicationContext.SaveChangesAsync();

                return new Answer<Product>() { Result = product };
            }
            catch (InvalidOperationException)
            {
                return new Answer<Product>() { Status = StatusAnswer.NotFaund, ErrorMassage = "Не найден Product с таким ID" };
            }
            catch (Exception ex)
            {
                return new Answer<Product>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
            }
        }

        [HttpGet("api/product/edit-active-history{productId}")]
        public async Task<Answer<Product>> EditActiveHistory(int productId, int histiryId)
        {
            try
            {
                Product product = await _applicationContext.Products
                    .Include(x=>x.Histories)
                    .FirstAsync(x => x.Id == productId);

                СalculationHistory history = await _applicationContext.Histories.FirstAsync(x => x.Id != histiryId);
                if (product.Histories.Any(x=>x.Id == histiryId))
                {
                    return new Answer<Product>() { Status = StatusAnswer.NotFaund, ErrorMassage = "Не найден histiryId с таким ID" };
                }

                product.ActivecalculationHistoryId=histiryId;
                product.Price = history.Price;

                await _applicationContext.SaveChangesAsync();

                return new Answer<Product>() { Result= product };
            }
            catch (InvalidOperationException)
            {
                return new Answer<Product>() { Status = StatusAnswer.NotFaund, ErrorMassage = "Не найден Product с таким ID" };
            }
            catch (Exception ex)
            {
                return new Answer<Product>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
            }
        }
    }
}