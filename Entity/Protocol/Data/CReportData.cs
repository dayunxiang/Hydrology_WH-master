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

        /// <summary>
        /// 日降水量
        /// </summary>
        public Nullable<Decimal> Dayrain { get; set; }
        /// <summary>
        /// 暴雨量
        /// </summary>
        public Nullable<Decimal> HeavyRain { get; set; }
        /// <summary>
        /// 30分钟时段降水量
        /// </summary>
        public Nullable<Decimal> Rain30 { get; set; }
        /// <summary>
        /// 10分钟时段降水量
        /// </summary>
        public Nullable<Decimal> Rain10 { get; set; }
        /// <summary>
        /// 5分钟时段降水量
        /// </summary>
        public Nullable<Decimal> Rain05 { get; set; }
        /// <summary>
        /// 1分钟时段降水量
        /// </summary>
        public Nullable<Decimal> Rain01 { get; set; }
        /// <summary>
        /// 12 小时时段降水量
        /// </summary>
        public Nullable<Decimal> Rain12hour { get; set; }
        /// <summary>
        /// 6 小时时段降水量
        /// </summary>
        public Nullable<Decimal> Rain6hour { get; set; }
        /// <summary>
        /// 3 小时时段降水量
        /// </summary>
        public Nullable<Decimal> Rain3hour { get; set; }
        /// <summary>
        /// 2 小时时段降水量
        /// </summary>
        public Nullable<Decimal> Rain2hour { get; set; }
        /// <summary>
        /// 1 小时时段降水量
        /// </summary>
        public Nullable<Decimal> Rain1hour { get; set; }
        /// <summary>
        /// 湿度
        /// </summary>
        public Nullable<Decimal> Humidity { get; set; }
        /// <summary>
        /// 100 厘米处土壤含水量
        /// </summary>
        public Nullable<Decimal> Soilhum100 { get; set; }
        /// <summary>
        /// 80 厘米处土壤含水量
        /// </summary>
        public Nullable<Decimal> Soilhum80 { get; set; }
        /// <summary>
        /// 60 厘米处土壤含水量
        /// </summary>
        public Nullable<Decimal> Soilhum60 { get; set; }
        /// <summary>
        /// 50 厘米处土壤含水量
        /// </summary>
        public Nullable<Decimal> Soilhum50 { get; set; }
        /// <summary>
        /// 40 厘米处土壤含水量
        /// </summary>
        public Nullable<Decimal> Soilhum40 { get; set; }
        /// <summary>
        /// 30 厘米处土壤含水量
        /// </summary>
        public Nullable<Decimal> Soilhum30 { get; set; }
        /// <summary>
        /// 20 厘米处土壤含水量
        /// </summary>
        public Nullable<Decimal> Soilhum20 { get; set; }
        /// <summary>
        /// 10 厘米处土壤含水量
        /// </summary>
        public Nullable<Decimal> Soilhum10 { get; set; }
        /// <summary>
        /// 波浪高度
        /// </summary>
        public Nullable<Decimal> Waveheight { get; set; }
        /// <summary>
        /// 地下水瞬时埋深
        /// </summary>
        public Nullable<Decimal> Groundwaterdepth { get; set; }
        /// <summary>
        /// 地温
        /// </summary>
        public Nullable<Decimal> Groundtemp { get; set; }
        /// <summary>
        /// 水库、闸坝闸门开启孔数
        /// </summary>
        public Nullable<Decimal> Holenum { get; set; }
        /// <summary>
        /// 输水设备类别
        /// </summary>
        public Nullable<Decimal> WaterequipType { get; set; }
        /// <summary>
        /// 输水设备、闸门(组)编号
        /// </summary>
        public Nullable<Decimal> WaterequipNum { get; set; }
        /// <summary>
        /// 闸坝、水库闸门开启高度
        /// </summary>
        public Nullable<Decimal> Openheight { get; set; }
        /// <summary>
        /// 气压
        /// </summary>
        public Nullable<Decimal> Airpressure { get; set; }
        /// <summary>
        /// 当前蒸发
        /// </summary>
        public Nullable<Decimal> Pevaporation { get; set; }
        /// <summary>
        /// 日蒸发量
        /// </summary>
        public Nullable<Decimal> EvaporationDay { get; set; }
        /// <summary>
        /// 瞬时水温
        /// </summary>
        public Nullable<Decimal> WaterTemp { get; set; }
        /// <summary>
        /// 瞬时气温
        /// </summary>
        public Nullable<Decimal> AirTemp { get; set; }
        /// <summary>
        /// 断面面积
        /// </summary>
        public Nullable<Decimal> SectionArea { get; set; }
        /// <summary>
        /// 图片信息
        /// </summary>
        public string Picture { get; set; }
        /// <summary>
        /// 版本信息
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 查询遥测站状态及报警信息
        /// </summary>
        public string Alarm { get; set; }
        /// <summary>
        /// 设罝遥测站IC卡状态
        /// </summary>
        public string ICConfig { get; set; }
        /// <summary>
        /// 水泵状态
        /// </summary>
        public string Pumpstate { get; set; }
        /// <summary>
        /// 阀门状态
        /// </summary>
        public string Valvestate { get; set; }
        /// <summary>
        /// 闸门状态
        /// </summary>
        public string Gatestate { get; set; }
        /// <summary>
        /// 水量定值控制
        /// </summary>
        public string Waterctrl { get; set; }
        /// <summary>
        /// 查询事件记录
        /// </summary>
        public string History { get; set; }

        /// <summary>
        /// 中心站地址
        /// </summary>
        public string Sid { get; set; }
        /// <summary>
        /// 遥测站地址
        /// </summary>
        public string Yid { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 目的地 1 信道类型及地址
        /// </summary>
        public string Desinfo1 { get; set; }
        /// <summary>
        /// 目的地 2 信道类型及地址
        /// </summary>
        public string Desinfo2 { get; set; }
        /// <summary>
        /// 目的地 3 信道类型及地址
        /// </summary>
        public string Desinfo3 { get; set; }
        /// <summary>
        /// 目的地 4 信道类型及地址
        /// </summary>
        public string Desinfo4 { get; set; }
        /// <summary>
        /// 主备信道设置
        /// </summary>
        public Nullable<Decimal> Channelset { get; set; }
        /// <summary>
        /// 工作方式
        /// </summary>
        public Nullable<Decimal> Workmode { get; set; }
        /// <summary>
        /// 遥测站采集要素设置
        /// </summary>
        public string Collectelements { get; set; }
        /// <summary>
        /// 中继站（集合转发站）服务地址范围
        /// </summary>
        public string AddressRange { get; set; }
        /// <summary>
        /// 遥测站通信设备识别号
        /// </summary>
        public string Idnum { get; set; }

        /// <summary>
        /// 运行参数
        /// </summary>
        public Nullable<Decimal>[] Oparameters { get; set; }

        /// <summary>
        /// 时间步长码
        /// </summary>
        public Nullable<Decimal> StepnumD { get; set; }
        /// <summary>
        /// 时间步长码
        /// </summary>
        public Nullable<Decimal> StepnumH { get; set; }
        /// <summary>
        /// 时间步长码
        /// </summary>
        public Nullable<Decimal> StepnumN { get; set; }
        /// <summary>
        /// 交流A相电压
        /// </summary>
        public Nullable<Decimal> VTA { get; set; }
        /// <summary>
        /// 交流B相电压
        /// </summary>
        public Nullable<Decimal> VTB { get; set; }
        /// <summary>
        /// 交流C相电压
        /// </summary>
        public Nullable<Decimal> VTC { get; set; }
        /// <summary>
        /// 交流A相电流
        /// </summary>
        public Nullable<Decimal> VIA { get; set; }
        /// <summary>
        /// 交流B相电流
        /// </summary>
        public Nullable<Decimal> VIB { get; set; }
        /// <summary>
        /// 交流C相电流
        /// </summary>
        public Nullable<Decimal> VIC { get; set; }


    }
}
