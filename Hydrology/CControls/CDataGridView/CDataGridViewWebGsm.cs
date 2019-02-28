using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Hydrology.DataMgr;
using Hydrology.DBManager.Interface;
using Hydrology.Entity;
using Hydrology.Forms;
using Protocol.Manager;
using System.Drawing;

namespace Hydrology.CControls
{
    /// <summary>
    /// 串口配置，至少要有一个信道协议，
    /// </summary>
    class CDataGridViewWebGsm : CExDataGridView
    {
        #region 静态常量

        // 表列属性
        private static readonly string CS_Delete = "删除";
        private static readonly string CS_ServerIp = "服务地址";
        private static readonly string CS_Port = "端口号";
        private static readonly string CS_ProtocolChannel = "通讯方式";
        private static readonly string CS_ProtocolData = "数据协议";
        private static readonly string CS_Account = "账户名";
        private static readonly string CS_Password = "密码";
        private static readonly string CS_CurrentStatus = "当前状态";

        //列属性值
        private static readonly string CS_Status_Enabled = "启用";
        private static readonly string CS_Status_Disable = "关闭";
        private static readonly string CS_None = "无";

        #endregion 静态常量

        #region 成员变量
        private List<string> m_listProtocolData;
        private List<string> webGsmNameLists;
        private List<CXMLWeb> m_listGsmConfig;
      private bool m_bHasModified;
        #endregion 成员变量

        public CDataGridViewWebGsm()
            : base()
        {
            m_bHasModified = false;
            m_listGsmConfig = new List<CXMLWeb>();
            InitDataSource();
            
        }

        public void InitDataSource()
        {
            m_listGsmConfig = new List<CXMLWeb>();
            m_listProtocolData = XmlDocManager.Instance.DataProtocolNames;
            //m_listGsmConfig = XmlDocManager.Instance.GetComOrPortConfig(false);
            webGsmNameLists = XmlDocManager.Instance.WebGSMProtocolNames;
            foreach (var gsmName in webGsmNameLists)
            {
                var dll = XmlDocManager.Instance.GetInfoByName(gsmName);
                foreach(CXMLWeb sxmlWeb in dll.Webs)
                {
                    m_listGsmConfig.Add(sxmlWeb);
                }
            }
                //m_listProtocolData = XmlDocManager.Instance.DataProtocolNames;
                //m_listProtocolGprs = XmlDocManager.Instance.GPRSProtocolNames;
                //// 当前的GPRS协议配置
                //m_listPortConfig = XmlDocManager.Instance.GetComOrPortConfig(false);

         }
        public bool Init()
        {
            // 判断是否有信道协议
            if (m_listProtocolData.Count <= 0 || webGsmNameLists.Count <= 0)
            {
                MessageBox.Show("请先配置GMS通讯方式和数据协议");
                return false;
            }
            // 初始化
            InitUI();
            return true;
        }


        /// <summary>
        /// 删除数据
        /// </summary>
        public void DoDelete()
        {
            if (this.IsCurrentCellInEditMode)
            {
                //MessageBox.Show("请完成当前的编辑");
                this.EndEdit();
                //return false;
            }

            for (int i = 0; i < m_listMaskedDeletedRows.Count; ++i)
            {
                base.DeleteRowData(m_listMaskedDeletedRows[i]);
            }
            m_listMaskedDeletedRows.Clear(); //清空
            base.UpdateDataToUI();
            m_bHasModified = true; //更改状态为已修改
        }

