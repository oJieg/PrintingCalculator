using OrderConvertible = printing_calculator.DataBase.crm.Order;
using ProductConvertible = printing_calculator.DataBase.crm.Product;
using StatusOrder = printing_calculator.DataBase.crm.StatusOrder;

namespace printing_calculator.ModelOut.crm
{
    public class Order
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public Product[] Products { get; set; }
        public DateTime DateTime { get; set; }
        public StatusOrder status { get; set; }

        public static explicit operator Order(OrderConvertible order)
        {
            int productCount = order.Products.Count;
            Product[] product = new Product[productCount];

            for (int i = 0; i < productCount; i++)
            {
                product[i] = (Product)order.Products[i];
            }

            return new Order()
            {
                Id = order.Id,
                Description = order.Description,
                DateTime = order.DateTime,
                status = order.status,
                Products = product
            };
        }

        public static explicit operator OrderConvertible(Order order)
        {
            int productCount = order.Products.Length;
            List< ProductConvertible> product = new(productCount);

            for (int i = 0; i < productCount; i++)
            {
                product.Add((ProductConvertible)order.Products[i]);
            }

            return new OrderConvertible()
            {
                Id = order.Id,
                Description = order.Description,
                DateTime = order.DateTime,
                status = order.status,
                Products = product
            };
        }
    }
}