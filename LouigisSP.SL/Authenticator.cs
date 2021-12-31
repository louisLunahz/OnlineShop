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


        public Customer GetCustomers(Tuple<string, string> customerCredentials)
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

        public Employee GetEmployees(Tuple<string, string> employeeCredentials)
        {
            Employee obj_Employee = null;
            IDbDataParameter[] parameters = GetParametersForPerson(employeeCredentials);
            DataTable dataTable = DBManager.GetDataTable("sp_retrieveCustomerByEmailAndPass", CommandType.StoredProcedure, parameters);
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
            else
            {
                Console.WriteLine("Error retrieving the user");
            }



            return obj_customer;
        }
        

     


        public Person FindUserByEmail(string email)
        {
            Person obj_person = null;
           
            if (email != null && email != "")
            {
                
            }
            return obj_person;

        }

        public bool InsertIntoTable(Tuple<string, string>[] values, string procedureName)
        {
            bool isInserted = false;
            //call the data layer
            for (int i = 0; i < values.Length; i++)
            {
                Console.WriteLine(values[i].Item1 + "      " + values[i].Item2);
            }
            QueryExecutor obj_QueryExecutor = new QueryExecutor();
            isInserted = obj_QueryExecutor.insertObjectIntoTable(values, procedureName);

            return isInserted;
        }




    }
}
