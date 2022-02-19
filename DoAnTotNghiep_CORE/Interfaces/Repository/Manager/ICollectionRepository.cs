using DoAnTotNghiep_CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTotNghiep_CORE.Interfaces.Repository.Manager
{
   public interface ICollectionRepository 
    {
        public List<Collection> GetAll();
        public Collection GetById(string id);
        public ServiceResult DeleteById(List<string> id);
        public ServiceResult Insert(Collection collection);
        public ServiceResult Update(Collection collection, string id);
 
    }
}
