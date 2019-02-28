/************************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Entity.Protocol.Data
*文件名： CSDStruct
*创建人： XXX
*创建时间：2018-12-11 10:57:20
*描述
*=====================================================================
*修改标记
*修改时间：2018-12-11 10:57:20
*修改人：XXX
*描述：
************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hydrology.Entity
{
    public class CSDStruct
    {
        public String StationID;
        public String Cmd;

        public List<CTimeAndAllData> Datas;
    }
    public class CTimeAndAllData
    {
        public DateTime Time;
        public string water;
        public string rain;
        public string voltage;
    }
}

