using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_do_zarządzania_magazynem.Models
{
    public class Log
    {
        public string Action { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public int QuantityChange { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}
