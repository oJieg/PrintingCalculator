using printing_calculator.DataBase;
using printing_calculator.DataBase.setting;
using printing_calculator.ViewModels;
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

		public Task<(СalculationHistory, Result, StatusCalculation )> TryConveyorStartAsync(СalculationHistory history, Result result, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromResult((history, result, new StatusCalculation () { 
					Status = StatusType.Cancellation
				}));
			}

			try
			{
				result.Price = Convert.ToInt32(Math.Round((double)result.Price / 100, 1) * 100); //округление
				if (history.Input.CommonToAllMarkupName != null)
				{
					result.CommonToAllMarkupName = new();
					foreach (string commonToAllMarkupName in history.Input.CommonToAllMarkupName)
					{
						if (commonToAllMarkupName != null)
						{
							
							CommonToAllMarkup commonToAllMarkup = _settings.CommonToAllMarkups.Where(x => x.Name == commonToAllMarkupName).First();
							int percentMarkup = commonToAllMarkup.PercentMarkup;
							result.CommonToAllMarkupName.Add(commonToAllMarkup);
							float multiplicationMarkup;
							if (percentMarkup > 0) {
								multiplicationMarkup = ((percentMarkup + 100) / 100f);
							}
							else
							{
								multiplicationMarkup = (percentMarkup + 100) / 100f;
							}

							result.Price = (int)((float)result.Price *
								multiplicationMarkup
								+ _settings.CommonToAllMarkups.Where(x => x.Name == commonToAllMarkupName).First().Adjustmen);
						}
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

				return Task.FromResult((history, result, new StatusCalculation()));
			}
			catch (OverflowException)
			{
				return Task.FromResult((history, result, new StatusCalculation() { 
					Status = StatusType.Other, 
					ErrorMassage = "Стоимость итоговая вышла за возможные приделы int" }));
			}
		}
	}
}