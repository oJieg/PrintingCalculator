namespace printing_calculator
{
	public class StatusCalculation
	{
		public StatusType Status { get; set; } = StatusType.Ok;
		public string? ErrorMassage { get; set; }
	}

	public enum StatusType
	{
		Ok = 0,
		WrongSize = 1,
		Cancellation = 2,
		Other = 3
	}
}
