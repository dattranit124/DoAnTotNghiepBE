using DoAnTotNghiep_CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTotNghiep_CORE.Interfaces.Repository.Manager
{
    public interface ICustomerRepository
    {
       public List<Customer> GetByFilter(string searchText, int pageSize, int pageIndex, string orderId);
        public Customer GetById(string id);
        public List<Customer> GetCustomer();

        public ServiceResult Delete(List<string> id);
        public ServiceResult Update(Customer customer, string id);
        public ServiceResult UpDateCart(Cart cart, string id);
        public ServiceResult UpDateOrder(Order order, string id);
        public ServiceResult act(Customer customer);
    }
}
