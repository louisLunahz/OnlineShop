using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LouigisSP.BO;

namespace LouigisSP.DL
{

   
    public class CustomerDAL
    {
        string dataS = "ASPLAPLTM049\\SQLEXPRESS";
        string initialC = "Louigis";
        string userId = "josehz";
        string pass = "C9b2a317e8";

    
        


        public Customer GetCustomerFromDB(int id) {
            Customer cus = null;
            SqlConnectionStringBuilder sConnB = new SqlConnectionStringBuilder();
            sConnB.DataSource = dataS;
            sConnB.InitialCatalog = initialC;
            sConnB.UserID = userId;
            sConnB.Password = pass;
            using (SqlConnection conn = new SqlConnection(sConnB.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM Customers where id="+id;
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

                        cus = new Customer();
                        cus.Id = int.Parse(userId);
                        cus.FirstName = firstName;
                        cus.LastName = lastName;
                        cus.PhoneNumber = phoneNumber;
                        cus.Email = email;
                        cus.Pass = pass;
                        cus.DateOfBirth =DateTime.Parse(dateOfBirt) ;
                        cus.DateOfRegistration =DateTime.Parse(dateOfRegistration);
                    }

                }
            }
            return cus;
        }

        public List<Customer> getAllCustomers() {
            List<Customer> customers = new List<Customer>();
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
                    cmd.CommandText = "SELECT * FROM Customers ";
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
                        Customer cus = new Customer();

                        cus.Id = int.Parse(userId);
                        cus.FirstName = firstName;
                        cus.LastName = lastName;
                        cus.PhoneNumber = phoneNumber;
                        cus.Email = email;
                        cus.Pass = pass;
                        cus.DateOfBirth = DateTime.Parse(dateOfBirt);
                        cus.DateOfRegistration = DateTime.Parse(dateOfRegistration);

                        customers.Add(cus);


                    }

                }
            }

            return customers;
        
        }


        public bool insertCustomer(Customer customer) {
            string query = "INSERT INTO customers (firstName, lastName, phoneNumber, email, pass, dateOfRegistration, dateOfBirth, shippingAddress,billingAddress) VALUES(@firstName, @lastName, @phoneNumber,@email,@pass,@dateOfRegistration, @dateOfBirth,@shippingAdress,@billingAddress)";
            
            
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
                    cmd.Parameters.AddWithValue("@firstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@phoneNumber", customer.PhoneNumber);
                    cmd.Parameters.AddWithValue("@email", customer.Email);
                    cmd.Parameters.AddWithValue("@pass", customer.Pass);
                    cmd.Parameters.AddWithValue("@dateOfRegistration", customer.DateOfRegistration);
                    cmd.Parameters.AddWithValue("@dateOfBirth", customer.DateOfBirth);
                    cmd.Parameters.AddWithValue("@shippingAdress", customer.ShippingAddress);
                    cmd.Parameters.AddWithValue("@billingAddress", customer.BillingAddress);



                    try {
                        conn.Open();
                       int rowsAffected= cmd.ExecuteNonQuery();
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


        public bool DeleteCustomer(string id) {
            string query = "delete from customers where id=@id";
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
                        int rowsAffected=cmd.ExecuteNonQuery();
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


        public bool EditCustomer(Customer customer) {
            string query = "update customers set firstName=@firstName, lastName=@lastName, phoneNumber=@phoneNumber, email=@email, dateOfRegistration=@dateOfRegistration, dateOfBirth=@dateOfBirth, shippingAddress=@shippingAddress, billingAddress=@billingAddress where id=@id"; 
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
                    cmd.Parameters.AddWithValue("@firstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@phoneNumber", customer.PhoneNumber);
                    cmd.Parameters.AddWithValue("@email", customer.Email);
                    cmd.Parameters.AddWithValue("@pass", customer.Pass);
                    cmd.Parameters.AddWithValue("@dateOfRegistration", customer.DateOfRegistration);
                    cmd.Parameters.AddWithValue("@dateOfBirth", customer.DateOfBirth);
                    cmd.Parameters.AddWithValue("@shippingAddress", customer.ShippingAddress);
                    cmd.Parameters.AddWithValue("@billingAddress", customer.BillingAddress);
                    cmd.Parameters.AddWithValue("@id", customer.Id);
                    try {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected >= 1)
                        {
                            return true;
                        }
                        else return false;

                    }
                    catch (SqlException e) {
                        Console.WriteLine(e.Message);
                        return false;
                    }

                }
            }
        }
    }
}
