using MaterialSkin.Controls;
using StufoodSystem_ADMIN.Controllers;
using StufoodSystem_ADMIN.Models;
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

namespace StufoodSystem_ADMIN.Views
{
    public partial class fmMain : MaterialForm
    {
        readonly MaterialSkin.MaterialSkinManager materialSkinManager;
        String strConn = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
        SqlConnection conn;
        public fmMain()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkin.MaterialSkinManager.Instance;
            materialSkinManager.EnforceBackcolorOnAllComponents = true;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.Amber500,
                MaterialSkin.Primary.Amber700, MaterialSkin.Primary.Amber100, MaterialSkin.Accent.Blue100,
                MaterialSkin.TextShade.BLACK);
        }

        private void materialTabSelector7_Click(object sender, EventArgs e)
        {
            List<Employee> employees = EmployeeController.GetAllEmployee();

            employeeListView.Items.Clear();
            employeeListView.AllowDrop = true;

            foreach (Employee employee in employees)
            {
                ListViewItem item = new ListViewItem(employee.employeeID);
                item.SubItems.Add(employee.employeeName);
                item.SubItems.Add(employee.phone);
                item.SubItems.Add(employee.address);
                item.SubItems.Add(employee.job);
                item.SubItems.Add(employee.position);
                item.SubItems.Add(employee.email);
                item.SubItems.Add(employee.salary.ToString());

                employeeListView.Items.Add(item);
            }
        }

        private void employeeListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        

    }
}
