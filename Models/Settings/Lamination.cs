﻿namespace printing_calculator.Models.Settings
{
    public class Lamination
    {
        public List<MarkupList> MarkupList { get; set; } = null!;
        public int Adjustment { get; set; }
        public int Job { get; set; }
    }
}