        public override bool DoSave()
        {
            if (this.IsCurrentCellInEditMode)
            {
                //MessageBox.Show("请完成当前的编辑");
                this.EndEdit();
                //return false;
            }
            // 无论是否修改都重新保存
            if (((m_listEditedRows.Count <= 0) && ((m_listMaskedDeletedRows.Count <= 0)) && (!m_bHasModified)) || (!(IsDataChanged())))
            {
                MessageBox.Show("没有任何修改，无需保存");
                return true;
            }
            if (!AssertSaveData())
            {
                // 有非法数据 
                return false;
            }
            // 生成ProtocolConfig
            GeneratePortConfig();
            // 应该作为一个事物一起处理
            //return base.DoSave();
            //TODO
            bool result = XmlDocManager.Instance.ResetWebConfig("WebGsm", m_listGsmConfig);
            if (result)
            {
                //CMessageBox box = new CMessageBox();
                //box.MessageInfo = "正在更新端口";
                //box.ShowDialog();
                // 通知其他界面更新消息
                CPortDataMgr.Instance.InitWebGsm();
                //box.CloseDialog();
                MessageBox.Show("保存成功");
            }
            else
            {
                MessageBox.Show("保存失败");
            }
            // 重新加载
            ClearAllState();
            InitDataSource();

            InitUI(); //重新加载界面
            return true;
        }
        /// <summary>
        /// 页面数据结果集添加
        /// </summary>
        private void GeneratePortConfig()
        {
            // 标记为删除的就不需要添加到结果集中了
            m_listGsmConfig  = new List<CXMLWeb>();
            // 获取修改了的所有数据
            for (int i = 0; i < m_dataTable.Rows.Count; ++i)
            {
                if (!m_listMaskedDeletedRows.Contains(i))
                {
                    // 不包含才需要判断
                    CXMLWeb config = new CXMLWeb();
                    config.IP = m_dataTable.Rows[i][CS_ServerIp].ToString();
                    config.PortNumber = int.Parse(m_dataTable.Rows[i][CS_Port].ToString());
                    config.ProtocolChannel = m_dataTable.Rows[i][CS_ProtocolChannel].ToString();
                    
                    config.ProtocolData = m_dataTable.Rows[i][CS_ProtocolData].ToString();
                    if (config.ProtocolData == CS_None)
                    {
                        config.ProtocolData = ""; //空数据协议
                    }
                    if (config.ProtocolData == CS_None)
                    {
                        config.ProtocolData = ""; //空数据协议
                    }

                    config.Account = m_dataTable.Rows[i][CS_Account].ToString();
                    config.Password = m_dataTable.Rows[i][CS_Password].ToString();
                    
                    if (CS_Status_Enabled == m_dataTable.Rows[i][CS_CurrentStatus].ToString())
                    {
                        // 启动状态
                        config.BStartOrNot = true;
                    }
                    else
                    {
                        config.BStartOrNot = false;
                    }
                    m_listGsmConfig.Add(config);
                }// end of if 
            }
            m_listEditedRows.Clear();
            m_listMaskedDeletedRows.Clear();
        }

        /// <summary>
        /// 设置每个每列对应的单元格的格式
        /// </summary>
        /// <param name="bEnable"></param>
        public void SetEditMode(bool bEnable)
        {
            if (bEnable)
            {
                // 设定标题栏,默认有个隐藏列
                this.Header = new string[]
                {
                    CS_Delete,
                    CS_ServerIp,
                    CS_Port,
                    CS_ProtocolChannel,
                    CS_ProtocolData,
                    CS_Account,
                    CS_Password,
                    CS_CurrentStatus
                };
                var delCol = new DataGridViewCheckBoxColumn();
                base.SetColumnEditStyle(0, delCol);

                //  添加IP
                DataGridViewTextBoxColumn ipCol = new DataGridViewTextBoxColumn();
                base.SetColumnEditStyle(1, ipCol);

                //  添加端口号
                DataGridViewNumericUpDownColumn portNumberCol = new DataGridViewNumericUpDownColumn();
                portNumberCol.Minimum = 1000;
                portNumberCol.Maximum = 65535;
                base.SetColumnEditStyle(2, portNumberCol);

                //  添加通讯方式
                var ChannelCol = new DataGridViewComboBoxColumn();
                ChannelCol.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                var channelName = "WebGsm";
                ChannelCol.Items.Add(CS_None);
                ChannelCol.Items.Add(channelName);
                base.SetColumnEditStyle(3, ChannelCol);

                //  添加数据协议
                var dataCol = new DataGridViewComboBoxColumn();
                dataCol.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                var dataNames = m_listProtocolData;
                dataCol.Items.Add(CS_None);
                foreach (var item in dataNames)
                {
                    dataCol.Items.Add(item);
                }
                base.SetColumnEditStyle(4, dataCol);

                //  添加用户账户
                DataGridViewTextBoxColumn accountCol = new DataGridViewTextBoxColumn();
                base.SetColumnEditStyle(5, accountCol);

                //  添加用户账户
                DataGridViewTextBoxColumn passCol = new DataGridViewTextBoxColumn();
                base.SetColumnEditStyle(6, passCol);

                //当前状态
                var statusCol = new DataGridViewComboBoxColumn();
                statusCol.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                statusCol.Items.AddRange(new object[] { CS_Status_Enabled, CS_Status_Disable });
                base.SetColumnEditStyle(7, statusCol);

                this.Columns[0].Width = 20; //删除列
            }
        }
        
