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
   public class NotificationRepository : INotificationRepository
    {
        protected IMongoDatabase _mongoConnect;
        protected IConfiguration _configuration;
        protected ServiceResult serviceResult;
        public NotificationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongoConnect = new MongoClient(_configuration.GetConnectionString("CuaHangThuyHang")).GetDatabase("CuaHangThuyHang");
            serviceResult = new ServiceResult();
        }
        public ServiceResult DeleteById(List<string> ids)
        {
            var isOk = true;
            foreach (var id in ids)
            {
                var filter = Builders<Notification>.Filter.Eq("NotificationId", id);
                var check = _mongoConnect.GetCollection<Notification>("Notification").DeleteOne(filter);
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

        public List<Notification> GetAll()
        {
            var list = _mongoConnect.GetCollection<Notification>("Notification").AsQueryable();
            return list.ToList();
        }

        public Notification GetById(string id)
        {
            var notification = _mongoConnect.GetCollection<Notification>("Notification").Find(x => x.NotificationId == id).FirstOrDefault();
            return notification;
        }

        public ServiceResult Insert(Notification notification)
        {
            notification.NotificationId = Helper.GenId();

            var check = _mongoConnect.GetCollection<Notification>("Notification").InsertOneAsync(notification);
            if (check != null)
            {
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
    }
}
