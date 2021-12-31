using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LouigisSP.BO
{
    public class Employee : Person
    {
        private bool isAdmin;
        public Employee(): base() { 
        
        }

        public Employee(int id, string firstName, string lastName, string phoneNumber, string email, string pass, DateTime dateOfRegistration, DateTime dateOfBirth, bool isAdmin ) : base(id, firstName, lastName, phoneNumber, email, pass, dateOfRegistration, dateOfBirth)
        {
            this.isAdmin = isAdmin;

        }

        public Employee(string id, string firstName, string lastName, string phoneNumber, string email, string pass, string dateOfRegistration, string dateOfBirth,  string isAdmin)
           : base(int.Parse(id), firstName, lastName, phoneNumber, email, pass, DateTime.Parse(dateOfRegistration), DateTime.Parse(dateOfBirth))
        {

            this.isAdmin = bool.Parse(isAdmin);
        }

        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
