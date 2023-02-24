﻿namespace printing_calculator.DataBase
{
    public class InputHistory
    {
        public int Id { get; set; }
        public int Height { get; set; }
        public int Whidth { get; set; }
        public PaperCatalog Paper { get; set; } = null!;
        public int Amount { get; set; }
        public int Kinds { get; set; }
        public bool Duplex { get; set; }
        public int? LaminationId { get; set; }
        public Lamination? Lamination { get; set; }
        public int CreasingAmount { get; set; }
        public int DrillingAmount { get; set; }
        public bool RoundingAmount { get; set; }
		public List<string>? CommonToAllMarkupName { get; set; } = null!;
	}
}