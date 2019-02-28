using System;
using Hydrology.Entity;
using System.Diagnostics;

namespace Hydrology.Entity
{
    public class CEntityStation : IComparable
    {
        #region PROPERTY

        /// <summary>
        /// 对应于每个站点的唯一索引，主键，四位,去除空格
        /// </summary>
        private string m_strStationID;
        public string StationID
        {
            get { return m_strStationID; }
            set { m_strStationID = value.Trim(); }
        }

        /// <summary>
        /// 所属分中心的ID
        /// </summary>
        public Nullable<int> SubCenterID { get; set; }

        /// <summary>
        ///  站点名字
        /// </summary>
        public string StationName { get; set; }

        /// <summary>
        ///  站点类型
        /// </summary>
        public EStationType StationType { get; set; }

        /// <summary>
        /// 站点的水位基值
        /// </summary>
        public Nullable<decimal> DWaterBase { get; set; }

        /// <summary>
        /// 站点的水位最大值
        /// </summary>
        public Nullable<decimal> DWaterMax { get; set; }

        /// <summary>
        /// 站点的水位最小值
        /// </summary>
        public Nullable<decimal> DWaterMin { get; set; }

        /// <summary>
        /// 允许的水位变化值,水位阀值
        /// </summary>
        public Nullable<decimal> DWaterChange { get; set; }

        /// <summary>
        /// 雨量精度( 0.1,0.5,1.0 ), 收到的雨量值如果是X，则雨量值是X*DRainAccuracy
        /// </summary>
        public float DRainAccuracy { get; set; }

        /// <summary>
        /// 允许的雨量变化，雨量阀值
        /// </summary>
        public Nullable<decimal> DRainChange { get; set; }

        /// <summary>
        /// GSM号码
        /// </summary>
        public string GSM { get; set; }

        /// <summary>
        /// GRPS号码
        /// </summary>
        public string GPRS { get; set; }

        /// <summary>
        /// 北斗卫星终端号码
        /// </summary>
        public string BDSatellite { get; set; }

        /// <summary>
        /// 北斗卫星成员号码
        /// </summary>
        public string BDMemberSatellite { get; set; }


        /// <summary>
        /// 电压的最低值
        /// </summary>
        public Nullable<float> DVoltageMin { get; set; }


        //主信道
        public string Maintran { get; set; }

        //备信道
        public string Subtran { get; set; }


        //数据协议
        public string Datapotocol { get; set; }

        //水位传感器
        public string Watersensor { get; set; }

        //雨量传感器
        public string Rainsensor { get; set; }

        //报讯段次
        public string Reportinterval { get; set; }



        //////////////////////////////////////////////////////////////////////////
        ///////////////////////////便于计算时段雨量和日雨量,以及水位变化//////////
        //////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 上一次的时段雨量
        /// </summary>
        public Nullable<Decimal> LastPeriodRain { get; set; }
        /// <summary>
        /// 上一次的雨量最大值
        /// </summary>
        public Nullable<Decimal> LastTotalRain { get; set; }

        /// <summary>
        /// 上一次整点的雨量值
        /// </summary>
        public Nullable<Decimal> LastClockSharpTotalRain { get; set; }
        public Nullable<DateTime> LastClockSharpTime { get; set; }
        /// <summary>
        /// 最近一天的的雨量记录
        /// </summary>
        public Nullable<Decimal> LastDayTotalRain { get; set; }
        /// <summary>
        /// 前一天的的雨量记录
        /// </summary>
        public Nullable<Decimal> LLastDayTotalRain { get; set; }

        /// <summary>
        /// 最近一天有雨量记录的时间
        /// </summary>
        public Nullable<DateTime> LastDayTime { get; set; }

        /// <summary>
        /// 上一次的水位
        /// </summary>
        public Nullable<Decimal> LastWaterStage { get; set; }

        /// <summary>
        /// 上一次的相应流量
        /// </summary>
        public Nullable<Decimal> LastWaterFlow { get; set; }

        /// <summary>
        /// 上一次传输的信道类型
        /// </summary>
        public Nullable<EChannelType> LastChannelType { get; set; }

        /// <summary>
        /// 上一次的数据类型
        /// </summary>
        public Nullable<EMessageType> LastMessageType { get; set; }

        /// <summary>
        /// 上一次的电压
        /// </summary>
        public Nullable<Decimal> LastVoltage { get; set; }

        /// <summary>
        /// 上一次数据的时间
        /// </summary>
        public Nullable<DateTime> LastDataTime { get; set; }

        #endregion

        public int CompareTo(object obj)
        {
            int result = 0;
            try
            {
                CEntityStation station = obj as CEntityStation;
                int idthis = int.Parse(this.StationID);
                int idobj = int.Parse(station.StationID);
                return idthis - idobj;
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return result;
        }
    }
}
