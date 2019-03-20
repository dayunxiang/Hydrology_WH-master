using System;
using System.Text;

namespace Hydrology.Entity
{
    public class CReportData
    {
        /// <summary>
        /// 数据采集时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 水量
        /// </summary>
        public Nullable<Decimal> Water { get; set; }
        /// <summary>
        /// 累计雨量
        /// </summary>
        public Nullable<Decimal> Rain { get; set; }
        /// <summary>
        /// 差值雨量
        /// </summary>
        public Nullable<Decimal> DiffRain { get; set; }

        /// <summary>
        /// 时段雨量
        /// </summary>
        public Nullable<Decimal> PeriodRain { get; set; }

        /// <summary>
        /// 当前降水量日开始点到现在的降水量，即上个8：00到现在的降水量
        /// </summary>
        public Nullable<Decimal> CurrentRain { get; set; }



        /// <summary>
        /// 电压
        /// </summary>
        public Nullable<Decimal> Voltge { get; set; }

        
    }
}
