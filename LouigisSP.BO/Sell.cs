using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LouigisSP.BO
{
    public class Sell: Fileable
    {
       
        public int idProduct { get; set; }
        public int idOrder { get; set; }
        public int quantity { get; set; }

        public Sell( int idProduct, int idOrder, int quantity)
        {
          
            this.idProduct = idProduct;
            this.idOrder = idOrder;
            this.quantity = quantity;
        }

        public Sell()
        {
        }
    }
}
