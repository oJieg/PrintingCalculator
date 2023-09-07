using printing_calculator.ViewModels.Result;

namespace printing_calculator.ViewModels
{
    public class ApiResultAnswer
    {
        public StatusCalculation Status { get; set; }
        public int? IdHistory { get; set; }
        public Result.Result Result { get; set; }
    }
}
