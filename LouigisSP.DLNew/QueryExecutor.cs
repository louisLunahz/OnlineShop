using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LouigisSP.BO;
using System.Reflection;

namespace LouigisSP.DL
{
    public class QueryExecutor
    {
        string dataS = "ASPLAPLTM049\\SQLEXPRESS";
        string initialC = "Louigis";
        string userId = "josehz";
        string pass = "C9b2a317e8";
        public List<Object> retrieveTableFromDatabase(Type type, string tableName) {

            
            
            var declaredProperties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            var inheritedProps = type.BaseType.GetProperties();
            var props = inheritedProps.Concat(declaredProperties);

            List <Object>objects = new List<Object>();


            SqlConnectionStringBuilder sConnB = new SqlConnectionStringBuilder();
            sConnB.DataSource = dataS;
            sConnB.InitialCatalog = initialC;
            sConnB.UserID = userId;
            sConnB.Password = pass;
            using (SqlConnection conn = new SqlConnection(sConnB.ConnectionString))
            {


                Console.WriteLine(conn.ToString());
                using (SqlCommand cmd = new SqlCommand("selectEverythingFromAnyTable", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tableName", tableName);
                   
                    try
                    {
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            
                            string[] parameters = new string[dr.FieldCount];
                            for(int i=0; i<dr.FieldCount; i++) {
                                parameters[i] = dr[i].ToString();
                            }
                            var instance = Activator.CreateInstance(type, parameters);
                            objects.Add(instance);

                        }
                        Console.WriteLine("Records retrieved Successfully");
                        

                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine("Error Generated. Details: " + e.ToString());
                        
                    }

                }
            }
            return objects;
        }

        public Object retrieveUserFromDatabase(Type type, string email, string password, string storeProcedureName)
        {

            Object instance = null;
            var declaredProperties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            var inheritedProps = type.BaseType.GetProperties();
            var props = inheritedProps.Concat(declaredProperties);



            SqlConnectionStringBuilder sConnB = new SqlConnectionStringBuilder();
            sConnB.DataSource = dataS;
            sConnB.InitialCatalog = initialC;
            sConnB.UserID = userId;
            sConnB.Password = pass;
            using (SqlConnection conn = new SqlConnection(sConnB.ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand(storeProcedureName, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@pass", password);

                    try
                    {
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (!dr.Read())
                        {
                            Console.WriteLine("No records were returned");
                        }
                        else {
                            string[] parameters = new string[dr.FieldCount];
                            for (int i = 0; i < dr.FieldCount; i++)
                            {
                              parameters[i] = dr[i].ToString();
                            }
                            instance = Activator.CreateInstance(type, parameters);
                        }
                       
                       

                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine("Error Generated. Details: " + e.ToString());

                    }

                }
            }
             return instance; 
        }

        public bool insertObjectIntoTable(Tuple<string, string>[] values, string storedProcedureName) {
            bool isInserted = false;
            SqlConnectionStringBuilder sConnB = new SqlConnectionStringBuilder();
            sConnB.DataSource = dataS;
            sConnB.InitialCatalog = initialC;
            sConnB.UserID = userId;
            sConnB.Password = pass;

            using (SqlConnection conn = new SqlConnection(sConnB.ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    for (int i=0; i<values.Length; i++) { 
                        cmd.Parameters.AddWithValue("@"+values[i].Item1, values[i].Item2);
                       // Console.WriteLine("@"+values[i].Item1, values[i].Item2);
                    }



                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        
                        if (rowsAffected >= 1)
                        {
                            isInserted = true;
                        }
                        else isInserted = false;

                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine("Error Generated. Details: " + e.ToString());
                        isInserted = false;
                    }

                }
            }

            return isInserted;
        }

        public bool InsertMultipleRowsIntoTable(List<Tuple<string, string>[] > listRows, string storedProcedureName) {
            bool isInserted = false;
            SqlConnectionStringBuilder sConnB = new SqlConnectionStringBuilder();
            sConnB.DataSource = dataS;
            sConnB.InitialCatalog = initialC;
            sConnB.UserID = userId;
            sConnB.Password = pass;
            int rowsAffected = 0;

            using (SqlConnection conn = new SqlConnection(sConnB.ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
                {
                    conn.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    for (int i=0; i<listRows.Count(); i++) {
                        Tuple<string, string>[] values = listRows.ElementAt(i);
                        for (int j=0; j< values.Length; j++) {
                            cmd.Parameters.AddWithValue("@" + values[j].Item1, values[j].Item2);
                        }


                        try
                        {
                            int ra = cmd.ExecuteNonQuery();
                            if (ra >= 1)
                            {
                                rowsAffected++;
                            }
                        }
                        catch (SqlException e)
                        {
                            Console.WriteLine("Error Generated. Details: " + e.ToString());
                            isInserted = false;
                        }
                        finally {
                            cmd.Parameters.Clear();
                        }
                    }

                   

                }
            }
            if (rowsAffected==listRows.Count()) {
                isInserted = true;
            }

            return isInserted;

        }


    }
}
