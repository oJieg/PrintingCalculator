using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using printing_calculator.DataBase.crm;
using printing_calculator.ViewModels;
using printing_calculator.DataBase;

using OutProduct = printing_calculator.ModelOut.crm.Product;

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

		[HttpGet("api/product/get-product{idProduction}")]
		public async Task<Answer<OutProduct>> GetProduct(int idProduction)
		{
			try
			{
				Product product = await _applicationContext.Products
					.Include(x => x.Histories)
					.FirstAsync(x => x.Id == idProduction);
				return new Answer<OutProduct>() { Result =(OutProduct)product };
			}
			catch (InvalidOperationException)
			{
				return new Answer<OutProduct>() { Status = StatusAnswer.NotFound, ErrorMassage = "Не найден Product с таким ID" };
			}
			catch (Exception ex)
			{
				return new Answer<OutProduct>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
			}
		}

		[HttpGet("api/product/add-new-product")]
		public async Task<Answer<OutProduct>> AddNewProduct(string? name, string? description)
		{
			try
			{
				Product product = new() { Name = name, Description = description };
				_applicationContext.Products.Add(product);
				await _applicationContext.SaveChangesAsync();

				return new Answer<OutProduct>() { Result = (OutProduct)product };
			}
			catch (Exception ex)
			{
				return new Answer<OutProduct>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
			}
		}

		[HttpGet("api/product/add-history{productId}")]
		public async Task<Answer<OutProduct>> AddHistory(int productId, int histiryId)
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

				return new Answer<OutProduct>() { Result = (OutProduct)product };
			}
			catch (InvalidOperationException)
			{
				return new Answer<OutProduct>() { Status = StatusAnswer.NotFound, ErrorMassage = "Не найден Product с таким ID" };
			}
			catch (Exception ex)
			{
				return new Answer<OutProduct>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
			}
		}

		[HttpGet("api/product/edit-active-history")]
		public async Task<Answer<OutProduct>> EditActiveHistory(int productId, int historyId)
		{
			try
			{
				Product product = await _applicationContext.Products
					.Include(x => x.Histories)
					.FirstAsync(x => x.Id == productId);

				СalculationHistory history = await _applicationContext.Histories.FirstAsync(x => x.Id == historyId);
				if (!product.Histories.Any(x => x.Id == historyId))
				{
					return new Answer<OutProduct>() { Status = StatusAnswer.NotFound, ErrorMassage = "Не найден histiryId с таким ID" };
				}

				product.ActivecalculationHistoryId = historyId;
				product.Price = history.Price;

				await _applicationContext.SaveChangesAsync();

				return new Answer<OutProduct>() { Result = (OutProduct)product };
			}
			catch (InvalidOperationException)
			{
				return new Answer<OutProduct>() { Status = StatusAnswer.NotFound, ErrorMassage = "Не найден Product с таким ID" };
			}
			catch (Exception ex)
			{
				return new Answer<OutProduct>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
			}
		}
		[HttpGet("api/product/edit-product")]
		public async Task<Answer<OutProduct>> Editproduct(int productId, string? name , string? description )
		{
			try
		{
				Product product = await _applicationContext.Products
						.FirstAsync(x => x.Id == productId);

				product.Name = name;
				product.Description = description;

				await _applicationContext.SaveChangesAsync();

				return new Answer<OutProduct>() { Result = (OutProduct)product };
			}
			catch (InvalidOperationException)
			{
				return new Answer<OutProduct>() { Status = StatusAnswer.NotFound, ErrorMassage = "Не найден Product с таким ID" };
			}
			catch (Exception ex)
			{
				return new Answer<OutProduct>() { Status = StatusAnswer.ErrorDataBase, ErrorMassage = ex.Message };
			}
		} 
	}
}