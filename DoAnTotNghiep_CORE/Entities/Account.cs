using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTotNghiep_CORE.Entities
{
    public class Account
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        /// <summary>
        /// Id bài tập
        /// </summary>
        public string AccountId { get; set; }
        public string Username{ get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
    }
}
