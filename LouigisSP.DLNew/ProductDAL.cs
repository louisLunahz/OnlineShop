using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LouigisSP.BO;

namespace LouigisSP.DL
{
    public class ProductDAL
    {
        string dataS = "ASPLAPLTM049\\SQLEXPRESS";
        string initialC = "Louigis";
        string userId = "josehz";
        string pass = "C9b2a317e8";
        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            SqlConnectionStringBuilder sConnB = new SqlConnectionStringBuilder();
            sConnB.DataSource = dataS;
            sConnB.InitialCatalog = initialC;
            sConnB.UserID = userId;
            sConnB.Password = pass;

            using (SqlConnection conn = new SqlConnection(sConnB.ConnectionString))
            {


                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM Products";
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        string id = dr["id"].ToString();
                        string name = dr["name"].ToString();
                        string brand = dr["brand"].ToString();
                        string model = dr["model"].ToString();
                        string color = dr["color"].ToString();
                        string price = dr["price"].ToString();
                        string stock = dr["stock"].ToString();

                        Product pro = new Product();

                        pro.Id = id;
                        pro.Name = name;
                        pro.Brand = brand;
                        pro.Model = model;
                        pro.Color = color;
                        pro.Price = float.Parse(price);
                        pro.Stock = int.Parse(stock);


                        products.Add(pro);


                    }

                }
            }

            return products;

        }
    }
}
