using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sklepik.Models
{
    public class Product
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Vat { get; set; }
        public bool IsHidden { get; set; }
        public string EmailExpert { get; set; }
        public byte[] Image { get; set; }
        public byte[] ImageSmall { get; set; }

    }
}