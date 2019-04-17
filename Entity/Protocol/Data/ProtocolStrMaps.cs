using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hydrology.Entity
{
    public class ProtocolMaps
    {
        public static CDictionary<EStationTypeProto, String> StationType4ProtoMap = new CDictionary<EStationTypeProto, String>()
        {
            //  需要修改
            { EStationTypeProto.ERainFall,           "01" },
            { EStationTypeProto.EParallelEHydrology, "03" },
            { EStationTypeProto.EParallelRiverWater, "02" },
            { EStationTypeProto.EParallelSpecial,    "17" },
            { EStationTypeProto.ESerialEHydrology,   "13" },
            { EStationTypeProto.ESerialRiverWater,   "12" },
            { EStationTypeProto.ESerialSpecial,      "07" }
        };
        public static CDictionary<ENormalState, String> NormalState4ProtoMap = new CDictionary<ENormalState, String>()
        {
            { ENormalState.GPRS,   "01" },
            { ENormalState.GSM,    "02" }
        };
        public static CDictionary<ENormalState, string> NormalState4UI = new CDictionary<ENormalState, string>()
        {
            { ENormalState.GPRS,   "GPRS" },
            { ENormalState.GSM,    "GSM" }
        };


        public static CDictionary<ETimeChoice, String> TimeChoice4ProtoMap = new CDictionary<ETimeChoice, String>()
        {
                        //  需要修改
            { ETimeChoice.AdjustTime,   "01" },
            { ETimeChoice.Two,          "02" }
        };
        public static CDictionary<ETimeChoice, String> TimeChoice4UI = new CDictionary<ETimeChoice, String>()
        {
                        //  需要修改
            { ETimeChoice.AdjustTime,   "对时" },
            { ETimeChoice.Two,          "不对时" }
        };


        public static CDictionary<EWorkStatus, String> WorkStatus4ProtoMap = new CDictionary<EWorkStatus, String>()
        {
                        //  需要修改
            { EWorkStatus.Debug,   "01" },
            { EWorkStatus.Normal,  "02" },
            { EWorkStatus.DoubleAddress,"03" }
        };
        public static CDictionary<EWorkStatus, String> WorkStatus4UI = new CDictionary<EWorkStatus, String>()
        {
                        //  需要修改
            { EWorkStatus.Debug,   "调试状态" },
            { EWorkStatus.Normal,  "正常工作状态" },
            { EWorkStatus.DoubleAddress,"双目标地址" }
        };

        public static CDictionary<EDownParam, String> DownParamMap = new CDictionary<EDownParam, String>()
        {
            { EDownParam.Clock ,        "03" },
            { EDownParam.NormalState ,  "04" },
            { EDownParam.Voltage ,      "05"},
            { EDownParam.StationCmdID , "08" },
            { EDownParam.TimeChoice ,   "14" },
            { EDownParam.TimePeriod ,   "24" },
            { EDownParam.WorkStatus ,   "20" },
            { EDownParam.VersionNum ,   "19" },
            { EDownParam.StandbyChannel,"27" },
            { EDownParam.TeleNum ,      "28" },
            { EDownParam.RingsNum ,     "37" },
            { EDownParam.DestPhoneNum , "49" },
            { EDownParam.TerminalNum ,  "15" },
            { EDownParam.RespBeam ,     "09" },
            { EDownParam.AvegTime ,     "16" },
            { EDownParam.RainPlusReportedValue , "10" },
            { EDownParam.KC ,       "62" },
            { EDownParam.Rain ,     "02" },
            { EDownParam.Water ,    "12" },
            { EDownParam.WaterPlusReportedValue ,     "06" },
            { EDownParam.SelectCollectionParagraphs , "11" },
            { EDownParam.StationType , "07" },
            { EDownParam.UserName , "54" },
            { EDownParam.StationName , "55" }
        };

        public static CDictionary<EDownParamGY, String> DownParamMapGY = new CDictionary<EDownParamGY, String>()
        {
            { EDownParamGY.Rdata ,        "03" },
            { EDownParamGY.ArtifN ,  "04" },
            { EDownParamGY.Selement ,      "05"},
            { EDownParamGY.pumpRead , "08" },
            { EDownParamGY.version ,   "14" },
            { EDownParamGY.alarm ,   "24" },
            { EDownParamGY.clockset ,   "20" },
            { EDownParamGY.history ,   "19" },
            { EDownParamGY.clocksearch,"27" },
            { EDownParamGY.oldPwd ,      "28" },
            { EDownParamGY.newPwd ,     "37" },
            { EDownParamGY.memoryReset , "49" },
            { EDownParamGY.timeFrom_To ,  "15" },
            { EDownParamGY.DRZ ,     "09" },
            { EDownParamGY.DRP ,     "16" },
            { EDownParamGY.Step , "10" },
            { EDownParamGY.basicConfigRead ,       "62" },
            { EDownParamGY.basicConfigModify ,     "02" },
            { EDownParamGY.operatingParaRead ,    "12" },
            { EDownParamGY.operatingParaModify ,     "06" },
            { EDownParamGY.Reset , "11" },
            { EDownParamGY.ICconfig , "07" },
            { EDownParamGY.pumpCtrl , "54" },
            { EDownParamGY.valveCtrl , "55" },
            { EDownParamGY.gateCtrl,"56"},
            { EDownParamGY.waterYield,"57"}
        };

        /// <summary>
        /// -1 表示长度未定
        /// 正数表示相应字段对应字符串解析中的长度
        /// </summary>
        public static CDictionary<EDownParam, String> DownParamLengthMap = new CDictionary<EDownParam, String>()
        {
            { EDownParam.Clock,                     "12"},
            { EDownParam.NormalState,               "2" },
            { EDownParam.Voltage,                   "4" },
            { EDownParam.StationCmdID,              "4" },
            { EDownParam.TimeChoice,                "2" },
            { EDownParam.TimePeriod,                "2" },
            { EDownParam.WorkStatus,                "2" },
            { EDownParam.VersionNum,                "-1"},
            { EDownParam.StandbyChannel,            "4" },
            { EDownParam.TeleNum,                   "-1"},
            { EDownParam.RingsNum,                  "2" },
            { EDownParam.DestPhoneNum,              "11"},
            { EDownParam.TerminalNum,               "8" },
            { EDownParam.RespBeam,                  "2" },
            { EDownParam.AvegTime,                  "2" },
            { EDownParam.RainPlusReportedValue,     "2" },
            { EDownParam.KC,                        "10"},
            { EDownParam.Rain,                      "4" },
            { EDownParam.Water,                     "6" },
            { EDownParam.WaterPlusReportedValue,    "2" },
            { EDownParam.SelectCollectionParagraphs,"2" },
            { EDownParam.StationType,               "2" },
            { EDownParam.UserName,                  "-1"},
            {EDownParam.StationName,                "-1"}
        };

        public static CDictionary<EDownParamGY, String> DownParamLengthMapGY = new CDictionary<EDownParamGY, String>()
        {
            { EDownParamGY.Rdata,                      "2" },
            { EDownParamGY.ArtifN,                     "2" },
            { EDownParamGY.Selement,                   "2"},
            { EDownParamGY.pumpRead,                   "2" },
            { EDownParamGY.version,                    "2" },
            { EDownParamGY.alarm,                      "2" },
            { EDownParamGY.clockset,                   "2" },
            { EDownParamGY.history,                    "2" },
            { EDownParamGY.clocksearch,                "2" },
            { EDownParamGY.oldPwd,                     "4" },
            { EDownParamGY.newPwd,                     "4" },
            { EDownParamGY.memoryReset,                "2" },
            { EDownParamGY.timeFrom_To,                "-1"},
            { EDownParamGY.DRZ,                        "3" },
            { EDownParamGY.DRP,                        "3" },
            { EDownParamGY.Step,                       "-1"},
            { EDownParamGY.basicConfigRead,            "-1"},
            { EDownParamGY.basicConfigModify,          "-1"},
            { EDownParamGY.operatingParaRead,          "2" },
            { EDownParamGY.operatingParaModify,        "2" },
            { EDownParamGY.Reset,                      "2" },
            { EDownParamGY.ICconfig,                   "6" },
            { EDownParamGY.pumpCtrl,                   "-1"},
            { EDownParamGY.valveCtrl,                  "-1"},
            { EDownParamGY.gateCtrl,                   "-1"},
            { EDownParamGY.waterYield,                 "-1"}
        };

        public static CDictionary<EDownParam, String> DownParam4ChineseMap = new CDictionary<EDownParam, string>()
        {
            { EDownParam.Clock,                     "时钟"},
            { EDownParam.NormalState,               "常规状态" },
            { EDownParam.Voltage,                   "电压" },
            { EDownParam.StationCmdID,              "站号" },
            { EDownParam.TimeChoice,                "对时选择" },
            { EDownParam.TimePeriod,                "定时段次" },
            { EDownParam.WorkStatus,                "工作状态" },
            { EDownParam.VersionNum,                "工作状态" },
            { EDownParam.StandbyChannel,            "主备信道" },
            { EDownParam.TeleNum,                   "SIM卡号"},
            { EDownParam.RingsNum,                  "振铃次数" },
            { EDownParam.DestPhoneNum,              "目的地手机号"},
            { EDownParam.TerminalNum,               "终端机号" },
            { EDownParam.RespBeam,                  "响应波束" },
            { EDownParam.AvegTime,                  "响应波束" },
            { EDownParam.RainPlusReportedValue,     "雨量加报值" },
            { EDownParam.KC,                        "KC"},
            { EDownParam.Rain,                      "雨量" },
            { EDownParam.Water,                     "水位" },
            { EDownParam.WaterPlusReportedValue,    "水位加报值" },
            { EDownParam.SelectCollectionParagraphs,"采集段次选择"},
            { EDownParam.StationType,               "测站类型" } ,
            { EDownParam.UserName,                  "用户名" } ,
            { EDownParam.StationName,               "测站名" }
        };

        public static CDictionary<EChannelType, String> ChannelType4ProtoMap = new CDictionary<EChannelType, String>()
        {
            { EChannelType.BeiDou,  "04" },
            { EChannelType.GPRS,    "06" },
            { EChannelType.GSM,     "05" },
            { EChannelType.None,    "00" },
            { EChannelType.PSTN,    "02" },
            { EChannelType.VHF,     "01" }
        };
        public static CDictionary<EChannelType, String> ChannelType4UIMap = new CDictionary<EChannelType, String>()
        {
            { EChannelType.BeiDou,  "北斗卫星" },
            { EChannelType.GPRS,    "GPRS" },
            { EChannelType.GSM,     "GSM" },
            { EChannelType.None,    "无" },
            { EChannelType.PSTN,    "PSTN" },
            { EChannelType.VHF,     "VHF" }
        };
        /// <summary>
        /// 不同类型的信道协议的起始字符
        /// 如 GSM信道 ： 起始字符为String.Empty
        ///    GPRS信道 ：  起始字符为 $
        ///   BeiDou,None,PSTV 暂时不清楚，所以用NOT_CLEAR代替
        /// </summary>
        public static CDictionary<EChannelType, String> ChannelProtocolStartCharMap = new CDictionary<EChannelType, string>()
        {
            { EChannelType.BeiDou,  "NOT_CLEAR" },
            { EChannelType.GPRS,     "$" },
            { EChannelType.GSM,    String.Empty },
            { EChannelType.None,    "NOT_CLEAR" },
            { EChannelType.PSTN,    "NOT_CLEAR" }
        };

        public static CDictionary<ETrans, String> TransMap = new CDictionary<ETrans, String>()
        {
            { ETrans.ByDay , "02" },
            { ETrans.ByHour, "03" }
        };

        public static CDictionary<EMessageType, String> MessageTypeMap = new CDictionary<EMessageType, String>()
        {
            { EMessageType.ETimed,       "22" },
            { EMessageType.EAdditional,  "21" },
            {EMessageType.Manual,        "11" }
        };

        public static CDictionary<EStationType, String> StationType4UIMap = new CDictionary<EStationType, string>()
        {
            //{ EStationType.ERiverWater, "01" },
            //{ EStationType.ERainFall,   "02" },
            //{ EStationType.EHydrology,  "03" }
            { EStationType.ERainFall,   "01" },
            { EStationType.ERiverWater, "02" },
            { EStationType.EHydrology,  "03" }
        };

        public static CDictionary<EStationType, String> StationType4ChineseMap = new CDictionary<EStationType, string>()
        {
            { EStationType.ERiverWater, "水位站" },
            { EStationType.ERainFall,   "雨量站" },
            { EStationType.EHydrology,  "水文站" }
        };

        public static CDictionary<ESelectCollectionParagraphs, String> SelectCollectionParagraphs4ProtoMap = new CDictionary<ESelectCollectionParagraphs, String>()
        {
            { ESelectCollectionParagraphs.FiveOrSix,    "01" },
            { ESelectCollectionParagraphs.TenOrTwelve,  "02" }
        };
        public static CDictionary<ESelectCollectionParagraphs, String> SelectCollectionParagraphs4UIMap = new CDictionary<ESelectCollectionParagraphs, string>()
        {
            { ESelectCollectionParagraphs.FiveOrSix,   "5/6" },
            { ESelectCollectionParagraphs.TenOrTwelve, "10/12"}
        };

        public static CDictionary<ETimePeriod, String> TimePeriodMap = new CDictionary<ETimePeriod, string>()
        {
            { ETimePeriod.One,"01" },
            { ETimePeriod.Two,"02" },
            { ETimePeriod.Four,"04" },
            { ETimePeriod.Six,"06" },
            { ETimePeriod.Eight,"08" },
            { ETimePeriod.Twelve,"12" },
            { ETimePeriod.TwentyFour,"24" },
            { ETimePeriod.FourtyEight,"48" }
        };

    }

    public class CDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public TValue FindValue(TKey key)
        {
            if (this.ContainsKey(key))
            {
                return this[key];
            }
            throw new Exception(String.Format("{0}不在映射字典中！", key));
        }
        public TKey FindKey(TValue value)
        {
            foreach (var item in this)
            {
                if (item.Value.Equals(value))
                    return item.Key;
            }
            throw new Exception(String.Format("{0}不在映射字典中！", value));
        }
    }

    public class ProtocolHelpers
    {
        public static bool DeleteSpecialChar(string oldStr, out string newStr)
        {
            //  删除起始符 '$'
            if (oldStr.StartsWith(CSpecialChars.DOLLAR_CHAR.ToString()))
                oldStr = oldStr.Substring(1);
            //  删除结束符 '\r'
            if (oldStr.EndsWith(CSpecialChars.ENTER_CHAR.ToString()))
                oldStr = oldStr.Replace(CSpecialChars.ENTER_CHAR, CSpecialChars.BALNK_CHAR);
            //  删除起始位置的空字符 ' '
            oldStr = oldStr.Trim();
            newStr = oldStr;
            return newStr.Length > 0;
        }

        public static EStationType ProtoStr2StationType(String str)
        {
            var pStationType = ProtocolMaps.StationType4ProtoMap.FindKey(str);
            string type = string.Empty;
            switch (pStationType)
            {
                case EStationTypeProto.EParallelEHydrology:
                case EStationTypeProto.ESerialEHydrology:
                case EStationTypeProto.EParallelSpecial:
                case EStationTypeProto.ESerialSpecial:
                    type = "03"; break;
                case EStationTypeProto.EParallelRiverWater:
                case EStationTypeProto.ESerialRiverWater:
                    //type = "01"; break;
                    type = "02"; break;
                case EStationTypeProto.ERainFall:
                    //type = "02"; break;
                    type = "01"; break;
                default:
                    throw new Exception("站点类型转换错误");
            }
            return ProtocolMaps.StationType4UIMap.FindKey(type);
        }

        public static String StationType2ProtoStr(EStationType type)
        {
            string str = string.Empty;
            switch (type)
            {
                case EStationType.EHydrology:
                case EStationType.ERiverWater:
                    str = ProtocolMaps.StationType4UIMap.FindValue(EStationType.ERiverWater);
                    break;
                case EStationType.ERainFall:
                    str = ProtocolMaps.StationType4UIMap.FindValue(EStationType.ERainFall);
                    break;
            }
            return str;
        }
        public static String StationType2ProtoStr_set(EStationType type)
        {
            string str = string.Empty;
            switch (type)
            {
                case EStationType.EHydrology:
                    str = ProtocolMaps.StationType4UIMap.FindValue(EStationType.EHydrology);
                    break;
                case EStationType.ERiverWater:
                    str = ProtocolMaps.StationType4UIMap.FindValue(EStationType.ERiverWater);
                    break;
                case EStationType.ERainFall:
                    str = ProtocolMaps.StationType4UIMap.FindValue(EStationType.ERainFall);
                    break;
            }
            return str;
        }

        public static String StationType2ProtoStr_1(EStationType type)
        {
            string str = string.Empty;
            switch (type)
            {
                case EStationType.EHydrology:
                case EStationType.ERiverWater:
                    str = ProtocolMaps.StationType4UIMap.FindValue(EStationType.ERainFall);
                    break;
                case EStationType.ERainFall:
                    str = ProtocolMaps.StationType4UIMap.FindValue(EStationType.ERiverWater);
                    break;
            }
            return str;
        }
    }
}
