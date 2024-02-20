using ProductConvertible = printing_calculator.DataBase.crm.Product;
using СalculationHistory = printing_calculator.DataBase.СalculationHistory;

namespace printing_calculator.ModelOut.crm
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public СalculationHistory[] Histories { get; set; }
        public int ActivecalculationHistoryId { get; set; }
        public int? Price { get; set; }

        public static explicit operator Product(ProductConvertible product)
        {
            return new Product()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Histories = product.Histories.ToArray(),
                ActivecalculationHistoryId = product.ActivecalculationHistoryId,
                Price = product.Price
            };
        }
        public static explicit operator ProductConvertible(Product product)
        {
            return new ProductConvertible()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Histories = product.Histories.ToList(),
                ActivecalculationHistoryId = product.ActivecalculationHistoryId,
                Price = product.Price
            };
        }
    }
}
