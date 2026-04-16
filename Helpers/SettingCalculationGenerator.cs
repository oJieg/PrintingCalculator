using printing_calculator.DataBase;
using printing_calculator.DataBase.setting;

namespace printing_calculator.Models
{
    public class SettingCalculationGenerator
    {
        private static readonly Random _random = new Random();

        public static SettingCalculation GenerateRandomSettingCalculation()
        {
            return new SettingCalculation
            {
                PrintingsMachines = GeneratePrintingMachines(),
                //Machines = GenerateMachines(),
                PosMachines = GeneratePosMachines(),
                CommonToAllMarkups = GenerateCommonMarkups(),
                PaperSizes = GeneratePaperSizes(),
                PaperCatalog = GeneratePaperCatalog(),
                Laminations = GenerateLaminations()
            };
        }

        
        private static PrintingMachineSetting[] GeneratePrintingMachines()
        {
            var machines = new List<PrintingMachineSetting>();
            var names = new[] { "Heidelberg XL 106", "Komori Lithrone G40", "Manroland 700", "Ryobi 925", "KBA Rapida 105" };
            var markups = GenerateMarkups();

            for (int i = 0; i < 3; i++)
            {
                machines.Add(new PrintingMachineSetting
                {
                    Id = i + 1,
                    NameMachine = names[_random.Next(names.Length)] + " " + _random.Next(1, 100),
                    Markups = markups.Take(_random.Next(1, 3)).ToList(),
                    ConsumableOther = 10,
                    AdjustmenPrice = _random.Next(2000, 8000),
                    WhiteFieldWidth = 350,
                    WhiteFieldHeight = 480,
                    MaximumSizeLength = _random.Next(500, 800),
                    MaximumSizeWidth = _random.Next(350, 600),
                    FieldForLabels = _random.Next(5, 15),
                    Bleed = _random.Next(2, 6),
                    ConsumableDye = _random.Next(8000, 20000),
                    MainConsumableForDrawing = _random.Next(40000, 80000)
                });
            }

            return machines.ToArray();
        }

        private static MachineSetting[] GenerateMachines()
        {
            var machines = new List<MachineSetting>();
            var names = new[] { "Guillotine Cutter", "Stahlfolder KH 82", "Perfect Binder", "MBO Folding Machine", "Horizon Binder" };
            var markups = GenerateMarkups();

            for (int i = 0; i < 4; i++)
            {
                machines.Add(new MachineSetting
                {
                    Id = i + 10,
                    NameMachine = names[_random.Next(names.Length)] + " " + _random.Next(100, 999),
                    Markups = markups.Take(_random.Next(1, 2)).ToList(),
                    ConsumableOther = 10,
                    AdjustmenPrice = _random.Next(500, 3500)
                });
            }

            return machines.ToArray();
        }

        private static PosMachinesSetting[] GeneratePosMachines()
        {
            var machines = new List<PosMachinesSetting>();
            var names = new[] { "Polar D", "Morgana BM", "Duplo DC", "Challenge Champion", "Wohlenberg" };
            var markups = GenerateMarkups();

            for (int i = 0; i < 3; i++)
            {
                machines.Add(new PosMachinesSetting
                {
                    Id = i + 20,
                    NameMachine = names[_random.Next(names.Length)] + " " + _random.Next(100, 999),
                    Markups = markups.Take(_random.Next(1, 2)).ToList(),
                    ConsumableOther = 10,
                    AdjustmenPrice = _random.Next(600, 2500),
                    CountOfPapersInOneAdjustmentCut = _random.Next(200, 800) * 100,
                    AddMoreHit = 10
                });
            }

            return machines.ToArray();
        }

        private static Markup[] GenerateMarkups()
        {
            var descriptions = new[] { "Стандартная наценка", "Срочный заказ", "Малый тираж", "Premium качество", "Сложная обработка" };
            var markups = new List<Markup>();

            for (int i = 0; i < _random.Next(2, 5); i++)
            {
                markups.Add(new Markup
                {
                    Id = i + 100,
                    MarkupForThisPage = 10,
                    Page = 10
                });
            }

            return markups.ToArray();
        }

        private static CommonToAllMarkup[] GenerateCommonMarkups()
        {
            var markups = new List<CommonToAllMarkup>();
            var names = new[] { "Общая наценка", "Наценка за срочность", "Логистика", "Упаковка", "Экология" };

            for (int i = 0; i < 3; i++)
            {
                markups.Add(new CommonToAllMarkup
                {
                    Id = i + 200,
                    Name = "test",
                    Adjustmen = 10,
                    PercentMarkup = 10,
                    Description = names[_random.Next(names.Length)],
                });
            }

            return markups.ToArray();
        }

        private static SizePaper[] GeneratePaperSizes()
        {
            var sizes = new List<SizePaper>
            {
                new SizePaper { Id = 1, Name = "A0", Height = 1189, Width = 841 },
                new SizePaper { Id = 2, Name = "A1", Height = 841, Width = 594 },
                new SizePaper { Id = 3, Name = "A2", Height = 594, Width = 420 },
                new SizePaper { Id = 4, Name = "A3", Height = 420, Width = 297 },
                new SizePaper { Id = 5, Name = "A4", Height = 297, Width = 210 },
                new SizePaper { Id = 6, Name = "A5", Height = 210, Width = 148 },
                new SizePaper { Id = 7, Name = "SRA3", Height = 450, Width = 320 },
                new SizePaper { Id = 8, Name = "B4", Height = 353, Width = 250 }
            };

            return sizes.ToArray();
        }

        private static PaperCatalog[] GeneratePaperCatalog()
        {
            var catalog = new List<PaperCatalog>();
            var sizes = GeneratePaperSizes();
            var paperNames = new[] { "Матовка", "Глянец", "Офсет", "Крафт", "Дизайнерская", "Картон", "Перламутровая", "Лен" };
            var grams = new[] { 80, 115, 130, 150, 170, 200, 250, 300, 350 };

            for (int i = 0; i < 10; i++)
            {
                var gramsWeight = grams[_random.Next(grams.Length)];
                var thickness = Math.Round(gramsWeight / 800.0f, 2); // Примерная толщина

                catalog.Add(new PaperCatalog
                {
                    Id = i + 300,
                    Name = gramsWeight + "г",
                    Size = new SizePaper { Id = 7, Name = "SRA3", Height = 450, Width = 320 },
                    Status = _random.Next(0, 2),
                    PaperThickness = 10,
                    Prices = 100,
                });
            }

            return catalog.ToArray();
        }

        private static Lamination[] GenerateLaminations()
        {
            var laminations = new List<Lamination>();
            var names = new[] { "Глянцевая 25мкм", "Матовая 25мкм", "Глянцевая 50мкм", "Матовая 50мкм", "Антибликовая", "Соф-тач" };

            for (int i = 0; i < names.Length; i++)
            {
                var price = i switch
                {
                    0 => 15.0f,
                    1 => 15.0f,
                    2 => 22.0f,
                    3 => 22.0f,
                    4 => 35.0f,
                    5 => 42.0f,
                    _ => 10.0f
                };

                laminations.Add(new Lamination
                {
                    Id = i + 400,
                    Name = names[i],
                    Price = price + (float)(_random.NextDouble() * 3 - 1.5), // небольшая вариация
                    Status = _random.Next(0, 2)
                });
            }

            return laminations.ToArray();
        }
    }
}