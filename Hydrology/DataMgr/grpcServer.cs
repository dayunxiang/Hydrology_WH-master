using Grpc.Core;
using Grpcservices;
using Hydrology.Entity;
using Hydrology.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;

namespace Hydrology.DataMgr
{
    public class grpcServer : gRPCServices.gRPCServicesBase
    {
        private string gprsNum;
        private string stationId;
        private string cmds;
        private bool isSet;
        private string grpcIp;
        private int grpcPort;
        bool downdataGet = false;
        bool timeDown = false;
        bool timeDown_1 = false;
        //bool notOL = false;

        readonly object myLock = new object();
        readonly Dictionary<BatchRequest, List<BatchData>> batchTrans = new Dictionary<BatchRequest, List<BatchData>>();
        private BatchMsg batchMsg;
        private List<DownConf> downConfList;
        private List<BatchData> batchList;
        public List<BatchData> GetBatchList
        {
            get
            {
                if (batchList == null)
                {
                    batchList = new List<BatchData>();
                }
                return batchList;
            }
        }

        private System.Timers.Timer m_timer = new System.Timers.Timer()
        {
            Enabled = true,
            Interval = 10 * 1000
        };

        private System.Timers.Timer m_timer_1 = new System.Timers.Timer()
        {
            Enabled = true,
            Interval = 30 * 1000
        };

        private static grpcServer instance;
        public static grpcServer Instance
        {
            get
            {
                if (instance == null)
                    instance = new grpcServer();
                return instance;
            }
        }

