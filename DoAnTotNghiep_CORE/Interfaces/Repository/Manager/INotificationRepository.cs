using DoAnTotNghiep_CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTotNghiep_CORE.Interfaces.Repository.Manager
{
   public interface INotificationRepository
    {
        public List<Notification> GetAll();
        public Notification GetById(string id);
        public ServiceResult DeleteById(List<string> id);
        public ServiceResult Insert(Notification notification);
    }
}
