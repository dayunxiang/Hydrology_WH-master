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
        /// 03 遥测站实时数据
        /// </summary>
        Rdata,

        /// <summary>
        /// 04 人工置数
        /// </summary>
        ArtifN,

        /// <summary>
        /// 05 指定要素
        /// </summary>
        Selement,

        /// <summary>
        /// 08 查询水泵实时数据
        /// </summary>
        pumpRead,

        /// <summary>
        /// 14 查询软件版本
        /// </summary>
        version,

        /// <summary>
        /// 24 查询报警信息
        /// </summary>
        alarm,

        /// <summary>
        /// 20 设置遥测站时钟
        /// </summary>
        clockset,

        /// <summary>
        /// 19 查询事件记录
        /// </summary>
        history,

        /// <summary>
        /// 27 查询遥测站时钟
        /// </summary>
        clocksearch,

        /// <summary>
        /// 28 旧密码
        /// </summary>
        oldPwd,

        /// <summary>
        /// 37 新密码
        /// </summary>
        newPwd,

        /// <summary>
        /// 49 初始化固态存储
        /// </summary>
        memoryReset,

        /// <summary>
        /// 15 时段起止时间
        /// </summary>
        timeFrom_To,

        /// <summary>
        /// 09  1 小时内 5 分钟间隔相对水位
        /// </summary>
        DRZ,

        /// <summary>
        /// 16 1 小时内每 5 分钟时段雨量
        /// </summary>
        DRP,

        /// <summary>
        /// 10 时间步长码
        /// </summary>
        Step,

        /// <summary>
        /// 62 遥测站基本配置读取
        /// </summary>
        basicConfigRead,

        /// <summary>
        /// 02 遥测站基本配置修改
        /// </summary>
        basicConfigModify,

        /// <summary>
        /// 12 运行参数读取
        /// </summary>
        operatingParaRead,

        /// <summary>
        /// 06 运行参数修改
        /// </summary>
        operatingParaModify,

        /// <summary>
        /// 11 恢复出厂设置
        /// </summary>
        Reset,

        /// <summary>
        /// 07 设罝遥测站IC卡状态
        /// </summary>
        ICconfig,

        /// <summary>
        /// 54  控制水泵状态
        /// </summary>
        pumpCtrl,

        /// <summary>
        /// 55  控制阀门状态
        /// </summary>
        valveCtrl,

        /// <summary>
        /// 56  控制闸门状态
        /// </summary>
        gateCtrl,

        /// <summary>
        /// 57 水量定值控制
        /// </summary>
        waterYield
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
