using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTotNghiep_CORE.Entities
{
  public  class Page
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string PageId { get; set; }
        public string Title { get; set; }
        public string  Content { get; set; }
        public string Image { get; set; }
        public string PageSlug { get; set; }
        public DateTime DateCreated { get; set; }
    }

}
