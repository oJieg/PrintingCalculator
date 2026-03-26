namespace printing_calculator.ViewModels
{
    public class Answer<T>
    {
        public StatusAnswer Status { get; set; } = StatusAnswer.Ok;
        public string? ErrorMassage { get; set; }
        public T? Result { get; set; }
    }
}
