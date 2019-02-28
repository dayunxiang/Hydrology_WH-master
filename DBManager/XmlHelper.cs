/************************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：DBManager
*文件名： XmlHelper
*创建人： XXX
*创建时间：2019-2-26 19:28:56
*描述
*=====================================================================
*修改标记
*修改时间：2019-2-26 19:28:56
*修改人：XXX
*描述：
************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Hydrology.DBManager
{
    public class XmlHelper
    {
        public static Dictionary<string, string> urlDic = new Dictionary<string, string>();
        public static Dictionary<string,string> getXMLInfo()
        {
            //将XML文件加载进来
            Dictionary<string, string> result = new Dictionary<string, string>();
            XDocument document = XDocument.Load("config\\datainterface.xml");
            //获取到XML的根元素进行操作
            XElement root = document.Root;
            XElement ip = root.Element("ip");
            //获取name标签的值
            urlDic["ip"] = ip.Value.ToString();
            return result;
            //Console.WriteLine(shuxing.Value);
            //获取根元素下的所有子元素
            //IEnumerable<XElement> enumerable = root.Elements();
            //foreach (XElement item in enumerable)
            //{
            //    foreach (XElement item1 in item.Elements())
            //    {
            //        Console.WriteLine(item1.Name);   //输出 name  name1            
            //    }
            //    Console.WriteLine(item.Attribute("id").Value);  //输出20
            //}
            //Console.ReadKey();
        }
    }
}