

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LouigisSP.DL;
using LouigisSP.BO;
using LouigisSP.SL;
using LougisSP.BO;
using System.Data;

namespace shoppingPortal
{
    class Program
    {
        static void Main(string[] args)
        {

            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }

        }

        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("++++++++++++++++WELCOME");
            Console.WriteLine("1.-Login as user");
            Console.WriteLine("2.-Login as Employee");
            Console.WriteLine("3.-signup to buy from the shop");
            Console.WriteLine("4.-exit");

            switch (Console.ReadLine())
            {
                case "1":
                    Person obj_customer = SignIn(new Customer());

                    if (obj_customer is null)
                    {
                        Console.WriteLine("Could not found that customer");
                    }
                    else
                    {
                        Menu(obj_customer);

                    }
                    Console.ReadKey();
                    return true;


                case "2":
                    Person obj_Employee = SignIn(new Employee());

                    if (obj_Employee is null)
                    {
                        Console.WriteLine("Could not found that Employee");
                    }
                    else
                    {
                        Menu(obj_Employee);
                    }
                    Console.ReadKey();
                    return true;
                case "3":
                    bool wasInserted = SignUP();
                    if (wasInserted)
                    {
                        Console.WriteLine("Customer registered succesfully");
                    }
                    else
                    {
                        Console.WriteLine("Error! Customer could not be registered");
                    }
                    Console.ReadKey();
                    return true;

                case "4":
                    return false;

                default:
                    Console.WriteLine("not valid option");
                    return true;

            }


        }




        private static Person SignIn(Person person)
        {


            Console.Clear();
            Console.WriteLine("Enter email");
            string email = Console.ReadLine();
            Console.WriteLine("Enter password");
            string pass = Console.ReadLine();
            Authenticator auth = new Authenticator();
            if (person is Customer)
            {
                return auth.GetCustomers(Tuple.Create(email, pass));

            }
            else if (person is Employee)
            {
                return auth.GetEmployees(Tuple.Create(email, pass));
            }
            else return null;



        }

        private static bool SignUP()
        {
            bool wasInserted ;
            Console.Clear();
            //call the service layer to signUP and the function returns a boolean to know if the customer was registered
            Console.WriteLine("Enter email");
            string email = Console.ReadLine();
            //call the service layer and call a function to know if the email is already in the customers table
            //if the user is in it, the user is retrieved, 
            //if the user is not, the functions returns null
            Authenticator obj_Authenticator = new Authenticator();
            Person obj_person = obj_Authenticator.SearchCustomerExistence(email);
            if (!(obj_person is null))//user is already registered
            {
                wasInserted = false;
            }
            else
            {
                Tuple<string, string>[] tuplesValues = new Tuple<string, string>[9];

                tuplesValues[3] = Tuple.Create("email", email);
                Console.WriteLine("Continue registration");

                Console.WriteLine("Enter your new password");
                string pass = Console.ReadLine();
                tuplesValues[4] = Tuple.Create("pass", pass);
                Console.WriteLine("Enter your first name");
                string fName = Console.ReadLine();
                tuplesValues[0] = Tuple.Create("firstName", fName);
                Console.WriteLine("Enter your last name");
                string lastName = Console.ReadLine();
                tuplesValues[1] = Tuple.Create("lastName", lastName);


                Console.WriteLine("Enter your phone number");
                string phone = Console.ReadLine();
                tuplesValues[2] = Tuple.Create("phoneNumber", phone);


                Console.WriteLine("Enter your Date of birth");
                string dob = Console.ReadLine();
                tuplesValues[6] = Tuple.Create("DateOfBirth", dob);


                Console.WriteLine("Enter your shipping Address");
                string shippingA = Console.ReadLine();
                tuplesValues[7] = Tuple.Create("shippingAddress", shippingA);


                Console.WriteLine("Enter your billing Address");
                string billingA = Console.ReadLine();
                tuplesValues[8] = Tuple.Create("billingAddress", billingA);
                tuplesValues[5] = Tuple.Create("dateOfRegistration", "2021-12-25");
                wasInserted = obj_Authenticator.InsertIntoTable(tuplesValues, "sp_insertCustomerIntoCustomers");



            }

            return wasInserted;

        }

        private static void Menu(Person obj_person)
        {
            //show menu for customers

            if (obj_person is Customer)
            {
                Customer obj_customer = (Customer)obj_person;
                ShoppingCart obj_shoppingCart = new ShoppingCart(obj_customer);

                string option = "";
                //menu for customers
                do
                {
                    Console.Clear();
                    Console.WriteLine("1.-Show all the products");
                    Console.WriteLine("2.-See cart");
                    Console.WriteLine("3.-show user information ");
                    Console.WriteLine("4.-end session");
                    switch (option = Console.ReadLine())
                    {
                        case "1":
                            buy(ref obj_shoppingCart);
                            break;

                        case "2":
                            if (obj_shoppingCart.ProductList.Count() >= 1)
                            {
                                Console.Clear();
                                showShoppingCart(ref obj_shoppingCart);
                                ShoppingCartOptions(ref obj_shoppingCart);
                            }
                            else
                            {
                                Console.WriteLine("There are no items in the cart yet");
                            }
                            break;

                        case "3":
                            Console.WriteLine(obj_customer.ToString());


                            break;

                    }

                } while (option != "4");


            }
            else if (obj_person is Employee)
            {
                //menu for employees 
                Console.WriteLine("employee menu");
            }





        }





        public static void ShowItems(List<Product> products)
        {
            //convert the list of objects into a list of the type given 
            Console.Clear();
            foreach (var p in products)
            {
                Console.WriteLine(p.ToString());
            }
        }

        public static bool buy(ref ShoppingCart sp)
        {
            QueryExecutor obj_QueryExecutor = new QueryExecutor();
            List<Object> products = obj_QueryExecutor.retrieveTableFromDatabase(new Product().GetType(), "Products");
            if (products != null)
            {
                List<Product> obj_listProducts = Utilities.ConvertList<List<Product>>(products, new Product().GetType());
                ShowItems(obj_listProducts);
                Console.WriteLine("To buy a product, insert the id");
                string id = Console.ReadLine();
                Product p = Utilities.searchProductInList(obj_listProducts, id);
                if (p != null)
                {
                    Console.Clear();
                    Console.WriteLine(p.ToString());
                    Console.WriteLine("Insert the quantity");
                    int quantity = int.Parse(Console.ReadLine());
                    bool isStockEnough = Utilities.CheckQuantity(p, quantity);
                    if (isStockEnough)
                    {
                        sp.ProductList.Add(Tuple.Create(p, quantity));
                        Console.Clear();
                        Console.WriteLine("Product added successfully to the cart");
                    }
                    else
                    {
                        Console.WriteLine("Not enough stock");
                    }

                }

            }

            return true;

        }

        public static void showShoppingCart(ref ShoppingCart obj_shoppingCart)
        {
            float total = 0;
            foreach (var item in obj_shoppingCart.ProductList)
            {
                total += item.Item1.Price * item.Item2;

                item.Item1.showDetails();
                Console.WriteLine(item.Item1.Price * item.Item2);
                Console.WriteLine("quantity: " + item.Item2);
            }
            Console.WriteLine("Total: " + total);
        }

        public static void ShoppingCartOptions(ref ShoppingCart obj_shoppingCart)
        {
            string option;
            string[] fields = new String[] { "idCustomer", "idProduct", "quantity", "orderDate" };
            do
            {
                Console.WriteLine("1.-Make Payment ");
                Console.WriteLine("2.-Remove item from shopping cart");
                Console.WriteLine("3.-return");
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        if (obj_shoppingCart.ProductList.Count >= 1)
                        {

                            ShoppingCartOperations obj_shoppingCartOperations = new ShoppingCartOperations();
                            if (obj_shoppingCartOperations.MakePayment(ref obj_shoppingCart, fields))
                            {
                                Console.WriteLine("Payment done");
                            }
                            else
                            {
                                Console.WriteLine("Failed to process the transaction");
                            }
                        }
                        else
                        {
                            Console.WriteLine("There are no products in the shopping cart");
                        }


                        break;
                    case "2":
                        Console.Clear();

                        if (obj_shoppingCart.ProductList.Count() >= 1)
                        {
                            showShoppingCart(ref obj_shoppingCart);
                            Console.WriteLine("----------------------------------------------------");
                            Console.WriteLine("Insert the id of the product to be deleted from the cart");
                            string id = Console.ReadLine();
                            bool wasRemoved = obj_shoppingCart.DeleteProductFromList(id);
                            if (wasRemoved)
                            {
                                Console.WriteLine("Item removed succesfully from the shopping cart");
                            }
                            else
                            {
                                Console.WriteLine("Could not find the item");
                            }

                        }
                        else
                        {
                            Console.WriteLine("There are no items in the shopping cart");
                        }


                        break;
                    default:
                        break;
                }

            } while (option != "3");

        }

    }


}
