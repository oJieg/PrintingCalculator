namespace printing_calculator.Clients.AnswerModels
{
    public class GetFieldsAnswer
    {
        public ResultGetField Result { get; set; }
    }
    public class ResultGetField
    {
        public Dictionary<string, ItemInfo> Fields {  get; set; }
    }

    public class ItemInfo
    {
        public string Title { get; set; }
    }
}
