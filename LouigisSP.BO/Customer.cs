using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace LouigisSP.BO
{
    public class Customer : Person, IEditInfo
    {
        string shippingAddress;
        string billingAddress;
        public Customer() : base()
        {

        }
        
        public string ShippingAddress { get => shippingAddress; set => shippingAddress = value; }
        public string BillingAddress { get => billingAddress; set => billingAddress = value; }


        public Customer(int id, string firstName, string lastName, string phoneNumber, string email, string pass, DateTime dateOfRegistration, DateTime dateOfBirth, string shippingAddress, string billingAddress ) : base(id, firstName, lastName, phoneNumber, email, pass, dateOfRegistration, dateOfBirth)
        {
            this.shippingAddress = shippingAddress;
            this.billingAddress = billingAddress;
        }

        public void EditInfo()
        {
            Console.WriteLine("enter firs Name");
             Console.ReadLine();


        }
    }
}
