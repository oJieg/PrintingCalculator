using printing_calculator.DataBase;
using printing_calculator.DataBase.setting;
using printing_calculator.Models.ConveyorCalculating;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.Calculating.ConveyorCalculating
{
	public class PosStapleBrochure : IConveyor
	{
		private readonly Setting _settings;

		public PosStapleBrochure(Setting settings)
		{
			_settings = settings;
		}

		public async Task<(СalculationHistory, Result, StatusCalculation)> TryConveyorStartAsync(СalculationHistory history, Result result, CancellationToken cancellationToken)
		{
			if (history.Input.StapleBrochure == false)
			{
				result.PosResult.ActualStapleBrochure = true;
				return (history, result, new StatusCalculation());
			}

			try
			{
				int сostPrice = Convert.ToInt32((_settings.Machines.First(x => x.NameMachine == "StapleBrochure").ConsumableOther) * result.Amount);

				CalculatingMarkup markups = new(_settings.Machines.First(x => x.NameMachine == "StapleBrochure").Markups);
				int markup = markups.GetMarkup(result.Amount);
				int price;

				result.PosResult.StapleBrochurePrice = Convert.ToInt32((сostPrice * ((markup + 100) / (float)100)) + _settings.Machines.First(x => x.NameMachine == "StapleBrochure").AdjustmenPrice);
				result.PosResult.StapleBrochurePrice *= history.Input.Amount;
				result.Price += result.PosResult.StapleBrochurePrice;
				if (history.StapleBrochurePrice!=null)
				{
					result.PosResult.ActualStapleBrochure = history.StapleBrochurePrice == result.PosResult.StapleBrochurePrice;
				}
				else
				{
					result.PosResult.ActualStapleBrochure = true;
					history.StapleBrochurePrice = result.PosResult.StapleBrochurePrice;
					history.Price += result.PosResult.StapleBrochurePrice;
				}
				return (history, result, new StatusCalculation());
			}
			catch
			{
				return (history, result, new StatusCalculation()
				{
					Status = StatusAnswer.Other,
					ErrorMassage = "Произошла ошибка при вычислении ConsumablePrice"
				});
			}
		}
	}
}
