﻿using Microsoft.AspNetCore.Mvc;
using printing_calculator.ViewModels;
using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Calculating;
using printing_calculator.Models;

namespace printing_calculator.controllers
{
    public class CalculatorResultController : Controller
    {
        private readonly ApplicationContext _applicationContext;
        private readonly ILogger<CalculatorResultController> _logger;
        private readonly ConveyorCalculator _calculator;
        private readonly GeneratorHistory _generatorHistory;
        private readonly Validation _validation;

        public CalculatorResultController(ApplicationContext applicationContext,
            ILogger<CalculatorResultController> loggerFactory,
            ConveyorCalculator conveyorCalculator,
            GeneratorHistory generatorHistory,
            Validation validation)
        {
            _applicationContext = applicationContext;
            _logger = loggerFactory;
            _calculator = conveyorCalculator;
            _generatorHistory = generatorHistory;
            _validation = validation;
        }

        [HttpPost]
        public async Task<IActionResult> Index(Input input, CancellationToken cancellationToken)
        {
            if (!(await _validation.TryValidateInputAsync(input, cancellationToken)))
            {
                _logger.LogError("input не прошел валидацию input:{input}", input);
                return BadRequest();
            }

            СalculationHistory? history = await _generatorHistory.GetFullIncludeHistoryAsync(input, cancellationToken);
            if (history == null)
                return NotFound(); //или другой код ошибки

            Result result;

            try
            {
                (history, result, bool tryAnswer) = await _calculator.TryStartCalculation(history, cancellationToken);

                if (!tryAnswer)
                {
                    _logger.LogError("не удался расчет для данных из Input");
                    return NotFound("Вероятнее всего на данный размер листа не помещается изделие!");
                }
            }
            catch (OperationCanceledException)
            {
                return new EmptyResult();
            }

            if (!input.NoSaveDB)
            {
                try
                {
                    _applicationContext.InputsHistories.Add(history.Input);
                    _applicationContext.Histories.Add(history);

                    await _applicationContext.SaveChangesAsync(cancellationToken);
                    result.HistoryInputId = history.Id;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "не удалось сохранить просчет");
                }
            }

            return View("CalculatorResult", result);
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id, CancellationToken cancellationToken)
        {
            ConveyorCalculator conveyor = _calculator;
            СalculationHistory? history = await _generatorHistory.GetFullIncludeHistoryAsync(id, cancellationToken);
            if (history == null)
                return NotFound(); //или другой код ошибки

            Result result;
            try
            {
                (history, result, bool tryAnswer) = await conveyor.TryStartCalculation(history, cancellationToken);

                if (!tryAnswer)
                {
                    _logger.LogError("не удался расчет на конвейере");
                    return NotFound();
                }
            }
            catch (OperationCanceledException)
            {
                return new EmptyResult();
            }

            result.HistoryInputId = id;

            return View("CalculatorResult", result);
        }
    }
}