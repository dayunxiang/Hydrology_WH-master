using System;
using System.Windows.Forms;
using Hydrology.CControls;
using Hydrology.DataMgr;

namespace Hydrology.Forms
{
    public partial class CWebGsmConfigForm : Form
    {
        private CDataGridViewWebGsm m_dgvGsm;
        public CWebGsmConfigForm()
        {
            InitializeComponent();
            Init();
            FormHelper.InitUserModeEvent(this);
        }

        private void Init()
        {
            tableLayoutPanel.SuspendLayout();
            m_dgvGsm = new CDataGridViewWebGsm();
            m_dgvGsm.AllowUserToAddRows = false;
            m_dgvGsm.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            m_dgvGsm.Dock = DockStyle.Fill;
            m_dgvGsm.AllowUserToResizeRows = false;
            m_dgvGsm.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            m_dgvGsm.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
            m_dgvGsm.RowHeadersWidth = 50;
            m_dgvGsm.ColumnHeadersHeight = 25;
            m_dgvGsm.SetEditMode(true);
            tableLayoutPanel.Controls.Add(m_dgvGsm, 0, 1);
            tableLayoutPanel.ResumeLayout(false);

            if (!m_dgvGsm.Init())
            {
                //初始化失败
                //throw new Exception("配置GPRS时，未配置通讯方式和数据协议");
                MessageBox.Show("配置GSM时，请先配置GSM通讯方式和数据协议");
                this.Close();
                return;
            }
        }

        private void tsButSave_Click(object sender, EventArgs e)
        {
            m_dgvGsm.DoSave();
            this.Focus();
        }

        private void tsButRevert_Click(object sender, EventArgs e)
        {
           m_dgvGsm.Revert();
        }

        private void tsButAddUser_Click(object sender, EventArgs e)
        {
            m_dgvGsm.AddNewPort();
        }

        private void tsButDelete_Click(object sender, EventArgs e)
        {
            m_dgvGsm.DoDelete();
        }

        private void tsButExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void CWebGsmConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_dgvGsm.Close())
            {

            }
            else
            {
                // 保存失败
                e.Cancel = true;
            }
        }
    }
}
