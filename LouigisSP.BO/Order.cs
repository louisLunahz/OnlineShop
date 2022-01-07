using LouigisSP.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LouigisSP.BO
{
    public class Order: Fileable
    {
        public int Id { get; set; }
        public int IdCustomer { get; set; }
        public DateTime orderDate { get; set; }

        public string Status { get; set; }

        public Order(int id, int idProduct, DateTime orderDate, string status)
        {
            Id = id;
            this.IdCustomer = idProduct;
            this.orderDate = orderDate;
            Status = status;
        }

        public Order()
        {
        }
    }
}
