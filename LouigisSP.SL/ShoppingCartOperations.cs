using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LougisSP.BO;
using LouigisSP.BO;
using LouigisSP.DL;

namespace LouigisSP.SL
{
    public class ShoppingCartOperations
    {

        public bool MakePayment(ref ShoppingCart obj_shoppingCart, string []fields) {
            List<Tuple<string, string>[]> rowsToBeInserted = new List<Tuple<string, string>[]>();//list with the rows to be inserted in the table
            bool wasPayed = false;
            List<Tuple<Product, int>> cartProducts = obj_shoppingCart.ProductList;//list of products in shopping cart
            //we have to send a list of arrays of tuples to the InsertMultipleRowsIntoTable method
           
            for (int i=0; i< cartProducts.Count(); i++) {//for each element in the list of products

                Tuple<string, string>[] values = new Tuple<string, string>[4];    //create an array of tuples 
                 //tuple<nameOfParameterInStoredProcedure, actualString>
                values[0] = Tuple.Create(fields[0], obj_shoppingCart.Cus.Id.ToString());
                values[1] = Tuple.Create(fields[1], cartProducts.ElementAt(i).Item1.Id);
                values[2] = Tuple.Create(fields[2], cartProducts.ElementAt(i).Item2.ToString());
                string date=DateTime.Now.Date.ToString("yyyy-MM-dd");
                values[3] = Tuple.Create(fields[3], date);
                rowsToBeInserted.Add(values);

            }
            QueryExecutor queryExecutor = new QueryExecutor();
          wasPayed=  queryExecutor.InsertMultipleRowsIntoTable(rowsToBeInserted, "sp_insertIntoSells");
            if (wasPayed) {
                obj_shoppingCart.ProductList.Clear();//clear the list of products in the shopping cart
            }

            return wasPayed;
        }
    }
}
