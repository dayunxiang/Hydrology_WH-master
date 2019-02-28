using System;

namespace Hydrology.Entity
{
    public class CGprsEventArgs : EventArgs
    {
        public Object Data { get; set; }
    }

    public class UpEventArgs : EventArgs
    {
        public CReportStruct Value { get; set; }
        /// <summary>
        /// 原始数据
        /// </summary>
        public string RawData { get; set; }
    }

    public class DownEventArgs : EventArgs
    {
        public CDownConf Value { get; set; }
        /// <summary>
        /// 原始数据
        /// </summary>
        public string RawData { get; set; }
    }

    public class BatchEventArgs : EventArgs
    {
        public CBatchStruct Value { get; set; }
        /// <summary>
        /// 原始数据
        /// </summary>
        public string RawData { get; set; }
    }

    public class ReceiveErrorEventArgs : EventArgs
    {
        public string Msg { get; set; }
    }

    public class ReceivedTimeOutEventArgs : EventArgs
    {
        public int Second { get; set; }
    }
}
