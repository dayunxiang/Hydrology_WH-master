/************************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Hydrology.DataMgr
*文件名： HttpHelper
*创建人： XXX
*创建时间：2019-1-25 9:24:34
*描述
*=====================================================================
*修改标记
*修改时间：2019-1-25 9:24:34
*修改人：XXX
*描述：
************************************************************************************/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Hydrology.DBManager
{
    //使用实例
    //string b = ConsoleApplication2.JsonDataContractJsonSerializer.SerializeDictionaryToJsonString(dic);
    //param["rain"] = dic;
    //string rainStr = Post(url, param);
    //List<Rain> rainList = (List<Rain>)ConsoleApplication2.JsonDataContractJsonSerializer.JsonToObject(rainStr, new List<Rain>());
    //string c = ConsoleApplication2.JsonDataContractJsonSerializer.ObjectToJson(rainList);
    //param["rain"] = c;
    //string rainStr2 = Post(url, param);
    public class HttpHelper
    {
        /// <summary>
        /// 以post方式发送HTTP请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string Post(string url, Dictionary<string, string> dic)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            #region 添加Post 参数
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in dic)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
            byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
        public static string Post(string url, Dictionary<string, Object> dic)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            #region 添加Post 参数
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in dic)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
            byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
        /// <summary>
        /// Dictionary转string字符串
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static string SerializeDictionaryToJsonString<TKey, TValue>(Dictionary<TKey, TValue> dict)
        {
            if (dict.Count == 0)
                return "";

            string jsonStr = JsonConvert.SerializeObject(dict);
            return jsonStr;
        }

        /// <summary>
        /// json字符串转对象 
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object JsonToObject(string jsonString, object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());

            MemoryStream mStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            return serializer.ReadObject(mStream);
        }

        /// <summary>
        /// 对象转json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson(object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, obj);
            byte[] dataBytes = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(dataBytes, 0, (int)stream.Length);
            return Encoding.UTF8.GetString(dataBytes);
        }
        /// <summary>
        /// 将"yyyy-MM-dd HH:mm:ss"格式的字符串转为"//Date(1294499956278+0800)//"格式  
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static string JsonDeserialize(string jsonString)
        {
            string p = @"(\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2})";
            System.Text.RegularExpressions.MatchEvaluator matchEvaluator = new System.Text.RegularExpressions.MatchEvaluator(ConvertDateStringToJsonDate);
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(p);
            string c = reg.Replace(jsonString, matchEvaluator);
            return c;
        }
        /// <summary>  
        /// 将时间字符串转为Json时间  
        /// </summary>  
        private static string ConvertDateStringToJsonDate(System.Text.RegularExpressions.Match m)
        {
            string result = string.Empty;
            DateTime dt = DateTime.Parse(m.Groups[0].Value);
            dt = dt.ToUniversalTime();
            TimeSpan ts = dt - DateTime.Parse("1970-01-01");
            result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);
            return result;
        }
    }
}