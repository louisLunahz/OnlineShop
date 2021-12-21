using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LouigisSP.DL;
using LouigisSP.BO;

namespace LouigisSP.SL
{
    public class CustomerOperations
    {
        public List<Customer> getCustomers() {
            CustomerDAL custDAL = new CustomerDAL();
           return custDAL.getAllCustomers();
           
        }

        public Customer getCustomerbyId(int id) {
            CustomerDAL custDAL = new CustomerDAL();
            return custDAL.GetCustomerFromDB(id);
        }

        

        

    }
}
