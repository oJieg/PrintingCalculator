namespace printing_calculator.Clients.AnswerModels
{
    public class GetFieldDealAnswer
    {
        public ResultGetFieldDeal Result { get; set; }
    }
    public class ResultGetFieldDeal
    {
        public Dictionary<string, object> Item { get; set; }
    }
}
