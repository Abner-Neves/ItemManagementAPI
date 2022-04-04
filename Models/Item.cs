using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemManagement.Models
{
    public class Item
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price{ get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
