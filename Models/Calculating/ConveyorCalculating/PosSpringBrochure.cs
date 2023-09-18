using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.DataBase.setting;
using printing_calculator.Models.ConveyorCalculating;
using Microsoft.EntityFrameworkCore;

namespace printing_calculator.Models.Calculating.ConveyorCalculating
{
	public class PosSpringBrochure : IConveyor
	{
		private readonly Setting _settings;
		private readonly ApplicationContext _applicationContext;

		public PosSpringBrochure(Setting settings, ApplicationContext applicationContext)
		{
			_settings = settings;
			_applicationContext = applicationContext;
		}

		public Task<(СalculationHistory, Result, StatusCalculation)> TryConveyorStartAsync(СalculationHistory history, Result result, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromResult((history, result, new StatusCalculation()
				{
					Status = StatusAnswer.Cancellation
				}));
			}
			if (history.Input.SpringBrochure == SpringBrochure.None)
			{
				return Task.FromResult((history, result, new StatusCalculation()));
			}

			SpringBrochureSetting springBrochureSetting = _applicationContext.SpringBrochureSettings.Include(x => x.SpringPrice).First();
			float paperThickness = history.Input.Paper.PaperThickness;

			if (history.Input.Kinds >= ConvertMmToPageCount( springBrochureSetting.SpringPrice.Max(x => x.Page),paperThickness))
			{
				return Task.FromResult((history, result, new StatusCalculation() { 
					Status = StatusAnswer.Other, 
					ErrorMassage = "Не возможно сшить такое количество страниц сшить на пружину"
				}));
			}

			result.SpringBrochure = new SpringBrochureResult();
			foreach (Markup maxPage in  springBrochureSetting.SpringPrice.OrderBy(x => x.Page))
			{
				if (history.Input.Kinds <= ConvertMmToPageCount( maxPage.Page,paperThickness))
				{
					result.SpringBrochure.Price =  maxPage.MarkupForThisPage;
					break;
				}
			}

			bool sizeLessA4 = history.Input.Whidth <= 297 && history.Input.Height <= 297;
			if (sizeLessA4)
			{
				result.SpringBrochure.Price += springBrochureSetting.PriceForA3;
			}

			switch (history.Input.SpringBrochure)
			{
				case SpringBrochure.CoverPlasticAndCardboard:
					result.SpringBrochure.Price += sizeLessA4 ?
						(springBrochureSetting.CoverPlasticA4Price + springBrochureSetting.CoverCardboardA4Price) :
						(springBrochureSetting.CoverPlasticA3Price + springBrochureSetting.CoverCardboardA3Price);
					break;
				case SpringBrochure.CoverTwoPlastics:
					result.SpringBrochure.Price += sizeLessA4 ?
						(springBrochureSetting.CoverPlasticA4Price * 2) :
						(springBrochureSetting.CoverPlasticA3Price * 2);
					break;
			}

			result.SpringBrochure.Price += Convert.ToInt32(_settings.Machines.First(x => x.NameMachine == "SpringBrochure").ConsumableOther * history.Input.Amount);

			result.SpringBrochure.SpringBrochure = history.Input.SpringBrochure;

			//---------------------//
			result.SpringBrochure.Price = AddMashineSetting(result);
			result.SpringBrochure.Price *= history.Input.Amount;

			result.Price += result.SpringBrochure.Price;
			if (history.SpringBrochurePrice == null)
			{
				history.SpringBrochurePrice = result.SpringBrochure.Price;
				history.Price += result.SpringBrochure.Price;
			}
			else
			{
				result.SpringBrochure.ActualPrice = history.SpringBrochurePrice==result.SpringBrochure.Price;
			}
			return Task.FromResult((history, result, new StatusCalculation()));
		}

		private int AddMashineSetting(Result result)
		{
			MachineSetting machineSetting = _settings.Machines.First(x => x.NameMachine == "SpringBrochure");
			CalculatingMarkup markups = new(machineSetting.Markups);
			int markup = markups.GetMarkup(result.Amount) ;
			if(markup == 0)
			{
				return result.SpringBrochure.Price;
			}
			float finishMarkup = (100-(float)markup)/ 100f;
			return Convert.ToInt32((result.SpringBrochure.Price * finishMarkup) + machineSetting.AdjustmenPrice);
		}

		private int ConvertMmToPageCount(int millimeters,float paperThickness)
		{
			return Convert.ToInt32(millimeters / paperThickness);
		}
	}
}
