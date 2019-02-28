using System;
using System.Collections.Generic;

namespace Hydrology.Entity
{
    public enum EDownParam
    {

        /// <summary>
        /// 03 时钟
        /// </summary>
        Clock,

        /// <summary>
        /// 04 常规状态
        /// </summary>
        NormalState,

        /// <summary>
        /// 05 电压
        /// </summary>
        Voltage,

        /// <summary>
        /// 08 站号
        /// </summary>
        StationCmdID,

        /// <summary>
        /// 14 对时选择
        /// </summary>
        TimeChoice,

        /// <summary>
        /// 24 定时段次
        /// </summary>
        TimePeriod,

        /// <summary>
        /// 20 工作状态
        /// </summary>
        WorkStatus,

        /// <summary>
        /// 19 版本号
        /// </summary>
        VersionNum,

        /// <summary>
        /// 27 主备信道
        /// </summary>
        StandbyChannel,

        /// <summary>
        /// 28 SIM卡号
        /// </summary>
        TeleNum,

        /// <summary>
        /// 37 振铃次数
        /// </summary>
        RingsNum,

        /// <summary>
        /// 49 目的地手机号码
        /// </summary>
        DestPhoneNum,

        /// <summary>
        /// 15 终端机号
        /// </summary>
        TerminalNum,

        /// <summary>
        /// 09 响应波束
        /// </summary>
        RespBeam,

        /// <summary>
        /// 16 平均时间
        /// </summary>
        AvegTime,

        /// <summary>
        /// 10 雨量加报值
        /// </summary>
        RainPlusReportedValue,

        /// <summary>
        /// 62 KC
        /// </summary>
        KC,

        /// <summary>
        /// 02 雨量
        /// </summary>
        Rain,

        /// <summary>
        /// 12 水位
        /// </summary>
        Water,

        /// <summary>
        /// 06 水位加报值
        /// </summary>
        WaterPlusReportedValue,

        /// <summary>
        /// 11 采集段次选择
        /// </summary>
        SelectCollectionParagraphs,

        /// <summary>
        /// 07 测站类型
        /// </summary>
        StationType,

        /// <summary>
        /// 54  用户名
        /// </summary>
        UserName,

        /// <summary>
        /// 55  测站名
        /// </summary>
        StationName
    }

    public enum EDownParamGY
    {
        /// <summary>
        /// 03 时钟
        /// </summary>
        ontime,

        /// <summary>
        /// 04 常规状态
        /// </summary>
        DRZ,

        /// <summary>
        /// 05 电压
        /// </summary>
        testtime,

        /// <summary>
        /// 08 站号
        /// </summary>
        oldPwd,

        /// <summary>
        /// 14 对时选择
        /// </summary>
        newPwd,

        /// <summary>
        /// 24 定时段次
        /// </summary>
        memoryReset,

        /// <summary>
        /// 20 工作状态
        /// </summary>
        timeFrom,

        /// <summary>
        /// 19 版本号
        /// </summary>
        timeTo,

        /// <summary>
        /// 27 主备信道
        /// </summary>
        timeFrom_To,

        /// <summary>
        /// 28 SIM卡号
        /// </summary>
        DRP,

        /// <summary>
        /// 37 振铃次数
        /// </summary>
        Step,

        /// <summary>
        /// 49 目的地手机号码
        /// </summary>
        basicConfig,

        /// <summary>
        /// 15 终端机号
        /// </summary>
        operatingPara,

        /// <summary>
        /// 09 响应波束
        /// </summary>
        Reset,

        /// <summary>
        /// 16 平均时间
        /// </summary>
        ICconfig,

        /// <summary>
        /// 10 雨量加报值
        /// </summary>
        pumpCtrl,

        /// <summary>
        /// 62 KC
        /// </summary>
        valveCtrl,

        /// <summary>
        /// 02 雨量
        /// </summary>
        gateCtrl,

        /// <summary>
        /// 12 水位
        /// </summary>
        waterYield,

        /// <summary>
        /// 06 水位加报值
        /// </summary>
        WaterPlusReportedValue,

        /// <summary>
        /// 11 采集段次选择
        /// </summary>
        SelectCollectionParagraphs,

        /// <summary>
        /// 07 测站类型
        /// </summary>
        StationType,

        /// <summary>
        /// 54  用户名
        /// </summary>
        UserName,

        /// <summary>
        /// 55  测站名
        /// </summary>
        StationName,

        Version


    }
    public enum ENormalState
    {
        GPRS,
        GSM
    }
    public enum ETimeChoice
    {
        AdjustTime,
        Two,
    }
    public enum EWorkStatus
    {
        Debug,
        Normal,
        DoubleAddress
    }
    public enum ESelectCollectionParagraphs
    {
        FiveOrSix,
        TenOrTwelve
    }
    public enum ETimePeriod
    {
        One,
        Two,
        Four,
        Six,
        Eight,
        Twelve,
        TwentyFour,
        FourtyEight
    }
}
