using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTotNghiep_CORE.Entities
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string OrderId { get; set;}
        public string OderCustomerCheck { get; set; }
        
        public CustomerOrder CustomerOrder { get; set; }
        public List<Product> Product { get; set; }
        /// <summary>
        /// Status của đơn hàng
        /// 1 : Khởi tạo đơn hàng
        /// 2 : Tiếp nhận đơn hàng từ của hàng
        /// 3 : Đang tiến hành giao hàng
        /// 4 : Đã giao hàng
        /// </summary>
        public int OrderStatus { get; set; }
        public double TotalPrice { get; set; }
        public DateTime DateUpdate { get; set;}
    }
}
