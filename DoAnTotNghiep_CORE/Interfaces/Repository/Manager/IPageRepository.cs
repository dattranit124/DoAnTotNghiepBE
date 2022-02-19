using DoAnTotNghiep_CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTotNghiep_CORE.Interfaces.Repository.Manager
{
   public interface IPageRepository
    {
        public List<Page> GetAll();
        public Page GetById(string id);
        public ServiceResult DeleteById(List<string> id);
        public ServiceResult Insert(Page page);
        public ServiceResult Update(Page page,string id);
        public Page GetBySlug(string slug);
    }
}
