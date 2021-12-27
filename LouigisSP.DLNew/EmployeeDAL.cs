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
                        int isAdmin = (int)dr["isAdmin"];
                        Employee emp;
                        if (isAdmin == 1) {
                             emp = new Admin(int.Parse(userId), firstName, lastName, phoneNumber, email, pass, DateTime.Parse(dateOfRegistration), DateTime.Parse(dateOfBirt));
                        } else {
                             emp = new Employee(int.Parse(userId), firstName, lastName, phoneNumber, email, pass, DateTime.Parse(dateOfRegistration), DateTime.Parse(dateOfBirt));
                        }
                        

                        employees.Add(emp);


                    }

                }
            }

            return employees;

        }

        public bool insertEmployee(Employee employee)
        {
            int isAdmin = 0;
            if (employee is Admin) {
                isAdmin = 1;
            }

            SqlConnectionStringBuilder sConnB = new SqlConnectionStringBuilder();
            sConnB.DataSource = dataS;
            sConnB.InitialCatalog = initialC;
            sConnB.UserID = userId;
            sConnB.Password = pass;

            using (SqlConnection conn = new SqlConnection(sConnB.ConnectionString))
            {


                Console.WriteLine(conn.ToString());
                using (SqlCommand cmd = new SqlCommand("spAddEmployeeToEmployeesTable", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@firstName", employee.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@phoneNumber", employee.PhoneNumber);
                    cmd.Parameters.AddWithValue("@email", employee.Email);
                    cmd.Parameters.AddWithValue("@pass", employee.Pass);
                    cmd.Parameters.AddWithValue("@dateOfRegistration", employee.DateOfRegistration);
                    cmd.Parameters.AddWithValue("@dateOfBirth", employee.DateOfBirth);
                    cmd.Parameters.AddWithValue("@isAdmin", isAdmin);



                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine("Records Inserted Successfully");
                        if (rowsAffected >= 1)
                        {
                            return true;
                        }
                        else return false;

                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine("Error Generated. Details: " + e.ToString());
                        return false;
                    }

                }
            }


        }


        public bool DeleteEmployee(string id)
        {
            string query = "delete from employees where id=@id";
            SqlConnectionStringBuilder sConnB = new SqlConnectionStringBuilder();
            sConnB.DataSource = dataS;
            sConnB.InitialCatalog = initialC;
            sConnB.UserID = userId;
            sConnB.Password = pass;

            using (SqlConnection conn = new SqlConnection(sConnB.ConnectionString))
            {


                Console.WriteLine(conn.ToString());
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected >= 1)
                        {
                            return true;
                        }
                        else return false;


                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine("Error Generated. Details: " + e.ToString());
                        return false;
                    }
                }
            }
        }


    }
}
