using StufoodSystem_ADMIN.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
