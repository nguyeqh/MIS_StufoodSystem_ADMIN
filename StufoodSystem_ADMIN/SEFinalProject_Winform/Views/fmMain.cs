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
            LoadEmployees();
        }


        //LOAD EMPLOYEE TO FIELDS
        private void employeeListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if there is a selected item
            if (employeeListView.SelectedItems.Count > 0)
            {
                // Access data from the selected row
                ListViewItem selectedRow = employeeListView.SelectedItems[0];

                textBox14.Text = selectedRow.SubItems[0].Text; //id
                textBox13.Text = selectedRow.SubItems[1].Text; //name
                textBox12.Text = selectedRow.SubItems[6].Text; //email
                textBox11.Text = selectedRow.SubItems[2].Text; //phone
                textBox10.Text = selectedRow.SubItems[4].Text; //job
                textBox9.Text = selectedRow.SubItems[5].Text; //position
                textBox8.Text = selectedRow.SubItems[7].Text; //salary
                richTextBox4.Text = selectedRow.SubItems[3].Text; //address

            }
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

                LoadEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi xử lý!");
                throw new Exception("Error:" + ex.Message);
            }
           
        }

        //UPDATE EMPLOYEE
        private void materialButton16_Click(object sender, EventArgs e)
        {
            Employee newEmployee = new Employee();

            newEmployee.employeeID = textBox14.Text;
            newEmployee.employeeName = textBox13.Text;
            newEmployee.email = textBox12.Text;
            newEmployee.phone = textBox11.Text;
            newEmployee.address = richTextBox4.Text.ToString();
            newEmployee.job = textBox10.Text;
            newEmployee.position = textBox9.Text;
            newEmployee.salary = Convert.ToDouble(textBox8.Text);

            try
            {

                EmployeeController.UpdateEmployee(newEmployee.employeeID, newEmployee);
                MessageBox.Show("Chỉnh sửa thông tin nhân viên thành công!");
                textBox14.Clear();
                textBox12.Clear();
                textBox13.Clear();
                textBox11.Clear();
                textBox10.Clear();
                textBox9.Clear();
                textBox8.Clear();
                richTextBox4.Clear();

                LoadEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi xử lý!");
                throw new Exception("Error:" + ex.Message);
            }
        }

        //LOAD AFFILIATED SCHOOLS
        private void materialTabSelector6_Click(object sender, EventArgs e)
        {
            LoadAffiliatedSchools();
        }

        ////LOAD AFFILIATED SCHOOL TO FIELDS
        private void schoolListView_SelectedIndexChanged(object sender, EventArgs e)
        {

            // Check if there is a selected item
            if (schoolListView.SelectedItems.Count > 0)
            {
                // Access data from the selected row
                ListViewItem selectedRow = schoolListView.SelectedItems[0];

                textBox16.Text = selectedRow.SubItems[0].Text; //id
                textBox15.Text = selectedRow.SubItems[1].Text; //name
                textBox17.Text = selectedRow.SubItems[2].Text; //phone
                textBox18.Text = selectedRow.SubItems[3].Text; //email
                textBox19.Text = selectedRow.SubItems[5].Text; //number of student
                richTextBox5.Text = selectedRow.SubItems[4].Text; //address

            }
        }

        //UPDATE SCHOOL
        private void materialButton13_Click(object sender, EventArgs e)
        {
            AffiliatedSchool newSchool = new AffiliatedSchool();

            newSchool.schoolId = textBox16.Text;
            newSchool.schoolName = textBox15.Text;
            newSchool.schoolPhone = textBox17.Text;
            newSchool.email = textBox18.Text;
            newSchool.numberOfStudents = Convert.ToInt32(textBox19.Text);
            newSchool.schoolAddress = richTextBox5.Text;

            try
            {

                AffiliatedSchoolController.UpdateAffiliatedSchool(newSchool.schoolId, newSchool);
                MessageBox.Show("Chỉnh sửa thông tin trường học thành công!");
                textBox16.Clear(); 
                textBox15.Clear(); 
                textBox17.Clear(); 
                textBox18.Clear();
                textBox19.Clear(); //number of student
                richTextBox5.Clear(); //address

                LoadAffiliatedSchools();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi xử lý!");
                throw new Exception("Error:" + ex.Message);
            }
        }

        //ADD NEW SCHOOL
        private void materialButton12_Click(object sender, EventArgs e)
        {
            AffiliatedSchool newSchool = new AffiliatedSchool();

            newSchool.schoolId = textBox20.Text;
            newSchool.schoolName = textBox21.Text;
            newSchool.schoolPhone = textBox22.Text;
            newSchool.email = textBox23.Text;
            newSchool.numberOfStudents = Convert.ToInt32(textBox24.Text);
            newSchool.schoolAddress = richTextBox6.Text;

            try
            {

                AffiliatedSchoolController.CreateAffiliatedSchool(newSchool);
                MessageBox.Show("Thêm trường học liên kết thành công!");
                textBox20.Clear();
                textBox21.Clear();
                textBox22.Clear();
                textBox23.Clear();
                textBox24.Clear();
                richTextBox6.Clear();

                LoadEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi xử lý!");
                throw new Exception("Error:" + ex.Message);
            }
        }

        //------------------------- FUNCTION ----------------- //
        private void LoadEmployees()
        {
            List<Employee> employees = EmployeeController.GetAllEmployee();

            employeeListView.Items.Clear();
            employeeListView.AllowDrop = true;

            foreach (Employee employee in employees)
            {
                ListViewItem item = new ListViewItem(employee.employeeID); //0
                item.SubItems.Add(employee.employeeName); //1
                item.SubItems.Add(employee.phone); //2
                item.SubItems.Add(employee.address); //3
                item.SubItems.Add(employee.job); //4
                item.SubItems.Add(employee.position); //5
                item.SubItems.Add(employee.email); //6
                item.SubItems.Add(employee.salary.ToString()); //7

                employeeListView.Items.Add(item);
            }
        }

        private void LoadAffiliatedSchools()
        {
            List<AffiliatedSchool> schools = AffiliatedSchoolController.GetAllSchool();

            schoolListView.Items.Clear();

            foreach(AffiliatedSchool school in schools)
            {
                ListViewItem item = new ListViewItem(school.schoolId); //0
                item.SubItems.Add(school.schoolName); //1
                item.SubItems.Add(school.schoolPhone); //2
                item.SubItems.Add(school.email); //3
                item.SubItems.Add(school.schoolAddress); //4
                item.SubItems.Add(school.numberOfStudents.ToString()); //5

                schoolListView.Items.Add(item);
            }
        }

        
    }
}
