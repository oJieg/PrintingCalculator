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
        /// доп надбавочная цена за действие (на эту цену действет маркап!)
        /// </summary>
        public float ConsumableOther { get; set; }

        /// <summary>
        /// цена приладки (не действует маркап на него!)
        /// </summary>
        public int AdjustmenPrice { get; set; }
    }
}