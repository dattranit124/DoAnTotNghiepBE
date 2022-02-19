using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTotNghiep_CORE.Entities
{
   public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Total { get; set; }
        public List<string> Size { get; set; }
        public List<string> Image { get; set; }
        public string Slug { get; set;}
        public Collection Collection { get; set; }
        public int Quanlity { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