        #region 帮助方法
        public static List<T> Clone<T>(object List)
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, List);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as List<T>;
            }
        }
        #endregion

        #region 初始化grpc服务器

        public void InitGrpcServer()
        {
            // 这个地方非常有疑问
            //var features = downConfList;
            XmlDocument doc = new XmlDocument();
            doc.Load("Config/grpc.xml");
            XmlNode xn = doc.SelectSingleNode("grpc");
            XmlNodeList xnl = xn.ChildNodes;
            grpcIp = xnl.Item(0).InnerText;
            grpcPort = int.Parse(xnl.Item(1).InnerText);

            try
            {
                Server server = new Server(new List<ChannelOption> { new ChannelOption(ChannelOptions.MaxSendMessageLength, int.MaxValue) })

                {
                    Services = { gRPCServices.BindService(Instance) },
                    Ports = { new ServerPort(grpcIp, grpcPort, ServerCredentials.Insecure) }
                };

                server.Start();

                this.downConfList = new List<DownConf>();

                m_timer.Elapsed += new ElapsedEventHandler(TimeElapsed);
                m_timer.Stop();
                m_timer_1.Elapsed += new ElapsedEventHandler(TimeElapsed_1);
                m_timer_1.Stop();
                CProtocolEventManager.DownForData += this.DownData_EventHandler;
                CProtocolEventManager.ErrorForData += this.ErrorData_EventHandler;
                CProtocolEventManager.BatchForData += this.BatchForData_EventHandler;

                //server.ShutdownAsync().Wait();

                CSystemInfoMgr.Instance.AddInfo(string.Format("配置...grpcServer...完成!"));
            }
            catch (Exception)
            {
                CSystemInfoMgr.Instance.AddInfo(string.Format("配置...grpcServer...失败!"));
            }
        }

        public void StopGrpcServer()
        {
            this.downConfList = null;
            CProtocolEventManager.DownForData -= this.DownData_EventHandler;
            CProtocolEventManager.ErrorForData -= this.ErrorData_EventHandler;
            CProtocolEventManager.BatchForData -= this.BatchForData_EventHandler;
            instance = null;
        }

        void TimeElapsed(object sender, EventArgs e)
        {
            if (!timeDown)
            {
                timeDown = true;
            }
        }

        void TimeElapsed_1(object sender, EventArgs e)
        {
            if (!timeDown_1)
            {
                timeDown_1 = true;
            }
        }

        #endregion

        #region 远程参数设置

        /// <summary>
        /// get方法  据说是客户端可以调用的
        /// </summary>
        /// <param name="DownRequest"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<DownConf> GetFeature(DownRequest DownRequest, ServerCallContext context)
        {
            return Task.FromResult(GetDownConf(DownRequest));
        }

        /// <summary>
        /// 处理请求方法，向终端发送请求并返回上行数据
        /// </summary>
        /// <param name="DownRequest"></param>
        /// <returns></returns>
        private DownConf GetDownConf(DownRequest DownRequest)
        {
            m_timer.Start();
            DownConf result = new DownConf();
            downConfList = null;
            downConfList = new List<DownConf>();
            downdataGet = false;
            string downQuery = string.Empty;
            gprsNum = DownRequest.GprsNum;
            stationId = DownRequest.StationId;
            cmds = DownRequest.Cmds;
            isSet = DownRequest.IsSet;

            if (isSet == false)
            {
                List<EDownParam> query = new List<EDownParam>();

                for (int i = cmds.Length; i > 0; i = i - 2)
                {

                    string downParamValue = cmds.Substring(0, 2);
                    EDownParam downParamKey = ProtocolMaps.DownParamMap.FindKey(downParamValue);
                    query.Add(downParamKey);
                    cmds = cmds.Substring(2);
                }

                downQuery = CPortDataMgr.Instance.SendGprsRead_BS(gprsNum, stationId, query);

                if (downQuery == "")
                {
                    result.IsOL = false;
                    return result;
                }
            }
            else if (isSet == true)
            {
                StringBuilder query = new StringBuilder();
                query.Append(ProtocolMaps.ChannelProtocolStartCharMap.FindValue(EChannelType.GPRS));
                query.Append(String.Format("{0:D4}", Int32.Parse(stationId.Trim())));
                query.Append("0S");

                query.Append(cmds);
                //while(cmds.Length > 0)
                //{
                //    try
                //    {
                //        query.Append(CSpecialChars.BALNK_CHAR);
                //        string downParamValue = cmds.Substring(0, 2);
                //        query.Append(downParamValue);
                //        EDownParam downParamKey = ProtocolMaps.DownParamMap.FindKey(downParamValue);
                //        int downParamLength = int.Parse(ProtocolMaps.DownParamLengthMap.FindValue(downParamKey));
                //        string downData = cmds.Substring(2, downParamLength);
                //        query.Append(downData);
                //        cmds = cmds.Substring(2 + downParamLength);
                //    }
                //    catch (Exception e)
                //    {
                //        Debug.WriteLine("下行指令解析失败" + e.Message);
                //    }
                //}

                query.Append(CSpecialChars.ENTER_CHAR);

                var gprs = CPortDataMgr.Instance.FindGprsByUserid(gprsNum);
                if (gprs != null)
                {
                    uint dtuID = 0;
                    if (gprs.FindByID(gprsNum, out dtuID))
                    {
                        gprs.SendDataTwice(dtuID, query.ToString());
                    }
                    else
                    {
                        result.IsOL = false;
                        return result;
                    }
                }
                else
                {
                    result.IsOL = false;
                    return result;
                }
            }

            result = CheckDownConf(DownRequest);

            while (!downdataGet)
            {
                Thread.Sleep(1000);
                result = CheckDownConf(DownRequest);
                if (timeDown)
                {
                    timeDown = false;
                    break;
                }
            }

            m_timer.Stop();

            return result;
        }



        //private void StationNotOL_EventHandler(object sender, DownEventArgs e)
        //{
        //    if(!notOL) notOL = true;
        //}

        /// <summary>
        /// 上行数据处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownData_EventHandler(object sender, DownEventArgs e)
        {
            try
            {
                CDownConf info = e.Value;
                DownConf downConf = new DownConf();
                if (info == null)
                    return;
                if (info.Clock.HasValue)
                {
                    downConf.Clock = info.Clock.ToString();
                }
                if (info.NormalState.HasValue)
                {
                    if (info.NormalState == ENormalState.GPRS) downConf.NormalState = DownConf.Types.NormalState.GprsState;
                    if (info.NormalState == ENormalState.GSM) downConf.NormalState = DownConf.Types.NormalState.GsmState;
                }
                if (info.Voltage.HasValue)
                {
                    downConf.Voltage = (double)info.Voltage;
                }
                if (!String.IsNullOrEmpty(info.StationCmdID))
                {
                    downConf.StationCmdID = info.StationCmdID;
                }
                if (info.TimeChoice.HasValue)
                {
                    if (info.TimeChoice == ETimeChoice.AdjustTime) downConf.TimeChoice = DownConf.Types.TimeChoice.AdjustTime;
                    if (info.TimeChoice == ETimeChoice.Two) downConf.TimeChoice = DownConf.Types.TimeChoice.NoAdjustTime;
                }
                if (info.TimePeriod.HasValue)
                {
                    if (info.TimePeriod == ETimePeriod.One) downConf.TimePeriod = DownConf.Types.TimePeriod.One;
                    if (info.TimePeriod == ETimePeriod.Two) downConf.TimePeriod = DownConf.Types.TimePeriod.Two;
                    if (info.TimePeriod == ETimePeriod.Four) downConf.TimePeriod = DownConf.Types.TimePeriod.Four;
                    if (info.TimePeriod == ETimePeriod.Six) downConf.TimePeriod = DownConf.Types.TimePeriod.Six;
                    if (info.TimePeriod == ETimePeriod.Eight) downConf.TimePeriod = DownConf.Types.TimePeriod.Eight;
                    if (info.TimePeriod == ETimePeriod.Twelve) downConf.TimePeriod = DownConf.Types.TimePeriod.Twelve;
                    if (info.TimePeriod == ETimePeriod.TwentyFour) downConf.TimePeriod = DownConf.Types.TimePeriod.TwentyFour;
                    if (info.TimePeriod == ETimePeriod.FourtyEight) downConf.TimePeriod = DownConf.Types.TimePeriod.FourtyEight;
                }
                if (info.WorkStatus.HasValue)
                {
                    if (info.WorkStatus == EWorkStatus.Debug)
                        downConf.WorkStatus = DownConf.Types.WorkStatus.Debug;
                    if (info.WorkStatus == EWorkStatus.DoubleAddress)
                        downConf.WorkStatus = DownConf.Types.WorkStatus.DoubleAddress;
                    if (info.WorkStatus == EWorkStatus.Normal)
                        downConf.WorkStatus = DownConf.Types.WorkStatus.Normal;
                }
                if (!String.IsNullOrEmpty(info.VersionNum))
                {
                    downConf.VersionNum = info.VersionNum;
                }
                if (info.MainChannel.HasValue && info.ViceChannel.HasValue)
                {
                    try
                    {
                        if (info.MainChannel == EChannelType.BeiDou) downConf.MainChannel = DownConf.Types.ChannelType.BeiDou;
                        if (info.MainChannel == EChannelType.Beidou500) downConf.MainChannel = DownConf.Types.ChannelType.Beidou500;
                        if (info.MainChannel == EChannelType.BeidouNormal) downConf.MainChannel = DownConf.Types.ChannelType.BeidouNormal;
                        if (info.MainChannel == EChannelType.GPRS) downConf.MainChannel = DownConf.Types.ChannelType.Gprs;
                        if (info.MainChannel == EChannelType.GSM) downConf.MainChannel = DownConf.Types.ChannelType.Gsm;
                        if (info.MainChannel == EChannelType.None) downConf.MainChannel = DownConf.Types.ChannelType.None;
                        if (info.MainChannel == EChannelType.PSTN) downConf.MainChannel = DownConf.Types.ChannelType.Pstn;
                        if (info.MainChannel == EChannelType.VHF) downConf.MainChannel = DownConf.Types.ChannelType.Vhf;

                    }
                    catch (Exception exp) { Debug.WriteLine(exp.Message); }
                    try
                    {
                        if (info.ViceChannel == EChannelType.BeiDou) downConf.ViceChannel = DownConf.Types.ChannelType.BeiDou;
                        if (info.ViceChannel == EChannelType.Beidou500) downConf.ViceChannel = DownConf.Types.ChannelType.Beidou500;
                        if (info.ViceChannel == EChannelType.BeidouNormal) downConf.ViceChannel = DownConf.Types.ChannelType.BeidouNormal;
                        if (info.ViceChannel == EChannelType.GPRS) downConf.ViceChannel = DownConf.Types.ChannelType.Gprs;
                        if (info.ViceChannel == EChannelType.GSM) downConf.ViceChannel = DownConf.Types.ChannelType.Gsm;
                        if (info.ViceChannel == EChannelType.None) downConf.ViceChannel = DownConf.Types.ChannelType.None;
                        if (info.ViceChannel == EChannelType.PSTN) downConf.ViceChannel = DownConf.Types.ChannelType.Pstn;
                        if (info.ViceChannel == EChannelType.VHF) downConf.ViceChannel = DownConf.Types.ChannelType.Vhf;

                    }
                    catch (Exception exp) { Debug.WriteLine(exp.Message); }
                }
                if (!String.IsNullOrEmpty(info.TeleNum))
                {
                    downConf.TeleNum = info.TeleNum;
                }
                if (info.RingsNum.HasValue)
                {
                    downConf.RingsNum = (double)info.RingsNum;
                }
                if (!String.IsNullOrEmpty(info.DestPhoneNum))
                {
                    downConf.DestPhoneNum = info.DestPhoneNum;
                }
                if (!String.IsNullOrEmpty(info.TerminalNum))
                {
                    downConf.TerminalNum = info.TerminalNum;
                }
                if (!String.IsNullOrEmpty(info.RespBeam))
                {
                    downConf.RespBeam = info.RespBeam;
                }
                if (info.AvegTime.HasValue)
                {
                    downConf.AvegTime = (double)info.AvegTime;
                }
                if (info.RainPlusReportedValue.HasValue)
                {
                    downConf.RainPlusReportedValue = (double)info.RainPlusReportedValue;
                }
                if (!String.IsNullOrEmpty(info.KC))
                {
                    downConf.KC = info.KC;
                    // KC值长度为20时，前十位为k，后十位为C
                }
                if (info.Rain.HasValue)
                {
                    downConf.Rain = (double)info.Rain;
                }
                if (info.Water.HasValue)
                {
                    downConf.Water = (double)info.Water;
                }
                if (info.WaterPlusReportedValue.HasValue)
                {
                    downConf.WaterPlusReportedValue = (double)info.WaterPlusReportedValue;
                }
                if (info.SelectCollectionParagraphs.HasValue)
                {
                    if (info.SelectCollectionParagraphs == ESelectCollectionParagraphs.FiveOrSix) downConf.Select = DownConf.Types.SelectCollectionParagraphs.FiveOrSix;
                    if (info.SelectCollectionParagraphs == ESelectCollectionParagraphs.TenOrTwelve) downConf.Select = DownConf.Types.SelectCollectionParagraphs.TenOrTwelve;
                }
                if (!String.IsNullOrEmpty(info.UserName))
                {
                    downConf.UserName = info.UserName;
                }
                if (!String.IsNullOrEmpty(info.StationName))
                {
                    downConf.StationName = info.StationName;
                }

                if (downConf != null)
                {
                    this.downConfList.Add(downConf);
                }
            }
            catch (Exception exp)
            {
                Debug.WriteLine("" + exp.Message);
            }
        }

        private void ErrorData_EventHandler(object sender, ReceiveErrorEventArgs e)
        {
            try
            {
                DownConf downConf = new DownConf();

                string msg = e.Msg;
                if (msg.Contains("TRU") && msg.Contains(stationId))
                {
                    downConf.TRU = true;
                    downConfList.Add(downConf);
                }
            }
            catch (Exception exp)
            {
                Debug.WriteLine("" + exp.Message);
            }
        }

        /// <summary>
        /// 检查上行数据列表中是否存在对应请求的数据
        /// </summary>
        /// <param name="DownRequest"></param>
        /// <returns></returns>
        private DownConf CheckDownConf(DownRequest DownRequest)
        {
            DownConf checkedDownConf = new DownConf();
            List<DownConf> downDataList = this.downConfList;
            foreach (var downdata in downDataList)
            {
                if (downdata.TRU == true)
                {
                    checkedDownConf = downdata;
                    checkedDownConf.IsOL = true;
                    downdataGet = true;
                    break;
                }
                if (downdata.StationCmdID == DownRequest.StationId)
                {
                    checkedDownConf = downdata;
                    checkedDownConf.IsOL = true;
                    downdataGet = true;
                    break;
                }
            }
            return checkedDownConf;
        }

        #endregion

        #region 批量传输
        public override async Task ListBatchData(BatchRequest request, IServerStreamWriter<BatchData> responseStream, ServerCallContext context)
        {
            List<BatchData> batchLists = GetBatchData(request);
            foreach (var batchData in batchLists)
            {
                await responseStream.WriteAsync(batchData);
            }
        }

        public override Task<BatchMsg> GetBatchMsg(BatchRequest request, ServerCallContext context)
        {
            return Task.FromResult(GetBatchMsg(request));
        }

        private BatchMsg GetBatchMsg(BatchRequest request)
        {
            m_timer_1.Start();
            SendBatchMsg(request);
            if (batchMsg.NotOL != false)
            {
                m_timer.Stop();
                return batchMsg;
            }
            while (true)
            {
                if (batchMsg.Msg != "")
                {
                    break;
                }
                Thread.Sleep(1000);
                if (timeDown_1)
                {
                    batchMsg = new BatchMsg() { TimeOut = true };
                    timeDown_1 = false;
                    break;
                }
            }
            m_timer_1.Stop();
            return batchMsg;
        }

        private List<BatchData> GetBatchData(BatchRequest batchRequest)
        {
            lock (myLock)
            {
                List<BatchData> datas = new List<BatchData>();
                m_timer_1.Start();
                SendBatchMsg(batchRequest);
                if (this.batchList.Count() != 0)
                {
                    if (this.batchList.First().NotOL == true)
                    {
                        m_timer_1.Stop();
                        return this.GetBatchList;
                    }
                    else
                    {
                        datas = CheckBatchData(batchRequest, this.batchList);
                        return datas;
                    }
                }
                else
                {
                    // 此处获取datas
                    while (true)
                    {
                        datas = CheckBatchData(batchRequest, this.batchList);
                        if (datas.Count() != 0)
                        {
                            break;
                        }
                        Thread.Sleep(1000);
                        if (timeDown_1)
                        {
                            timeDown_1 = false;
                            break;
                        }
                    }
                }
                m_timer_1.Stop();
                if (datas.Count() == 0)
                {
                    BatchData error = new BatchData();
                    error.StationId = batchRequest.StationId;
                    error.TimeOut = true;
                    datas.Add(error);
                    BatchData endInfo = new BatchData();
                    endInfo.StationId = endInfo.StationId;
                    endInfo.End = true;
                    datas.Add(endInfo);
                }
                return datas;

            }
        }

        private List<BatchData> CheckBatchData(BatchRequest request, List<BatchData> batchDatas)
        {
            List<BatchData> checkedbatchList = new List<BatchData>();
            int i = 0;
            if (batchDatas.Count() == 0)
            {
                return checkedbatchList;
            }
            foreach (var v in batchDatas)
            {
                i++;
                checkedbatchList.Add(v);
                if (v.End == true && v.StationId == request.StationId)
                {
                    break;
                }
            }
            GetBatchList.RemoveRange(0, i);
            return checkedbatchList;
        }
        private void SendBatchMsg(BatchRequest request)
        {
            this.batchMsg = null;
            this.batchMsg = new BatchMsg() { };
            this.batchList = new List<BatchData>();
            EStationType stype = request.ReportType == true ? EStationType.ERainFall : EStationType.EHydrology;
            ETrans trans = request.TransType == true ? ETrans.ByDay : ETrans.ByHour;
            DateTime st = Convert.ToDateTime(request.StartTime);
            DateTime et = Convert.ToDateTime(request.EndTime);
            DateTime beginTime;
            DateTime endTime;
            if (trans == ETrans.ByHour)
            {
                beginTime = new DateTime(
                year: st.Year,
                month: st.Month,
                day: st.Day,
                hour: st.Hour,
                minute: 0,
                second: 0

            );
                endTime = new DateTime(
                    year: et.Year,
                    month: et.Month,
                    day: et.Day,
                    hour: et.Hour,
                    minute: 0,
                    second: 0

                );
            }
            else
            {
                beginTime = new DateTime(
                year: st.Year,
                month: st.Month,
                day: st.Day,
                hour: 8,
                minute: 0,
                second: 0

            );
                endTime = new DateTime(
                     year: st.Year,
                     month: et.Month,
                     day: et.Day,
                     hour: 8,
                     minute: 0,
                     second: 0

                 );
            }

            string query = string.Empty;
            var gprs = CPortDataMgr.Instance.FindGprsByUserid(request.Gprsid);
            if (gprs != null)
            {
                uint dtuID = 0;
                if (gprs.FindByID(request.Gprsid, out dtuID))
                {
                    query = gprs.FlashBatch.BuildQuery(request.StationId, stype, trans, beginTime, endTime, EChannelType.GPRS);
                    gprs.SendDataTwiceForBatchTrans(dtuID, query);
                }
            }
            else
            {
                batchMsg = new BatchMsg() { NotOL = true };
                BatchData error = new BatchData();
                error.StationId = request.StationId;
                error.NotOL = true;
                this.batchList.Add(error);
                BatchData endInfo = new BatchData();
                endInfo.StationId = endInfo.StationId;
                endInfo.End = true;
                this.batchList.Add(endInfo);
            }
        }

        private void BatchForData_EventHandler(object sender, BatchEventArgs e)
        {
            try
            {
                batchMsg = new BatchMsg() { Msg = e.RawData };
                CBatchStruct info = e.Value;
                if (info == null)
                    return;
                BatchData batchInfo = new BatchData();
                batchInfo.StationId = info.StationID;
                if (info.StationType == EStationType.ERainFall)
                {
                    batchInfo.StationType = true;
                }
                else
                {
                    batchInfo.StationType = false;
                }
                this.GetBatchList.Add(batchInfo);

                foreach (var v in info.Datas)
                {
                    BatchData dataInfo = new BatchData();
                    dataInfo.Time = v.Time.ToString();
                    dataInfo.Data = v.Data;
                    this.GetBatchList.Add(dataInfo);
                }

                BatchData endInfo = new BatchData();
                endInfo.StationId = endInfo.StationId;
                endInfo.End = true;
                this.GetBatchList.Add(endInfo);
            }
            catch (Exception exp)
            {
                Debug.WriteLine("" + exp.Message);
            }
        }

        #endregion

        #region 获取在线列表

        public override Task<DtuList> GetDtuList(Subcenter subcenter, ServerCallContext context)
        {
            return Task.FromResult(FindDtuList(subcenter));
        }

        private DtuList FindDtuList(Subcenter subcenter)
        {
            DtuList dtuList = new DtuList();

            List<ModemInfoStruct> stateList = new List<ModemInfoStruct>();
            List<CEntityStation> stations = new List<CEntityStation>();
            List<CEntitySoilStation> soilStations = new List<CEntitySoilStation>();
            List<CEntitySubCenter> subCenters = new List<CEntitySubCenter>();
            Dictionary<string, ModemInfoStruct> gprsDic = new Dictionary<string, ModemInfoStruct>();

            stateList = Clone<ModemInfoStruct>(CProtocolEventManager.Instance.GetOnlineStatusList());
            stations = CDBDataMgr.Instance.GetAllStation();
            soilStations = CDBSoilDataMgr.Instance.GetAllSoilStation();
            subCenters = CDBDataMgr.Instance.GetAllSubCenter();

            if (stateList.Count() != 0)
            {
                for (int i = 0; i < stateList.Count(); i++)
                {
                    string uid = ((uint)stateList[i].m_modemId).ToString("X").PadLeft(8, '0');
                    gprsDic.Add(uid, stateList[i]);
                }
            }

            foreach (var s in stations)
            {
                if (subcenter.SubcenterdId == s.SubCenterID.ToString() || subcenter.SubcenterdId == "0")
                {
                    if (gprsDic.Count() != 0)
                    {
                        if (gprsDic.ContainsKey(s.GPRS))
                        {
                            Dtu dtu = new Dtu();

                            ModemInfoStruct state = gprsDic[s.GPRS];
                            string phoneno = CGprsUtil.Byte11ToPhoneNO(state.m_phoneno, 0);
                            string dynIP = CGprsUtil.Byte4ToIP(state.m_dynip, 0);
                            string connectTime = CGprsUtil.ULongToDatetime(state.m_conn_time).ToString();
                            string refreshTime = CGprsUtil.ULongToDatetime(state.m_refresh_time).ToString();
                            
                            dtu.SubcenterId = s.SubCenterID.ToString();
                            string subName = CDBDataMgr.Instance.GetSubCenterName(s.SubCenterID.ToString());
                            dtu.SubcenterName = subName;
                            dtu.StationId = s.StationID;
                            dtu.StationName = s.StationName;
                            dtu.GprsId = s.GPRS;
                            dtu.GsmNum = phoneno;
                            dtu.IpAddr = dynIP;
                            dtu.ConnTime = connectTime;
                            dtu.RefreshTime = refreshTime;
                            dtu.State = "1";
                            dtu.StationType = CEnumHelper.StationTypeToDBStr(s.StationType);
                            dtuList.Dtu.Add(dtu);
                        }
                    }
                    else
                    {
                        Dtu dtu = new Dtu();
                        dtu.SubcenterId = s.SubCenterID.ToString();
                        string subName = CDBDataMgr.Instance.GetSubCenterName(s.SubCenterID.ToString());
                        dtu.SubcenterName = subName;
                        dtu.StationId = s.StationID;
                        dtu.StationName = s.StationName;
                        dtu.GprsId = s.GPRS;
                        dtu.State = "2";
                        dtu.StationType = CEnumHelper.StationTypeToDBStr(s.StationType);
                        dtuList.Dtu.Add(dtu);
                    }
                }
            }

            foreach (var s in stations)
            {
                if (subcenter.SubcenterdId == s.SubCenterID.ToString() || subcenter.SubcenterdId == "0")
                {
                    if (gprsDic.Count() != 0)
                    {
                        if (!gprsDic.ContainsKey(s.GPRS))
                        {
                            Dtu dtu = new Dtu();
                            dtu.SubcenterId = s.SubCenterID.ToString();
                            string subName = CDBDataMgr.Instance.GetSubCenterName(s.SubCenterID.ToString());
                            dtu.SubcenterName = subName;
                            dtu.StationId = s.StationID;
                            dtu.StationName = s.StationName;
                            dtu.GprsId = s.GPRS;
                            dtu.State = "2";
                            dtu.StationType = CEnumHelper.StationTypeToDBStr(s.StationType);
                            dtuList.Dtu.Add(dtu);
                        }
                    }
                }
            }

            foreach (var s in soilStations)
            {
                if (subcenter.SubcenterdId == s.SubCenterID.ToString() || subcenter.SubcenterdId == "0")
                {
                    if (gprsDic.Count() != 0)
                    {
                        if (gprsDic.ContainsKey(s.GPRS))
                        {
                            Dtu dtu = new Dtu();

                            ModemInfoStruct state = gprsDic[s.GPRS];
                            string phoneno = CGprsUtil.Byte11ToPhoneNO(state.m_phoneno, 0);
                            string dynIP = CGprsUtil.Byte4ToIP(state.m_dynip, 0);
                            string connectTime = CGprsUtil.ULongToDatetime(state.m_conn_time).ToString();
                            string refreshTime = CGprsUtil.ULongToDatetime(state.m_refresh_time).ToString();

                            if (subcenter.SubcenterdId == s.SubCenterID.ToString() || subcenter.SubcenterdId == "0")
                            {
                                dtu.SubcenterId = s.SubCenterID.ToString();
                                string subName = CDBDataMgr.Instance.GetSubCenterName(s.SubCenterID.ToString());
                                dtu.SubcenterName = subName;
                                dtu.StationId = s.StationID;
                                dtu.StationName = s.StationName;
                                dtu.GprsId = s.GPRS;
                                dtu.GsmNum = phoneno;
                                dtu.IpAddr = dynIP;
                                dtu.ConnTime = connectTime;
                                dtu.RefreshTime = refreshTime;
                                dtu.State = "1";
                                dtu.StationType = CEnumHelper.StationTypeToDBStr(s.StationType);
                                dtuList.Dtu.Add(dtu);
                            }
                        }
                    }
                    else
                    {
                        Dtu dtu = new Dtu();
                        dtu.SubcenterId = s.SubCenterID.ToString();
                        string subName = CDBDataMgr.Instance.GetSubCenterName(s.SubCenterID.ToString());
                        dtu.SubcenterName = subName;
                        dtu.StationId = s.StationID;
                        dtu.StationName = s.StationName;
                        dtu.GprsId = s.GPRS;
                        dtu.State = "2";
                        dtu.StationType = CEnumHelper.StationTypeToDBStr(s.StationType);
                        dtuList.Dtu.Add(dtu);
                    }
                }
            }

            foreach (var s in soilStations)
            {
                if (subcenter.SubcenterdId == s.SubCenterID.ToString() || subcenter.SubcenterdId == "0")
                {
                    if (gprsDic.Count() != 0)
                    {
                        if (!gprsDic.ContainsKey(s.GPRS))
                        {
                            Dtu dtu = new Dtu();
                            dtu.SubcenterId = s.SubCenterID.ToString();
                            string subName = CDBDataMgr.Instance.GetSubCenterName(s.SubCenterID.ToString());
                            dtu.SubcenterName = subName;
                            dtu.StationId = s.StationID;
                            dtu.StationName = s.StationName;
                            dtu.GprsId = s.GPRS;
                            dtu.State = "2";
                            dtu.StationType = CEnumHelper.StationTypeToDBStr(s.StationType);
                            dtuList.Dtu.Add(dtu);
                        }
                    }
                }
            }

            return dtuList;
        }

        #endregion

        #region 批量对时
        //public override Task<TruList> BatchTime(StationList list, ServerCallContext context)
        //{
        //    return Task.FromResult(BatchTime(list));
        //}

        //public TruList BatchTime(StationList list)
        //{
        //    return null;
        //}

        #endregion
    }
}
