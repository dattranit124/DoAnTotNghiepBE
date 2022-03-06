using DoAnTotNghiep_CORE.Entities;
using DoAnTotNghiep_CORE.Helpers;
using DoAnTotNghiep_CORE.Interfaces.Repository.Manager;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTotNghiep_REPOSITORY.Repository.Manager
{
    public class CustomerRepository : ICustomerRepository
    {
        protected IMongoDatabase _mongoConnect;
        protected IConfiguration _configuration;
        protected ServiceResult serviceResult;
        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongoConnect = new MongoClient(_configuration.GetConnectionString("CuaHangThuyHang")).GetDatabase("CuaHangThuyHang");
            serviceResult = new ServiceResult();
        }
        public ServiceResult Delete(List<string> ids)
        {
            var isOk = true;
            foreach (var id in ids)
            {
                var cus = _mongoConnect.GetCollection<Customer>("Customer").Find(x => x.CustomerId == id.ToString()).FirstOrDefault();
                if(cus!=null)
                {

                //Xóa bên customer
                var filterCus = Builders<Customer>.Filter.Eq("CustomerId", id);
                    //Delete Order in Table Customer
                    var checkCus = _mongoConnect.GetCollection<Customer>("Customer").DeleteOne(filterCus);

                if (checkCus == null)
                {
                    isOk = false;
                }
                }
            }
            if (isOk)
            {
                serviceResult.IsSuccess = true;
                serviceResult.MSG = Resource.SuccessDelete;
                return serviceResult;
            }
            else
            {
                serviceResult.IsSuccess = false;
                serviceResult.MSG = Resource.FailDelete;
                return serviceResult;
            }
        }

        public object GetByFilter(string searchText, int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public Customer GetById(string id)
        {
            var customer = _mongoConnect.GetCollection<Customer>("Customer").Find(x => x.CustomerId == id).FirstOrDefault();
            return customer;
        }

        public List<Customer> GetCustomer()
        {
            var filter = Builders<Customer>.Filter.Empty;

            var list = _mongoConnect.GetCollection<Customer>("Customer").Find(filter).Skip(1).ToList();
            return list;
        }

        public ServiceResult Update(Customer customer,string id)
        {
            var checkCustomer = _mongoConnect.GetCollection<Customer>("Customer").Find(x => x.CustomerId == id).FirstOrDefault();
            var checkAccount = _mongoConnect.GetCollection<Account>("Account").Find(x => x.Username == customer.Account.Username).FirstOrDefault();
            if( checkCustomer != null && checkAccount != null)
            {
                _mongoConnect.GetCollection<Customer>("Customer").InsertOneAsync(customer);
                _mongoConnect.GetCollection<Account>("Account").InsertOneAsync(customer.Account);
                customer.Orders = checkCustomer.Orders;
                customer.Cart = checkCustomer.Cart;
                serviceResult.IsSuccess = true;
                serviceResult.MSG = Resource.SuccessAdd;
                return serviceResult;
            }    
           
            else
            {
                serviceResult.IsSuccess = false;
                serviceResult.MSG = Resource.FailAdd;
                return serviceResult;
            }

        }
        public ServiceResult UpDateCart(Cart cart, string id)
        {
            var customers = _mongoConnect.GetCollection<Customer>("Customer");
            var filter = Builders<Customer>.Filter.Eq(x => x.CustomerId, id);
            double total = 0;
            foreach (var item in cart.Products)
            {
                total = total + item.Price*item.Quanlity;
            }
            cart.TotalPrice = total;
            var update = Builders<Customer>.Update.Set(x => x.Cart, cart);
            customers.UpdateOneAsync(filter, update);
            serviceResult.IsSuccess = true;
            serviceResult.MSG = Resource.SuccessUpdate;
            return serviceResult;
        }

        public ServiceResult UpDateOrder(Order order, string id)
        {
            var customers = _mongoConnect.GetCollection<Customer>("Customer");
            order.OrderId = Helper.GenId();
            Random rdb = new Random();
            order.OderCustomerCheck = rdb.Next().ToString();
            order.DateUpdate = DateTime.Now;
            order.OrderStatus = 0;
            double total = 0;
            foreach (var item in order.Product)
            {
                total = total + item.Price * item.Quanlity;
            }
            order.TotalPrice = total;
            var filter = Builders<Customer>.Filter.Eq(x => x.CustomerId, id);
             var update = Builders<Customer>.Update.Push<String>(e => e.Orders, order.OrderId).Set(x=>x.Cart, null);
             customers.FindOneAndUpdateAsync(filter, update);
            _mongoConnect.GetCollection<Order>("Order").InsertOneAsync(order);
                serviceResult.IsSuccess = true;
                serviceResult.MSG = Resource.SuccessAdd + " Mã đơn hàng : " + order.OderCustomerCheck;
            return serviceResult;
           
        }

        public ServiceResult act(Customer customer)
        {

            if (String.IsNullOrEmpty(customer.CustomerId))
            {
                customer.CustomerId = Helper.GenId();
                var check = _mongoConnect.GetCollection<Customer>("Customer").InsertOneAsync(customer);
                if (check != null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.MSG = Resource.SuccessAdd;
                    serviceResult.Id = customer.CustomerId;
                    return serviceResult;
                }
                else
                {
                    serviceResult.IsSuccess = false;
                    serviceResult.MSG = Resource.FailAdd;
                    return serviceResult;
                }
            }
            else
            {
                var filter = Builders<Customer>.Filter.Eq(g => g.CustomerId, customer.CustomerId);
                var check = _mongoConnect.GetCollection<Customer>("Customer").ReplaceOneAsync(filter, customer);
                if (check != null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.MSG = Resource.SuccessUpdate;
                    serviceResult.Id = customer.CustomerId;
                    return serviceResult;
                }
                else
                {
                    serviceResult.IsSuccess = false;
                    serviceResult.MSG = Resource.FailUpdate;
                    return serviceResult;
                }

            }

        }
    }
}
