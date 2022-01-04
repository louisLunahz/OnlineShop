using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LouigisSP.BO;

namespace LougisSP.BO
{
    public class ShoppingCart
    {

        Customer cus;
        List<Tuple<Product, int>> productList;
        float total;
        public ShoppingCart(Customer cus)
        {
            this.cus = cus;
            productList = new List<Tuple<Product, int>>();
            total = 0;
        }

        public Customer Cus { get => cus; set => cus = value; }
        public List<Tuple<Product, int>> ProductList { get => productList; set => productList = value; }
        public float Total { get => total; set => total = value; }

        public  bool DeleteProductFromList( string id)
        {
            bool wasDeleted = false;
            for (int i = 0; i < productList.Count(); i++)
            {
                if (productList.ElementAt(i).Item1.Id == int.Parse(id))
                {
                    productList.RemoveAt(i);
                    wasDeleted = true;
                    break;
                }

            }
            return wasDeleted;
        }
    }
}
