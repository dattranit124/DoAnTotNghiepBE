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
    public class OrderRepository : IOrderRepository
    {

        protected IMongoDatabase _mongoConnect;
        protected IConfiguration _configuration;
        protected ServiceResult serviceResult;
        public OrderRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongoConnect = new MongoClient(_configuration.GetConnectionString("CuaHangThuyHang")).GetDatabase("CuaHangThuyHang");
            serviceResult = new ServiceResult();
        }

        public ServiceResult act(Order order)
        {   
           if(String.IsNullOrEmpty(order.OrderId))
            {
                order.OrderId = Helper.GenId();
                var ischeck = _mongoConnect.GetCollection<Order>("Order").InsertOneAsync(order);
                if(ischeck != null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.MSG = Resource.SuccessAdd;
                    serviceResult.Id = order.OrderId;
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
                var filter = Builders<Order>.Filter.Eq(g => g.OrderId,order.OrderId);
                var check = _mongoConnect.GetCollection<Order>("Order").ReplaceOneAsync(filter, order);
                if (check != null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.MSG = Resource.SuccessUpdate;
                    serviceResult.Id = order.OrderId;
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

        public ServiceResult DeleteById(List<string> ids)
        {
            var isOk = true;
            foreach (var id in ids)
            {
                var filter = Builders<Order>.Filter.Eq(e=>e.OrderId, id);
                var check = _mongoConnect.GetCollection<Order>("Order").DeleteOne(filter);
                if (check == null)
                {
                    isOk = false;
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

        public List<Order> get()
        {
            var  filter = Builders<Order>.Filter.Empty;
            var order = _mongoConnect.GetCollection<Order>("Order").Find(filter).SortByDescending(e=>e.OrderId).ToList();
            return order;
        }

        public Order getById(string id)
        {
            var Order = _mongoConnect.GetCollection<Order>("Order").Find(x => x.OrderId == id).FirstOrDefault();
            return Order;

        }

        public ServiceResult UpdateStatusOrder(int status, string orderId)
        {
            throw new NotImplementedException();
        }
    }
}
