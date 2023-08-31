namespace printing_calculator.DataBase.setting
{
	public class SpringBrochureSetting
	{
		public int Id { get; set; }
		public List<Markup> SpringPrice { get; set; }
		public int CoverCardboardA4Price { get; set; }
		public int CoverCardboardA3Price { get; set; }
		public int CoverPlasticA4Price { get; set; }
		public int CoverPlasticA3Price { get; set; }
		public int PriceForA3 { get; set; }
	}
}
