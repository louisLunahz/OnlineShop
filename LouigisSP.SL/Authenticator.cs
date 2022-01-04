using LouigisSP.BO;
using LouigisSP.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LouigisSP.SL
{
    public class Authenticator
    {


        public Customer GetCustomer(Tuple<string, string> customerCredentials) //Tuple<string email, string pass>
        {
            Customer obj_customer = null;
            IDbDataParameter[] parameters = GetParametersForPerson(customerCredentials);
            DataTable dataTable = DBManager.GetDataTable("sp_retrieveCustomerByEmailAndPass", CommandType.StoredProcedure, parameters);
            if (dataTable.Rows.Count >= 1)
            {
                Console.WriteLine("it was retrieved succesfully");
                DataRow row = dataTable.Rows[0];

                obj_customer = new Customer();
                ProcssPersonInformation(row, (Person)obj_customer);
                obj_customer.ShippingAddress = (string)row["shippingAddress"];
                obj_customer.BillingAddress = (string)row["billingAddress"];
            }
            else
            {
                Console.WriteLine("Error retrieving the user");
            }
            return obj_customer;
        }

        private IDbDataParameter[] GetParametersForPerson(Tuple<string, string> personCredentials) 
        {
            var emailP = DBManager.CreateParameter("@email", personCredentials.Item1, DbType.String);
            var passP = DBManager.CreateParameter("@pass", personCredentials.Item2, DbType.String);
            IDbDataParameter[] parameters = new IDbDataParameter[2];
            parameters[0] = emailP;
            parameters[1] = passP;

            return parameters;
        }

        private void  ProcssPersonInformation(DataRow row,  Person person)
        {
            
            person.Id = (int)row["id"];
            person.FirstName = (string)row["firstName"];
            person.LastName = (string)row["lastName"];
            person.PhoneNumber = (string)row["phoneNumber"];
            person.Email = (string)row["email"];
            person.Pass = (string)row["pass"];
            person.DateOfRegistration = (DateTime)row["dateOfRegistration"];
            person.DateOfBirth = (DateTime)row["dateOfBirth"];


        }

        public Employee GetEmployee(Tuple<string, string> employeeCredentials)
        {
            Employee obj_Employee = null;
            IDbDataParameter[] parameters = GetParametersForPerson(employeeCredentials);
            DataTable dataTable = DBManager.GetDataTable("sp_retrieveEmployeeByEmailAndPass", CommandType.StoredProcedure, parameters);
            if (dataTable.Rows.Count >= 1)
            {
                Console.WriteLine("it was retrieved succesfully");
                var rows = dataTable.Rows;
                DataRow row = dataTable.Rows[0];

                obj_Employee = new Employee();
                ProcssPersonInformation(row, (Person)obj_Employee);
            }
            else
            {
                Console.WriteLine("Error retrieving the user");
            }
            return obj_Employee;
        }

        public Customer SearchCustomerExistence(string email) {
            IDbDataParameter[] parameters = new IDbDataParameter[1];
            Customer obj_customer=null;
            parameters[0] = DBManager.CreateParameter("@email", email, DbType.String);
           
            DataTable dataTable = DBManager.GetDataTable("sp_checkCustomerExistence", CommandType.StoredProcedure, parameters);
            if (dataTable.Rows.Count >= 1)
            {
                DataRow row = dataTable.Rows[0];
                obj_customer = new Customer();
                ProcssPersonInformation(row, (Person)obj_customer);
                obj_customer.ShippingAddress = (string)row["shippingAddress"];
                obj_customer.BillingAddress = (string)row["billingAddress"];
            }
           



            return obj_customer;
        }


        public bool insertProduct(string []values) {
            bool wasInserted = false;
            try {
                if (values != null)
                {
                    int lastid;
                    IDbDataParameter[] parameters = new IDbDataParameter[values.Length];
                    parameters[0] = DBManager.CreateParameter("@name", values[0], DbType.String);
                    parameters[1] = DBManager.CreateParameter("@brand", values[1], DbType.String);
                    parameters[2] = DBManager.CreateParameter("@model", values[2], DbType.String);
                    parameters[3] = DBManager.CreateParameter("@color", values[3], DbType.String);
                    parameters[4] = DBManager.CreateParameter("@price", values[4], DbType.Currency);
                    parameters[5] = DBManager.CreateParameter("@stock", values[5], DbType.Int32);
                    parameters[6] = DBManager.CreateParameter("@extraInfo", values[6], DbType.String);
                    DBManager.Insert("spAddProductToProductsTable", commandType: CommandType.StoredProcedure, parameters, out lastid);
                    Console.WriteLine("last id: " + lastid);
                    wasInserted = true;

                }
               

            }
            catch (Exception e) {
                Console.WriteLine("there was an error while trying toinsert the value to the database");
            }
           
            return wasInserted; 
        
        }


        public bool InsertCustomer(string []values) {
         
            bool wasInserted = false;
            try {
                if (values != null)
                {
                    
                    IDbDataParameter[] parameters = new IDbDataParameter[9];
                    parameters[0] = DBManager.CreateParameter("@firstName", values[0], DbType.String);
                    parameters[1] = DBManager.CreateParameter("@lastName", values[1], DbType.String);
                    parameters[2] = DBManager.CreateParameter("@phoneNumber", values[2], DbType.String);
                    parameters[3] = DBManager.CreateParameter("@email", values[3], DbType.String);
                    parameters[4] = DBManager.CreateParameter("@pass", values[4], DbType.String);
                    parameters[5] = DBManager.CreateParameter("@dateOfRegistration",DateTime.Now, DbType.DateTime);
                    parameters[6] = DBManager.CreateParameter("@dateOfBirth", values[5], DbType.DateTime);
                    parameters[7] = DBManager.CreateParameter("@shippingAddress", values[6], DbType.String);
                    parameters[8] = DBManager.CreateParameter("@billingAddress", values[7], DbType.String);
                    DBManager.Insert("sp_InsertCustomerToCustomersTable", commandType: CommandType.StoredProcedure, parameters);
                    wasInserted = true;

                }
               

            }
            catch (Exception e) {
                Console.WriteLine("there was an error while trying toinsert the value to the database");
            }
           
            return wasInserted; 
        
        }





    }
}
