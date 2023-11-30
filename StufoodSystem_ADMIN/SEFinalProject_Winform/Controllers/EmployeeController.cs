using StufoodSystem_ADMIN.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StufoodSystem_ADMIN.Controllers
{
    public class EmployeeController
    {
        private static String strConn = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
        private static SqlConnection conn;
        public EmployeeController() 
        {
            conn = new SqlConnection(strConn);
            
        }
        public static List<Employee> GetAllEmployee()
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            String sSQL = "SELECT * FROM EMPLOYEE";
            List<Employee> employees = GetEmployeesFromDatabase(strConn, sSQL);
      
            conn.Close();
            return employees;

        }

        public static Employee GetEmployeeByID(String id)
        {
            String sSQL = "SELECT * FROM EMPLOYEE;";

            List<Employee> employees = GetEmployeesFromDatabase(strConn, sSQL);
            Employee foundItem = employees.FirstOrDefault(item => item.employeeID == id);


            return foundItem;
        }

        public static void UpdateEmployee (String id, Employee updateEmployee)
        {
            String sSQL = "UPDATE EMPLOYEE SET EMPLOYEENAME = @EmployeeName, PHONE = @Phone, ADDRESS = @Adress, JOB = @Job, POSITION = @Position, " +
                "EMAIL = @Email, SALARY = @Salary WHERE EMPOLYEEID = @EmpID";
            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.Add(new SqlParameter("@EmpID", id));
            cmd.Parameters.Add(new SqlParameter("@EmployeeName", updateEmployee.employeeName));
            cmd.Parameters.Add(new SqlParameter("@Phone", updateEmployee.phone));
            cmd.Parameters.Add(new SqlParameter("@Address", updateEmployee.address));
            cmd.Parameters.Add(new SqlParameter("@Email", updateEmployee.email));
            cmd.Parameters.Add(new SqlParameter("@Job", updateEmployee.job));
            cmd.Parameters.Add(new SqlParameter("@Position", updateEmployee.position));
            cmd.Parameters.Add(new SqlParameter("@Salary", updateEmployee.salary));
            try
            {

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error:" + ex.Message);
            }
        }

        public static void CreateEmployee(Employee employee)
        {
           
            String sSQL = "INSERT INTO EMPLOYEE (EmployeeID, EMPLOYEENAME, PHONE, ADDRESS, JOB, POSITION, EMAIL, SALARY) " +
                "VALUES (@EmpID, @EmployeeName, @Phone, @Address, @Job, @Position, @Email, @Salary)";
            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.Add(new SqlParameter("@EmpID", employee.employeeID));
            cmd.Parameters.Add(new SqlParameter("@EmployeeName", employee.employeeName));
            cmd.Parameters.Add(new SqlParameter("@Phone", employee.phone));
            cmd.Parameters.Add(new SqlParameter("@Address", employee.address));
            cmd.Parameters.Add(new SqlParameter("@Email", employee.email));
            cmd.Parameters.Add(new SqlParameter("@Job", employee.job));
            cmd.Parameters.Add(new SqlParameter("@Position", employee.position));
            cmd.Parameters.Add(new SqlParameter("@Salary", employee.salary));
            try
            {

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error:" + ex.Message);
            }
        
        }
        //--------------- PRIVATE FUNCTION -------------------- //

        static List<Employee> GetEmployeesFromDatabase(string connectionString, string sSQL)
        {
            List<Employee> result = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlDataAdapter adapter = new SqlDataAdapter(sSQL, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Convert DataTable rows to List<Employee>
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Employee employee = new Employee
                        {
                            employeeID = row["employeeID"].ToString(),
                            employeeName = row["employeeName"].ToString(),
                            phone = row["phone"].ToString(),
                            address = row["address"].ToString(),
                            job = row["job"].ToString(),
                            position = row["position"].ToString(),
                            email = row["email"].ToString(),
                            salary = Convert.ToDouble(row["salary"])
                        };

                        result.Add(employee);
                    }
                }
            }

            return result;
        }
    }
}
