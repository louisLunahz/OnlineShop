using LouigisSP.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LouigisSP.DL;

namespace LouigisSP.SL
{
    public class ProductOperations
    {

        public void ShowProducts(List<Product> producs)
        {
            foreach (Product p in producs)
            {
                Console.WriteLine(p.ToString());
            }
        }

        public List<Product> getProducts() {
            ProductDAL prDAL = new ProductDAL();
           return  prDAL.GetAllProducts();
        }
    }
}
