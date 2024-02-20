namespace printing_calculator.ModelOut
{
    public class SizePaper
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        /// <summary>
        /// длинная часть
        /// </summary>
        public int Height { get; set; } 
        /// <summary>
        /// Короткая часть
        /// </summary>
        public int Width { get; set; } 
    }
}