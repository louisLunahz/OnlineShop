using LouigisSP.BO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LouigisSP.DL
{
   public class EmployeeDAL
    {
        string dataS = "ASPLAPLTM049\\SQLEXPRESS";
        string initialC = "Louigis";
        string userId = "josehz";
        string pass = "C9b2a317e8";

        public List<Employee> getAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            SqlConnectionStringBuilder sConnB = new SqlConnectionStringBuilder();
            sConnB.DataSource = dataS;
            sConnB.InitialCatalog = initialC;
            sConnB.UserID = userId;
            sConnB.Password = pass;

            using (SqlConnection conn = new SqlConnection(sConnB.ConnectionString))
            {


                Console.WriteLine(conn.ToString());
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM Employees ";
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        string userId = dr["id"].ToString();
                        string firstName = dr["firstName"].ToString();
                        string lastName = dr["lastName"].ToString();
                        string phoneNumber = dr["phoneNumber"].ToString();
                        string email = dr["email"].ToString();
                        string pass = dr["pass"].ToString();
                        string dateOfRegistration = dr["dateOfRegistration"].ToString();
                        string dateOfBirt = dr["dateOfBirth"].ToString();
                        Employee emp = new Employee();

                        emp.Id = int.Parse(userId);
                        emp.FirstName = firstName;
                        emp.LastName = lastName;
                        emp.PhoneNumber = phoneNumber;
                        emp.Email = email;
                        emp.Pass = pass;
                        emp.DateOfBirth = DateTime.Parse(dateOfBirt);
                        emp.DateOfRegistration = DateTime.Parse(dateOfRegistration);

                        employees.Add(emp);


                    }

                }
            }

            return employees;

        }


    }
}
