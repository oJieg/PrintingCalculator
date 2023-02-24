using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace printing_calculator.DataBase.setting
{
	[Index(nameof(NameMachine), IsUnique = true)]
	public class MachineSetting
    {
        public int Id { get; set; }
        public string NameMachine { get; set; }

        /// <summary>
        /// наценка за готовый лист(цена печати+бумаги)
        /// </summary>
        public List<Markup> Markups { get; set; }

        /// <summary>
        /// доп надбавочная цена за действие (на эту цену действет маркап!)
        /// </summary>
        public float ConsumableOther { get; set; }

        /// <summary>
        /// цена приладки (не действует маркап на него!)
        /// </summary>
        public int AdjustmenPrice { get; set; }
    }
}