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
            //getting the data from the csv files
            bool passCorrect;
            var cr = new CsvReader<Customer>();
            var cr2 = new CsvReader<Admin>();
            var cr3 = new CsvReader<Employee>();
            var cr4 = new CsvReader<Product>();
            var customers = cr.Read("customers.csv", false).ToList();
            var products = cr4.Read("products.csv", false).ToList();
            var admins = cr2.Read("admins.csv", false).ToList();
            var employees = cr3.Read("employees.csv", false).ToList();
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
                        if (person is Admin)
                        {
                            Console.WriteLine("Welcome Admin: " + person.FirstName + " " + person.LastName);
                            Console.ReadKey();
                        }
                        else if (person is Employee)
                        {
                            Console.WriteLine("Welcome Employee: " + person.FirstName + " " + person.LastName);
                            Console.ReadKey();
                        }
                        else if (person is Customer)
                        {
                            Console.WriteLine("Welcome customer: "+ person.FirstName+"  "+person.LastName);
                            int optionProducts;
                            bool valid;
                            do
                            {

                                Console.Clear();
                                Console.WriteLine("1.-Show all products");
                                Console.WriteLine("2.---------------------");
                                Console.WriteLine("3.---------------------");
                                Console.WriteLine("4.-exit");
                                valid = int.TryParse(Console.ReadLine(), out optionProducts);

                                switch (optionProducts)
                                {
                                    case 1:
                                        Console.Clear();
                                        ShowProducts(products);
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



                            } while (optionProducts != 4);
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
                        customer.Id = customers.Count()+1;
                        
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



        public static Person SearchPersonByEmail(string email, List<Person>persons) {
           Person per  =
               (from person in persons
                where person.Email == email
                select person).SingleOrDefault<Person>();
            return per;

            
        }


        public static void ShowProducts(List<Product> producs) {
            foreach (Product p in producs) {
                Console.WriteLine(p.ToString());
            }
        }


    }
}
