using LouigisSP.BO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace LouigisSP.DL
{
    public static class PersonsData
    {

        public static List<Person> GetAllPersons() {
            //Log Persondata.GetAll persons called
            List<Person> persons = new List<Person>();
            List<Customer> customers = GetCustomers("customers.txt");
            List<Employee> employees = GetEmployees("employees.txt");
            //List<Admin> admins = GetAdministrators("admins.txt");

            foreach (Customer cst in customers) {
                persons.Add(cst);
            }
            foreach (Employee emp in employees) {
                persons.Add(emp);
            }
            return persons;
        }


        public static void AddPerson(Person person) {
            if (person is Admin) {
                AddAdministrator((Admin)person, Directory.GetCurrentDirectory() + @"\admins.txt");
            } else if (person is Employee) {
                AddEmployee((Employee)person, Directory.GetCurrentDirectory() + @"\employees.txt");
            } else if (person is Customer) {
                Console.WriteLine("current directory: "+ Directory.GetCurrentDirectory());
                AddCustomer((Customer)person, Directory.GetCurrentDirectory()+@"\customers.txt");
            }
        }



        public static List<Customer> GetCustomers(string path) {
            string[] customers=null;
            List<Customer> list_customers = new List<Customer>();

            try {
                customers = File.ReadAllLines(path);
                foreach (string customer in customers)
                {
                    string[] data = customer.Split(',');
                    Customer cst = new Customer();
                    cst.Id = int.Parse(data[0]);
                    cst.FirstName = data[1];
                    cst.LastName = data[2];
                    cst.PhoneNumber = data[3];
                    cst.Email = data[4];
                    cst.Pass = data[5];
                    cst.DateOfRegistration = DateTime.Parse(data[6]);
                    cst.DateOfBirth = DateTime.Parse(data[7]);
                    cst.ShippingAddress = data[8];
                    cst.BillingAddress = data[9];
                    list_customers.Add(cst);



                }
            }
            catch (FileNotFoundException e) {
                Console.WriteLine(e.Message);
            }
            catch (IOException e) {
                Console.WriteLine(e.Message);
            }

            //Login
            //Logging
           
            

            return list_customers;
        }

       


        public static List<Employee> GetEmployees(string path)
        {
            string[] employees = null;
            List<Employee> list_employees = new List<Employee>();

            try
            {
                employees = File.ReadAllLines(path);
                foreach (string employee in employees)
                {
                    string[] data = employee.Split(',');
                    Employee emp = new Employee();
                    emp.Id = int.Parse(data[0]);
                    emp.FirstName = data[1];
                    emp.LastName = data[2];
                    emp.PhoneNumber = data[3];
                    emp.Email = data[4];
                    emp.Pass = data[5];
                    emp.DateOfRegistration = DateTime.Parse(data[6]);
                    emp.DateOfBirth = DateTime.Parse(data[7]);
                    list_employees.Add(emp);
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e) {
                Console.WriteLine("Something went wrong");
            }



            return list_employees;
        }





        //public static List<Admin> GetAdministrators(string path)
        //{
        //    string[] admins = null;
        //    List<Admin> list_admins = new List<Admin>();

        //    try
        //    {
        //        admins = File.ReadAllLines(path);
        //        foreach (string admin in admins)
        //        {
        //            string[] data = admin.Split(',');
        //            Admin adm = new Admin();
        //            adm.Id = int.Parse(data[0]);
        //            adm.FirstName = data[1];
        //            adm.LastName = data[2];
        //            adm.PhoneNumber = data[3];
        //            adm.Email = data[4];
        //            adm.Pass = data[5];
        //            adm.DateOfRegistration = DateTime.Parse(data[6]);
        //            adm.DateOfBirth = DateTime.Parse(data[7]);
        //            list_admins.Add(adm);
        //        }
        //    }
        //    catch (FileNotFoundException e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //    catch (IOException e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }



        //    return list_admins;
        //}

        public static List<Admin> GetAdministrators(string path)
        {
            List<Admin> admins = new List<Admin>();
            if (!(path is null))
            {
                //verify that the file exists and read from it
                XmlDocument doc = new XmlDocument();
                doc.Load(Directory.GetCurrentDirectory() + @"\" + path);
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    String id = node.Attributes["Id"].Value;
                    Console.WriteLine("id: " + id);
                    if (node.HasChildNodes)
                    {
                        for (int i = 0; i < node.ChildNodes.Count; i++)
                        {
                            Console.WriteLine(node.ChildNodes[i].Name + " : " + node.ChildNodes[i].InnerText);
                        }
                    }

                }

            }
            return admins;
        }




        public static bool AddCustomer(Customer customer, string path) {

            try
            {
                string str_customer = customer.Id + "," + customer.FirstName + "," + customer.LastName + "," + customer.PhoneNumber + "," + customer.Email + "," + customer.Pass + "," + customer.DateOfRegistration + "," + customer.DateOfBirth + "," + customer.ShippingAddress + "," + customer.BillingAddress;
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(str_customer);
                    return true;

                }

            }
            
            catch (IOException e) {
                Console.WriteLine("error while writing to the file");
                return false;
            }
        }




        public static bool AddEmployee(Employee employee, string path)
        {

            try
            {
                string str_employee = employee.Id + "," + employee.FirstName + "," + employee.LastName + "," + employee.PhoneNumber + "," + employee.Pass + "," + employee.DateOfRegistration + "," + employee.DateOfBirth;

                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(str_employee);
                    return true;

                }

            }

            catch (IOException e)
            {
                Console.WriteLine("error while writing to the file");
                return false;
            }
        }


        public static bool AddAdministrator(Admin admin, string path)
        {

            try
            {
                string str_admin = admin.Id + "," + admin.FirstName + "," + admin.LastName + "," + admin.PhoneNumber + "," + admin.Pass + "," + admin.DateOfRegistration + "," + admin.DateOfBirth;


                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(str_admin);
                    return true;

                }

            }

            catch (IOException e)
            {
                Console.WriteLine("error while writing to the file");
                return false;
            }
        }


    }
}
