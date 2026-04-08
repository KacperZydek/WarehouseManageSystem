using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_do_zarządzania_magazynem.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string ProductName { get; set; } = "";
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Product(int ID,string Name, int quantity, decimal Price)
        {
            this.ID = ID;
            ProductName = Name;
            Quantity = quantity;
            this.Price = Price;
        }
        public Product(string Name, int quantity, decimal Price)
        {
            ProductName = Name;
            Quantity = quantity;
            this.Price = Price;
        }
        public Product() { }
    }
}
