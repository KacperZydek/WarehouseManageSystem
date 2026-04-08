using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System_do_zarządzania_magazynem.Models;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace System_do_zarządzania_magazynem.Data
{
    public class ProductRepository
    {
        Database db = new Database();
        
        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Products", conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product((int)reader["Id"], reader["Name"].ToString(), (int)reader["Quantity"], (Decimal)reader["Price"]));
                    }
                }


            }
            return products;
        }
        public void DeleteProduct(Product product)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
             
                SqlCommand cmd = new SqlCommand("DELETE FROM Products WHERE Id=@id", conn);
                cmd.Parameters.AddWithValue("@id", product.ID);
                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }
        public void AddProduct(Product product)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Products(Name, Quantity, Price) VALUES (@name,@quantity,@price)", conn);
                cmd.Parameters.AddWithValue("@name", product.ProductName);
                cmd.Parameters.AddWithValue("@quantity", product.Quantity);
                cmd.Parameters.AddWithValue("@price", product.Price);

                cmd.ExecuteNonQuery();  
            }
        }
        public void EditProduct(Product product)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("UPDATE Products SET Name=@name, Quantity=@quantity, Price=@price WHERE Id=@id", conn);
                cmd.Parameters.AddWithValue("@name", product.ProductName);
                cmd.Parameters.AddWithValue("@quantity", product.Quantity);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@id", product.ID);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
