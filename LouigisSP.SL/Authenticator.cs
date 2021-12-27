using LouigisSP.BO;
using LouigisSP.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LouigisSP.SL
{
   public class Authenticator
    {

        public Object getSession(Type type, string email, string pass, string storedProcedureName) {
            //returns an object with the user, migh be customer or employee
            Object user = null;
            //get the properties
            QueryExecutor qu = new QueryExecutor();

             user= qu.retrieveUserFromDatabase(type, email, pass, storedProcedureName);
            return user;
        }

        public Object FindUserByEmail(string email) {
            Object obj = null;
            //here we need to call the data layer with the info 
            if (email != null && email != "")
            {
                Console.WriteLine("calling the data layer");
            }
            return obj;

        }

        public Boolean insertIntoTable(Tuple<string, string>[] values, string procedureName) {
            bool isInserted = false;
            //call the data layer
            for (int i= 0; i < values.Length; i++) {
                Console.WriteLine(values[i].Item1+"      "+values[i].Item2);
            }
            QueryExecutor obj_QueryExecutor = new QueryExecutor();
           isInserted= obj_QueryExecutor.insertObjectIntoTable(values, procedureName);

            return isInserted;
        }

       
    }
}
