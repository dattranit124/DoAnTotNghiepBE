using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTotNghiep_CORE.Entities
{
    public class Cart
    {
        public double TotalPrice { get; set; }
        public List<Product> Products { get; set; }

    }
}
