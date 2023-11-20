using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StufoodSystem_ADMIN
{
    public partial class frmMainForm : Form
    {
        String strConn = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
        private String AccountID = "";
        public frmMainForm()
        {
            InitializeComponent();
        }

        

        private void thốngKêThángToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void frmMainForm_Load(object sender, EventArgs e)
        {
            frmLoginAccountant lgForm = new frmLoginAccountant(AccountID);
            lgForm.LoginEvent += LgForm_LoginEvent;

            lgForm.ShowDialog();
            this.WindowState = FormWindowState.Maximized;
        }

        private void LgForm_LoginEvent(string accountantID)
        {

            
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();

            String sSQL = "SELECT * FROM ACCOUNTANT WHERE ACCOUNTID = @AccountID;";

            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.Add(new SqlParameter("@AccountID", accountantID));
            try
            { 
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception("Error:" + ex.Message);
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                tbmãNhânViênToolStripMenuItem.Text = "Mã nhân viên: " + accountantID;
                tbtênNhânViênToolStripMenuItem.Text = "Tên nhân viên: " + dt.Rows[0][1].ToString();
                tbsốĐiệnThoạiToolStripMenuItem.Text ="Số điện thoại: " +  dt.Rows[0][2].ToString();
                tbđịaChỉToolStripMenuItem.Text = "Địa chỉ: " + dt.Rows[0][3].ToString();
                AccountID = accountantID;
            }
        }

        private void nhậpHàngToolStripMenuItem1_Click(object sender, EventArgs e)
        {
          
        }

        private void lượngHàngTồnKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

        }

        private void quảnLýKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmCustomerManagement frmCus = new frmCustomerManagement();
            //frmCus.MdiParent = this;
            //frmCus.Show();
        }

        private void quảnLýĐơnHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }


    }
}
