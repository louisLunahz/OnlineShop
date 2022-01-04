using System;
using System.Collections.Generic;
using System.Data;
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

        //public bool MakePayment(ref ShoppingCart obj_shoppingCart, string[] fields)
        //{
        //    List<Tuple<string, string>[]> rowsToBeInserted = new List<Tuple<string, string>[]>();//list with the rows to be inserted in the table
        //    bool wasPayed = false;
        //    List<Tuple<Product, int>> cartProducts = obj_shoppingCart.ProductList;//list of products in shopping cart
        //                                                                          //we have to send a list of arrays of tuples to the InsertMultipleRowsIntoTable method

        //    for (int i = 0; i < cartProducts.Count(); i++)
        //    {//for each element in the list of products

        //        Tuple<string, string>[] values = new Tuple<string, string>[4];    //create an array of tuples 
        //                                                                          //tuple<nameOfParameterInStoredProcedure, actualString>
        //        values[0] = Tuple.Create(fields[0], obj_shoppingCart.Cus.Id.ToString());
        //        values[1] = Tuple.Create(fields[1], cartProducts.ElementAt(i).Item1.Id);
        //        values[2] = Tuple.Create(fields[2], cartProducts.ElementAt(i).Item2.ToString());
        //        string date = DateTime.Now.Date.ToString("yyyy-MM-dd");
        //        values[3] = Tuple.Create(fields[3], date);
        //        rowsToBeInserted.Add(values);

        //    }
        //    QueryExecutor queryExecutor = new QueryExecutor();
        //    wasPayed = queryExecutor.InsertMultipleRowsIntoTable(rowsToBeInserted, "sp_insertIntoSells");
        //    if (wasPayed)
        //    {
        //        obj_shoppingCart.ProductList.Clear();//clear the list of products in the shopping cart
        //    }

        //    return wasPayed;
        //}


        public bool setOrder(int idCustomer, List<Tuple<int, int>> products)
        {
            bool orderSet = false;
            if (products != null)
            {
                //get last order id
               int lastOrder= GetLastOrderId();

                bool orderInserted = InsertOrder(idCustomer);
                bool sellInserted = InsertSell( products, lastOrder);
                if (orderInserted && sellInserted) {
                    orderSet = true;
                }
               
                
            }
            return orderSet;

        }

        private bool InsertOrder(int idCustomer)
        {
            //insert order into orders table
            bool wasInserted = false;

            IDbDataParameter[] parameters = new IDbDataParameter[3];

            
            parameters[0] = DBManager.CreateParameter("@idCustomer", idCustomer, DbType.Int32);
            parameters[1] = DBManager.CreateParameter("@orderDate", DateTime.Now, DbType.DateTime);
            parameters[2] = DBManager.CreateParameter("@st", "waiting for delivery", DbType.String);
           

            DBManager.Insert("sp_insertIntoOrders", commandType: CommandType.StoredProcedure, parameters);
            wasInserted = true;
            return wasInserted;

        }

        private bool InsertSell(List<Tuple<int, int>> products, int lastOrder)
        {
            //tuple <idProduct, quantity>
            bool wasInserted = false;
            for (int i=0; i<products.Count; i++) {
                IDbDataParameter[] parameters = new IDbDataParameter[3];


                parameters[0] = DBManager.CreateParameter("@idProduct", products.ElementAt(i).Item1, DbType.Int32);
                parameters[1] = DBManager.CreateParameter("@idOrder", lastOrder+1, DbType.Int32);
                parameters[2] = DBManager.CreateParameter("@quantity", products.ElementAt(i).Item2, DbType.Int32);
                DBManager.Insert("sp_insertIntoSells", commandType: CommandType.StoredProcedure, parameters);
                wasInserted= true; 
            }

            return wasInserted; 
        
        }

        private int GetLastOrderId() {
            
          object id = DBManager.GetScalarValue("sp_getLastOrderId", commandType: CommandType.StoredProcedure);
            return (int)id;
        
        }

    }
}
