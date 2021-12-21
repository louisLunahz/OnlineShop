using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LouigisSP.DL;
using LouigisSP.BO;
using LouigisSP.SL;



namespace shoppingPortal
{
    class Program
    {
        static void Main(string[] args)
        {


            CustomerOperations co = new CustomerOperations();
            ProductOperations po = new ProductOperations();

            
            var cr2 = new CsvReader<Admin>();
            var cr3 = new CsvReader<Employee>();

            var customers = co.getCustomers();
            var products = po.getProducts();
            var admins = cr2.Read("admins.csv", false).ToList();
            var employees = cr3.Read("employees.csv", false).ToList();
            bool passCorrect;
            List<Person> persons = customers.Concat<Person>(employees).Concat<Person>(admins).ToList();
            bool validOption;
            int option;
            do
            {
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("++++++++++++++++WELCOME");
                Console.WriteLine("1.-Sign in ");
                Console.WriteLine("2.-Sign up");
                Console.WriteLine("3.-exit");
                validOption = int.TryParse(Console.ReadLine(), out option);
                switch (option)
                {
                    case 1:
                        Person person;

                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Enter email");
                            person = SearchPersonByEmail(Console.ReadLine(), persons);
                        } while (person == null);
                        do
                        {
                            Console.WriteLine("Enter pass");
                            passCorrect = Validator.ComparePass(Console.ReadLine(), person);

                        } while (!passCorrect);
                        if (person is Admin)//person is admin
                        {
                            Console.WriteLine("Welcome Admin: " + person.FirstName + " " + person.LastName);
                            //AdminMenu();




                        }
                        else if (person is Employee)//person is employee
                        {
                            Console.WriteLine("Welcome Employee: " + person.FirstName + " " + person.LastName);
                            Console.ReadKey();
                        }
                        else if (person is Customer)//person is customer
                        {
                            Console.WriteLine("Welcome customer: " + person.FirstName + "  " + person.LastName);
                            int optionProducts;
                            bool valid;
                            do
                            {

                                Console.Clear();
                                Console.WriteLine("1.-Show all products");
                                Console.WriteLine("2.-exit");
                                valid = int.TryParse(Console.ReadLine(), out optionProducts);

                                switch (optionProducts)
                                {
                                    case 1:
                                        Console.Clear();
                                        po.ShowProducts(products);
                                        string idKeyboard;
                                        Product product;

                                        do
                                        {
                                            Console.WriteLine(" Enter the id of the product to see full details or r to return ");
                                            idKeyboard = Console.ReadLine();
                                            if (idKeyboard == "r")
                                            {
                                                Console.WriteLine("leaving...");
                                                Console.ReadKey();
                                                break;
                                            }

                                            product = Searcher.GetProductFromList(products, idKeyboard);

                                            if (product is null)
                                            {
                                                Console.WriteLine("not found");
                                                Console.ReadKey();
                                            }
                                            else
                                            {
                                                product.PrintAllDetails();
                                                Console.ReadKey();
                                            }
                                        } while (idKeyboard == "" || idKeyboard is null);




                                        break;

                                        //case 2:
                                        //    Console.Clear();
                                        //    ShowProducts(products, new Product());
                                        //    Console.ReadKey();
                                        //    break;

                                        //case 3:

                                        //    Console.Clear();
                                        //    ShowProducts(products, new Keyboard());
                                        //    Console.ReadKey();
                                        //    break;
                                }



                            } while (optionProducts != 2);
                        }
                        break;

                    case 2:
                        person = null;
                        string pass;
                        bool nameCorrect;
                        string email;
                        bool emailCorrect;
                        string shippingAddress;
                        string billingAdreess;
                        bool isValidNumber;
                        string phoneNumber;
                        bool isValidDOB;
                        string firstName;
                        string lastName;
                        string dateOfBirth;

                        Customer customer = new Customer();


                        do
                        {
                            Console.WriteLine("Enter email to register");
                            email = Console.ReadLine();
                            emailCorrect = Validator.CheckEmail(email);
                            person = SearchPersonByEmail(email, customers.Cast<Person>().ToList());
                            if (!emailCorrect)
                            {
                                Console.WriteLine("email bad formated");
                            }
                            if (person != null)
                            {
                                Console.WriteLine("email  already registered");
                            }
                        } while (!emailCorrect || !(person is null));
                        customer.Email = email;
                        do
                        {
                            Console.WriteLine("Enter password: ");
                            passCorrect = Validator.VerifyPassword(pass = Console.ReadLine());
                        } while (passCorrect == false);


                        do
                        {
                            Console.WriteLine("Enter password again: ");
                            passCorrect = Console.ReadLine() == pass;
                        } while (passCorrect == false);

                        customer.Pass = pass;

                        do
                        {
                            Console.WriteLine("Enter First Name");
                            nameCorrect = Validator.CheckName(firstName = Console.ReadLine());
                        } while (nameCorrect == false);
                        customer.FirstName = firstName;
                        do
                        {
                            Console.WriteLine("Enter Last Name");
                            nameCorrect = Validator.CheckName(lastName = Console.ReadLine());
                        } while (nameCorrect == false);
                        customer.LastName = lastName;
                        do
                        {
                            Console.WriteLine("Enter shipping Adress");
                            shippingAddress = Console.ReadLine();
                        } while (string.IsNullOrEmpty(shippingAddress));
                        customer.ShippingAddress = shippingAddress;

                        do
                        {
                            Console.WriteLine("Enter billing Adress");
                            billingAdreess = Console.ReadLine();
                        } while (string.IsNullOrEmpty(billingAdreess));
                        customer.BillingAddress = billingAdreess;
                        do
                        {
                            Console.WriteLine("Enter your phone number");
                            isValidNumber = Validator.CheckPhoneNumber(phoneNumber = Console.ReadLine());
                        } while (!isValidNumber);

                        customer.PhoneNumber = phoneNumber;
                        do
                        {
                            Console.WriteLine("Enter your date of Birth");
                            isValidDOB = Validator.CheckDateOfBirth(dateOfBirth = Console.ReadLine());
                        } while (!isValidDOB);
                        customer.DateOfBirth = DateTime.Parse(dateOfBirth);
                        customer.Id = customers.Count() + 1;

                        customers.Add(customer);
                        Console.WriteLine("Customer registered succesfully");
                        Console.ReadKey();
                        break;

                    case 3:
                        var cw = new CsvWriter<Person>();
                        cw.Write(customers, "customers.csv");
                        break;
                }
            } while (option != 3);
        }



        public static Person SearchPersonByEmail(string email, List<Person> persons)
        {
            Person per =
                (from person in persons
                 where person.Email == email
                 select person).SingleOrDefault<Person>();
            return per;


        }


       

        public static void AdminMenu()
        {
            int option = -1;
            do
            {
                Console.WriteLine("1.-Register a new admin");
                Console.WriteLine("2.-Register a new Employee");
                Console.WriteLine("3.-exit");
                try
                {
                    option = int.Parse(Console.ReadLine());
                    switch (option)
                    {
                        case 1:

                            Console.WriteLine("Enter first name ");
                            string fname = Console.ReadLine();
                            Console.WriteLine("Enter last name");
                            string lname = Console.ReadLine();
                            Console.WriteLine("Enter email");
                            string email = Console.ReadLine();
                            Console.WriteLine("Enter pass");
                            string pass = Console.ReadLine();
                            Console.WriteLine("Enter date of birth");

                            DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());



                            break;


                    }
                }
                catch (System.FormatException e)
                {
                    Console.WriteLine("Invalid Option");
                }



            } while (option != 3);
        }


    }
}
