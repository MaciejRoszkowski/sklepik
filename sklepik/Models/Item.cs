using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sklepik.Models
{
    public class Item
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public Item(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

    }
}