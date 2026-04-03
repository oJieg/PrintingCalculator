namespace printing_calculator.Clients.DTO
{
    public class CrmDealUpdate
    {
        public int entityTypeId {  get; set; }
        public int Id { get; set; }
        public FieldsDetailUpdate Fields { get; set; }
        public string? auth {  get; set; }
    }
}
