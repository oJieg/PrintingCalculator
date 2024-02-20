using Microsoft.EntityFrameworkCore;

namespace printing_calculator.DataBase.setting
{
	[Index(nameof(Name), IsUnique = true)]
	public class CommonToAllMarkup
	{
		public int? Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int PercentMarkup { get; set; }
		public int Adjustmen { get; set; }
	}
}
