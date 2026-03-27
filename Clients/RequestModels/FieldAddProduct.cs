namespace printing_calculator.Clients.RequestModels
{
    public class FieldAddProduct
    {
        public int ownerId {  get; set; }
        public string ownerType { get; set; }
        public double price { get; set; }
    }
}
