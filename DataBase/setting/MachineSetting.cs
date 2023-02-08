using System.ComponentModel.DataAnnotations.Schema;

namespace printing_calculator.DataBase.setting
{
    public class MachineSetting
    {
        public int Id { get; set; }
        //[ForeignKey("Setting")]
        //public int SettingId { get; set; }
        public string NameMAchine { get; set; }

        /// <summary>
        /// наценка за готовый лист(цена печати+бумаги)
        /// </summary>
        public List<Markups> Markup { get; set; }

        /// <summary>
        /// доп надбавочная цена за действие
        /// </summary>
        public float ConsumableOther { get; set; }

        /// <summary>
        /// цена приладки
        /// </summary>
        public int AdjustmenPrice { get; set; }
    }
}