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

        //load employee list
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

        //ADD NEW EMPLOYEE
        private void materialButton14_Click(object sender, EventArgs e)
        {
            Employee newEmployee = new Employee();

            newEmployee.employeeID = textBox1.Text;
            newEmployee.employeeName = textBox2.Text;
            newEmployee.email = textBox3.Text;
            newEmployee.phone = textBox4.Text;
            newEmployee.address = richTextBox3.Text.ToString();
            newEmployee.job = textBox5.Text;
            newEmployee.position = textBox6.Text;
            newEmployee.salary = Convert.ToDouble(textBox7.Text);

            try
            {

                EmployeeController.CreateEmployee(newEmployee);
                MessageBox.Show("Thêm nhân viên thành công!");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox7.Clear();
                richTextBox3.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi xử lý!");
                throw new Exception("Error:" + ex.Message);
            }

           
        }
    }
}
