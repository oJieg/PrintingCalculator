using printing_calculator.DataBase;

namespace printing_calculator.ViewModels.Result
{
	public class SpringBrochureResult
	{
		public SpringBrochure SpringBrochure { get; set; }
		public int Price { get; set; }
		public bool ActualPrice { get; set; } = true;
	}
}
