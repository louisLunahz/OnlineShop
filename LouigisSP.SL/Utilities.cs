using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LouigisSP.BO;

namespace LouigisSP.SL
{
    public class Utilities {

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

     

        public static T ConvertList<T>(List<Object> list1, Type type)
        {
            Type listType = typeof(List<>).MakeGenericType(new[] { type });
            IList list = (IList)Activator.CreateInstance(listType);
            foreach (var element in list1) {
                list.Add(element);
            }
            return (T)list;

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
