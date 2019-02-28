using System;

namespace Hydrology.Entity
{
    public class CEntityOnlineState
    {
        #region PROPERTY

        /// <summary>
        /// 分中心ID
        /// </summary>
        public String subcenterId { get; set; }

       /// <summary>
       /// 分中心名称
       /// </summary>
        public String subcenterName { get; set; }

        /// <summary>
        /// 站点名称
        /// </summary>
        public String stationName { get; set; }

        /// <summary>
        /// 站点ID
        /// </summary>
        public String stationId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public String userId { get; set; }
       
        /// <summary>
        /// sim卡号
        /// </summary>
        public String sim { get; set; }

        /// <summary>
        /// ip
        /// </summary>
        public String ip { get; set; }


        /// <summary>
        /// 连接时间
        /// </summary>
        public String connctTime { get; set; }

        /// <summary>
        /// 刷新时间
        /// </summary>
        public String refreshTime { get; set; }

        /// <summary>
        /// 在线状态
        /// </summary>
        public String state { get; set; }

        #endregion
    }
}
