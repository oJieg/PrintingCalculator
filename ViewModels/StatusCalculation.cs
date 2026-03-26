namespace printing_calculator
{
	public class StatusCalculation
	{
		public StatusAnswer Status { get; set; } = StatusAnswer.Ok;
		public string? ErrorMassage { get; set; }
	}

	public enum StatusAnswer
	{
		Ok = 0,
		WrongSize = 1,
		Cancellation = 2,
		Other = 3,
		NotFaund = 4,
		ErrorDataBase = 5
	}
}
