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
        private List<Product> listProducts;
        private List<Customer> listCustomers;
        private List<Employee> listEmployees;
        private List<Order> listOrders;
        private List<Sell> listSells;
        public Authenticator()
        {
            listProducts = getAllProducts();
            listCustomers = GetAllCustomers();
            listEmployees = GetAllEmployees();
            listOrders = GetAllOrders();
            listSells = GetAllSells();
        }
        public void SaveAll()
        {
            var customersWriter = new CsvWriter<Customer>();
            customersWriter.Write(listCustomers, "customers.csv");
            var employeesWritter = new CsvWriter<Employee>();
            employeesWritter.Write(listEmployees, "employees.csv");
            var ProductsWriter = new CsvWriter<Product>();
            ProductsWriter.Write(listProducts, "products.csv");

            var ordersWriter = new CsvWriter<Order>();
            ordersWriter.Write(listOrders, "orders.csv");
            var sellsWriter = new CsvWriter<Sell>();
            sellsWriter.Write(listSells, "sells.csv");



        }
        private List<Product> getAllProducts()
        {
            List<Product> products;
            var csvReader = new CsvReader<Product>();
            products = csvReader.Read("products.csv", false).ToList();
            return products;
        }
        public List<Product> GetAllProducts() {
            return listProducts;
        }

        private List<Customer> GetAllCustomers()
        {
            List<Customer> customers;
            var csvReader = new CsvReader<Customer>();
            customers = csvReader.Read("customers.csv", false).ToList();
            return customers;
        }

        private List<Employee> GetAllEmployees()
        {
            List<Employee> employees;
            var csvReader = new CsvReader<Employee>();
            employees = csvReader.Read("employees.csv", false).ToList();
            return employees;
        }

        private List<Order> GetAllOrders() {
            List<Order> orders;
            var csvReader = new CsvReader<Order>();
            orders = csvReader.Read("orders.csv", false).ToList();
            return orders;
        }

        private List<Sell> GetAllSells()
        {
            List<Sell> sells;
            var csvReader = new CsvReader<Sell>();
            sells = csvReader.Read("sells.csv", false).ToList();
            return sells;
        }






        public Customer GetCustomer(Tuple<string, string> customerCredentials)
        {
            if (customerCredentials is null || customerCredentials.Item1.Length == 0 || customerCredentials.Item2.Length == 0)
            {
                throw new InvalidCredentialsException("given credential are null or empty");
            }

            Customer obj_customer = null;
            obj_customer = listCustomers.Where(x => x.Email == customerCredentials.Item1 && x.Pass == customerCredentials.Item2).FirstOrDefault();

            if (obj_customer != null)
            {

                return obj_customer;
            }
            else
            {
                throw new UserNotFoundException("User could not be retrieved");
            }

        }

        public Employee GetEmployee(Tuple<string, string> employeeCredentials)
        {
            if (employeeCredentials is null || employeeCredentials.Item1.Length == 0 || employeeCredentials.Item2.Length == 0)
            {
                throw new InvalidCredentialsException("given credential are null or empty");
            }

            Employee obj_Employee = null;
            obj_Employee = listEmployees.Where(x => x.Email == employeeCredentials.Item1 && x.Pass == employeeCredentials.Item2).FirstOrDefault();

            if (obj_Employee != null)
            {

                return obj_Employee;
            }
            else
            {
                throw new UserNotFoundException("User could not be retrieved");
            }

        }

        public Product GetProduct(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIDException("Invalid Id");
            }

            Product obj_product = null;
            obj_product = listProducts.Where(x => x.Id == id).FirstOrDefault();

            if (obj_product != null)
            {

                return obj_product;
            }
            else
            {
                throw new ProductNotFoundException("Product could not be retrieved");
            }
        }

        public Customer SearchCustomerExistence(string email)
        {
            {
                try
                {
                    Validator.CheckEmail(email);
                }
                catch (Exception e)
                {
                    throw new InvalidCredentialsException("email bad formated");
                }
                Customer obj_customer = null;
                obj_customer = listCustomers.Where(x => x.Email == email).FirstOrDefault();
                if (obj_customer != null)
                {
                    return obj_customer;
                }
                else throw new UserNotFoundException("Customer not found");

            }
            






        }

        public void InserProduct(Product obj_product)
        {
            if (obj_product is null)
            {
                throw new ProductNotFoundException("No product was given to insert");
            }
            listProducts.Add(obj_product);
        }


        public void InsertCustomer(Customer obj_Customer)
        {
            if (obj_Customer is null)
            {
                throw new UserNotFoundException("No user was given to insert");
            }
            listCustomers.Add(obj_Customer);
        }
        public void InsertEmployee(Employee obj_employee)
        {
            if (obj_employee is null)
            {
                throw new UserNotFoundException("No employee was given to insert");
            }
            listEmployees.Add(obj_employee);
        }

        public int GetLasCustomerId() {
            int id;
            Customer cus= listCustomers.OrderByDescending(c => c.Id).FirstOrDefault();
            if (cus != null)
            {
                id = cus.Id;
                return id;
            }
            else {
                throw new IDNotFoundException("Could not found the last Id");
            }
            
        }


        public int GetLastProductId()
        {
            int id;
            Product product = listProducts.OrderByDescending(c => c.Id).FirstOrDefault();
            if (product != null)
            {
                id = product.Id;
                return id;
            }
            else
            {
                throw new IDNotFoundException("Could not found the last Id");
            }

        }

        public int GetLastOrderId()
        {
            int id;
            Order order = listOrders.OrderByDescending(c => c.Id).FirstOrDefault();
            if (order != null)
            {
                id = order.Id;
                return id;
            }
            else
            {
                throw new InvalidIDException("Could not found the last Id");
            }

        }

        public bool setOrder(int idCustomer, List<Tuple<int, int>> products)
        {
            bool orderSet = false;
            if (products != null)
            {
                //get last order id
                try {
                    int lastOrder = GetLastOrderId();

                    bool orderInserted = InsertOrder(idCustomer);
                    bool sellInserted = InsertSell(products, lastOrder);
                    if (orderInserted && sellInserted)
                    {
                        orderSet = true;
                    }
                }
                catch (InvalidIDException e) {
                    throw e;
                }
                catch (DatabaseInsertionException e) {
                    throw e;
                }
               
              


            }
            return orderSet;

        }

        private bool InsertOrder(int idCustomer)
        {
            
            if (idCustomer<=0)
            {
                throw new Exception();
            }
            try {
                int id = GetLastOrderId() + 1;
                Order order = new Order(id, idCustomer, DateTime.Now.Date, "Waiting for delivery");
                listOrders.Add(order);
                return true;
            }
            catch (InvalidIDException e) {
                throw e;
            }
           

        }

        private bool InsertSell(List<Tuple<int, int>> products, int lastOrder)
        {
               
            if (products is null || lastOrder<=0) {
                throw new DatabaseInsertionException("could not enter the row into the database");
            }
            //tuple <idProduct, quantity>
            bool wasInserted = false;
            for (int i = 0; i < products.Count; i++)
            {

                Sell sell = new Sell();
                sell.idProduct = products.ElementAt(i).Item1;
                sell.idOrder = lastOrder + 1;
                sell.quantity = products.ElementAt(i).Item2;
                listSells.Add(sell);


                wasInserted = true;
            }

            return wasInserted;

        }

    }
}
