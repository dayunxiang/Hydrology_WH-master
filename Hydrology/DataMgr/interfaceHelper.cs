/************************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Hydrology.DataMgr
*文件名： interfaceHelper
*创建人： XXX
*创建时间：2019-1-25 9:40:53
*描述
*=====================================================================
*修改标记
*修改时间：2019-1-25 9:40:53
*修改人：XXX
*描述：
************************************************************************************/
using Hydrology.DBManager;
using Hydrology.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Hydrology.DataMgr
{
    public class interfaceHelper
    {

        #region 单件模式
        private static interfaceHelper m_sInstance;   //实例指针
        
        public static interfaceHelper Instance
        {
            get { return GetInstance(); }
        }
        public static interfaceHelper GetInstance()
        {
            if (m_sInstance == null)
            {
                m_sInstance = new interfaceHelper();
            }
            return m_sInstance;
        }
        #endregion ///<单件模式


        #region 定时器
        private System.Timers.Timer m_addRain;
        private System.Timers.Timer m_addWater;
        private System.Timers.Timer m_addVoltage;
        #endregion

        #region 变量
        List<CEntityRain> rainList2DB = new List<CEntityRain>();
        #endregion



        #region 常量
        private int addToDBDelay = 60 * 1000;
        private string suffix  =  "http://127.0.0.1:8088/";
        #endregion
        

        private interfaceHelper()
        {
            m_addRain = new System.Timers.Timer();
            m_addRain.Elapsed += new System.Timers.ElapsedEventHandler(insertRainListTriger);
            m_addRain.Enabled = false;
            m_addRain.Interval = addToDBDelay;

        }
        #region 雨量
        public void insertRainListTriger(object sender, ElapsedEventArgs e)
        {
            CDBLog.Instance.AddInfo(string.Format("新增雨量触发器"));
            string url = suffix + "rain/insertRain";
            Dictionary<string, string> param = new Dictionary<string, string>();
            if (rainList2DB != null || rainList2DB.Count > 0)
            {
                //string rainList2DBStr = HttpHelper.ObjectToJson(rainList2DB);
                //param["rain"] = rainList2DBStr;
                //string resultStr = HttpHelper.Post(url, param);
                //rainList2DB.Clear();
            }

        }
        public  int insertRainList(List<CEntityRain> rainList)
        {
            m_addRain.Start();
            CDBLog.Instance.AddInfo(string.Format("新增雨量触发器开始"));
            string url = suffix + "rain/insertRain";
            Dictionary<string, string> param = new Dictionary<string, string>();
            int result = 0;
            if(rainList == null || rainList.Count == 0)
            {
                return 0;
            }
            rainList2DB.AddRange(rainList);
            if(rainList.Count >= 500)
            {
                string rainList2DBStr = HttpHelper.ObjectToJson(rainList2DB);
                param["rain"] = rainList2DBStr;
                string resultStr = HttpHelper.Post(url, param);
                rainList2DB.Clear();
            }

            return result;
        }
        #endregion


    }
}