        /// <summary>
        /// 新增行数据
        /// </summary>
        public void AddNewPort()
        {
            base.AddRow(new string[]
                {
                    "False",
                    "",
                    "",
                    CS_None,
                    CS_None,
                    "",
                    "",
                    CS_Status_Disable
                }, EDataState.ENormal, false);
            base.m_listEditedRows.Add(m_dataTable.Rows.Count - 1);
            base.ClearSelection(); //????
            base.UpdateDataToUI();
            m_bHasModified = true; //设置修改模式
        }

       /// <summary>
       /// 页面关闭
       /// </summary>
       /// <returns></returns>
        public bool Close()
        {
            if (this.IsCurrentCellInEditMode)
            {
                //MessageBox.Show("请完成当前的编辑");
                this.EndEdit();
                //return false;
            }
            if (!AssertSaveData())
            {
                // 不让退出
                DialogResult result = MessageBox.Show("是否强行退出？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult.Yes == result)
                {
                    return true;
                }
                return false;
            }
            // 关闭方法
            if (((m_listEditedRows.Count > 0) || (m_listMaskedDeletedRows.Count > 0) || (m_bHasModified)) && IsDataChanged())
            {
                DialogResult result = MessageBox.Show("当前所做修改尚未保存，是否保存？", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (DialogResult.Cancel == result)
                {
                    return false;
                }
                else if (DialogResult.Yes == result)
                {
                    // 保存当前修改
                    DoSave();
                }
                else if (DialogResult.No == result)
                {
                    return true;
                }
            }
            return true;
        }


        public void Revert()
        {
            // 清空所有状态
            this.ClearAllState();
            InitDataSource();
            InitUI();
            m_bHasModified = false;
        }
        /// <summary>
        /// 校验数据是否合法
        /// </summary>
        /// <returns></returns>
        private bool AssertSaveData()
        {
            // 判断当前数据是否合法
            List<int> listEditRows = new List<int>();
            foreach (int item in base.m_listEditedRows)
            {
                if (!m_listMaskedDeletedRows.Contains(item))
                {
                    listEditRows.Add(item);
                }
            }
            // 将去重后的项赋给编辑项
            base.m_listEditedRows = listEditRows;

            // 判断串口号不能为空,以及不能重复
            for (int i = 0; i < listEditRows.Count; ++i)
            {
                // 没有删除，并且不是新增的

                if (base.m_dataTable.Rows[listEditRows[i]][CS_ServerIp].Equals(""))
                {
                    MessageBox.Show("服务地址不能为空");
                    return false;
                }
                if (base.m_dataTable.Rows[listEditRows[i]][CS_Port].Equals(""))
                {
                    MessageBox.Show("端口号不能为空");
                    return false;
                }
                int port = int.Parse(base.m_dataTable.Rows[listEditRows[i]][CS_Port].ToString());
                //if (portnumber.Contains(port))
                //{
                //    MessageBox.Show("串口号不能重复");
                //    return false;
                //}
                //portnumber.Add(port);
            }
            // 判断端口号是否重复，以及信道协议是否为空
            List<int> allPortNumber = new List<int>();
            for (int i = 0; i < m_dataTable.Rows.Count; ++i)
            {
                if (!m_listMaskedDeletedRows.Contains(i))
                {
                    allPortNumber.Add(int.Parse(m_dataTable.Rows[i][CS_Port].ToString()));
                    if (CS_None == m_dataTable.Rows[i][CS_ProtocolChannel].ToString())
                    {
                        MessageBox.Show("通讯方式不能为空");
                        return false;
                    }
                }

            }

            for (int i = 0; i < m_dataTable.Rows.Count; ++i)
            {
                int port = int.Parse(m_dataTable.Rows[i][CS_Port].ToString());
                if (!m_listMaskedDeletedRows.Contains(i))
                {
                    // 不包含才需要判断
                    if (allPortNumber.IndexOf(port) !=
                    allPortNumber.LastIndexOf(port))
                    {
                        MessageBox.Show(string.Format("端口号\"{0}\"不能重复", port));
                        return false;
                    }
                }

            }
            return true;
        }
        #region 事件重写
        // 重写Cell值改变事件
        protected override void EHValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (base.m_arrayStrHeader[e.ColumnIndex] == CS_Delete)
            {
                // 删除列
                if (base.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Equals("True"))
                {
                    // 删除项
                    base.MarkRowDeletedOrNot(e.RowIndex, true);
                }
                else
                {
                    base.MarkRowDeletedOrNot(e.RowIndex, false);
                }
            }
            else
            {
                // 非删除列
                base.EHValueChanged(sender, e);
            }
            base.UpdateDataToUI();
        }

        // 重写双击事件
        protected override void OnCellMouseDoubleClick(DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (base.m_listMaskedDeletedRows.Contains(e.RowIndex))
            {
                if (base.m_arrayStrHeader[e.ColumnIndex] == CS_Delete)
                {
                    //开启编辑
                    base.OnCellMouseDoubleClick(e);
                }
                else
                {
                    //不编辑
                }
            }
            else
            {
                // 自动启动方式与手动启动方式，只是针对下一次系统加载时候的说法
                //if (base.m_arrayStrHeader[e.ColumnIndex] == CS_CurrentStatus)
                //{
                //    // 点击了当前状态列，判断是否可修改，仅当是自动启动状态才能启用编辑
                //    if (base.m_dataTable.Rows[e.RowIndex][CS_StartType].ToString() == CS_StartType_Manaual)
                //    {
                //        // 开启编辑
                //        base.OnCellMouseDoubleClick(e);
                //    }
                //    else
                //    {
                //        // 不开启编辑
                //    }
                //}
                //else
                //{
                // 开启编辑
                base.OnCellMouseDoubleClick(e);
                //}
            }
        }

        // 单击事件
        protected override void OnCellClick(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (base.m_listMaskedDeletedRows.Contains(e.RowIndex))
            {
                if (base.m_arrayStrHeader[e.ColumnIndex] == CS_Delete)
                {
                    //开启编辑
                    base.OnCellClick(e);
                }
                else
                {
                    //不编辑
                }
            }
            else
            {
                if (base.m_arrayStrHeader[e.ColumnIndex] == CS_CurrentStatus)
                {
                    
                        // 开启编辑
                        base.OnCellClick(e);
                   
                   
                }
                else
                {
                    // 开启编辑
                    base.OnCellClick(e);
                }
            }
        }

        // 清空所有数据，恢复到刚加载的状态
        protected override void ClearAllState()
        {
            base.ClearAllState();
            m_bHasModified = false;

        }

        #endregion 事件重写

        protected void InitUI()
        {
            base.ClearAllRows();
            List<string[]> listShowArray = new List<string[]>();
            List<EDataState> listState = new List<EDataState>();
            for (int i = 0; i < m_listGsmConfig.Count; ++i)
            {
                List<string> listShow = new List<string>();
                listShow.Add("False");
                listShow.Add(m_listGsmConfig[i].IP);
                listShow.Add(m_listGsmConfig[i].PortNumber.ToString());
                if (null != m_listGsmConfig[i].ProtocolChannel)
                {
                    listShow.Add(m_listGsmConfig[i].ProtocolChannel);
                }
                else
                {
                    listShow.Add(CS_None);
                    //TODO
                    //CPortDataMgr.Instance.StopGprs();
                }

                if (null != m_listGsmConfig[i].ProtocolData)
                {
                    listShow.Add(m_listGsmConfig[i].ProtocolData);
                }
                else
                {
                    listShow.Add(CS_None);
                    //TODO
                    //CPortDataMgr.Instance.StopGprs();
                }
                listShow.Add(m_listGsmConfig[i].Account);
                listShow.Add(m_listGsmConfig[i].Password);
                listShow.Add((m_listGsmConfig[i].BStartOrNot ? CS_Status_Enabled : CS_Status_Disable));
                listShowArray.Add(listShow.ToArray());
                listState.Add(EDataState.ENormal);

            }
            base.AddRowRange(listShowArray, listState);
            base.UpdateDataToUI();
        }

    }
}
