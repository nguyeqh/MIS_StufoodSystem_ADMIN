using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Xml.Linq;
using System.Runtime.Remoting.Messaging;

namespace StufoodSystem_ADMIN
{
    public partial class frmLoginAccountant : Form
    {
        String strConn = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
        private string AccountID;

        public delegate void Login(String accountantID);
        public event Login LoginEvent;
        public frmLoginAccountant()
        {
            InitializeComponent();
        }

        public frmLoginAccountant(string Message) : this()
        {
            AccountID = Message;
           
        }

        

        private void btnLogin_Clicked(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();

            //String sSQL = "SELECT * FROM ACCOUNTANT WHERE ACCOUNTID = @AccountID and AGENTPHONE = @AgentPhone;";

            //SqlCommand cmd = new SqlCommand(sSQL, conn);
            //cmd.Parameters.Add(new SqlParameter("@AccountID", tbAccountantID.Text.ToString()));
            //cmd.Parameters.Add(new SqlParameter("@AgentPhone", tbAgentPhone.Text.ToString()));

            
            //---------------- SQL INJECTION TEST ----------------//
            String sSQL = "SELECT * FROM ACCOUNTANT WHERE ACCOUNTID = '" + tbAccountantID.Text.ToString() + "' and AGENTPHONE = '" + tbAgentPhone.Text.ToString() + "';";

            SqlCommand cmd = new SqlCommand(sSQL, conn);

            //---------------- END OF SQL INJECTION TEST -----------//
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
                if (LoginEvent != null)
                {
                    LoginEvent(tbAccountantID.Text);
                }
                
                
                this.Close();
                
            }
            else
            {
                MessageBox.Show("Mã nhân viên không hợp lệ");
            }

            conn.Close();

        }

        private void frmLoginAccountant_Load(object sender, EventArgs e)
        {

        }
    }
}
