using LouigisSP.BO;
using LouigisSP.DL;
using LouigisSP.SL.Exceptions;
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

            if (customerCredentials is null || customerCredentials.Item1.Length == 0 || customerCredentials.Item2.Length == 0)
            {
                throw new InvalidCredentialsException("given credential are null or empty");
            }
            else{
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
                    throw new UserNotFoundException("User could not be retrieved");
                }
                return obj_customer;

            }

           
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
            if (employeeCredentials is null || employeeCredentials.Item1 is null || employeeCredentials.Item2 is null)
            {

                throw new InvalidCredentialsException("Credentials given are null or empty");
            }
            else {
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
                    throw new UserNotFoundException("User could not be retrieved");
                }
                return obj_Employee;
            }
            
        }

        public Customer SearchCustomerExistence(string email) {
            if (email is null || email=="")
            {
                throw new NullParameterException("email can't be empty");
            }
            IDbDataParameter[] parameters = new IDbDataParameter[1];
            
            parameters[0] = DBManager.CreateParameter("@email", email, DbType.String);
           
            DataTable dataTable = DBManager.GetDataTable("sp_checkCustomerExistence", CommandType.StoredProcedure, parameters);
            if (dataTable.Rows.Count >= 1)
            {
                
                DataRow row = dataTable.Rows[0];
                Customer obj_customer = new Customer();
                ProcssPersonInformation(row, (Person)obj_customer);
                obj_customer.ShippingAddress = (string)row["shippingAddress"];
                obj_customer.BillingAddress = (string)row["billingAddress"];
                return obj_customer;
            }
            else {
                throw new UserNotFoundException("Customer could not be found");
            }
           



            
        }


        public void insertProduct(string []values) {
            if (values is null)
            {
                throw new NoValuesToInsertException("No values to be inserted were found");
            }
            else {

                if (Validator.CheckProductParameters(values))
                {
                    try
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
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }

                }
                else {
                    throw new NoValuesToInsertException();
                }
               
            }
         
           
           
        
        }


        public void InsertCustomer(string []values) {
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
                }
               
            }
            catch (Exception e) {
                Console.WriteLine("Error while trying to insert to the database");
                throw new DatabaseInsertionException();
            }
        }





    }
}
