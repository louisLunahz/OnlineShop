using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LouigisSP.BO;

namespace LouigisSP.SL
{
    public static class Utilities {

        public static Product searchProductInList(List<Product> products, string id) {
            Product p = null;
            int int_id;
            if (int.TryParse(id, out int_id)) {
                foreach (Product product in products)
                {
                    if (product.Id == int_id)
                    {
                        p = product;
                    }

                }

            }
            
            return p;
        }

        public static bool CheckQuantity(Product p, int quantity)
        {
            if (quantity <= 0)
            {
                return false;
            }
            else if (p.Stock < quantity)
            {
                return false;
            }
            else return true;
        }
    }
}
