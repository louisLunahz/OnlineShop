using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LouigisSP.BO;


namespace LouigisSP.SL
{
    public class Searcher
    {
        public static Product GetProductFromList(List<Product> products, string id)
        {
            Product pdt =
                (from product in products
                 where product.Id == id
                 select product).SingleOrDefault<Product>();
            return pdt;
        }

    }
}
