using DoAnTotNghiep_CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTotNghiep_CORE.Interfaces.Repository.Manager
{
   public interface IOrderRepository
    {
        public ServiceResult UpdateStatusOrder(int status, string orderId);
        public List<Order> get();
        public ServiceResult DeleteById(List<string> id);
        public Order getById(string id);
        public ServiceResult act(Order order);

    }
}
