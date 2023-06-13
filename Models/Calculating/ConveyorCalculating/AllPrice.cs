using printing_calculator.DataBase;
using printing_calculator.DataBase.setting;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
	public class AllPrice : IConveyor
	{
		private readonly Setting _settings;

		public AllPrice(Setting settings)
		{
			_settings = settings;
		}

		public Task<(СalculationHistory, Result, bool)> TryConveyorStartAsync(СalculationHistory history, Result result, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromResult((history, result, false));
			}

			try
			{
				result.Price = Convert.ToInt32(Math.Round((double)result.Price / 100, 1) * 100); //округление
				if (history.Input.CommonToAllMarkupName != null)
				{
					foreach (string commonToAllMarkupName in history.Input.CommonToAllMarkupName)
					{
						result.Price = (int)((float)result.Price *
							((_settings.CommonToAllMarkups.Where(x => x.Name == commonToAllMarkupName).First().PercentMarkup + 100) / 100f)
							+ _settings.CommonToAllMarkups.Where(x => x.Name == commonToAllMarkupName).First().Adjustmen);
					}
				}

				if (history.Price == null)
				{
					result.TryPrice = true;
					history.Price = result.Price;
				}

				result.TryPrice = result.IsActualPaperPrice() &&
					(result.Price == history.Price);

				if (result.Price != history.Price)
				{
					result.Price = history.Price.Value;
					result.TryPrice = false;
				}

				return Task.FromResult((history, result, true));
			}
			catch (OverflowException)
			{
				return Task.FromResult((history, result, false));
			}
		}
	}
}