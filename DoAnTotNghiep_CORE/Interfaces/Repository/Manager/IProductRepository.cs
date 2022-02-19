using DoAnTotNghiep_CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTotNghiep_CORE.Interfaces.Repository.Manager
{
   public interface IProductRepository
    {
        public object GetByFilter(string searchText, string collectionId, int pageSize, int pageIndex);
        public Product GetById(string id);
        public ServiceResult DeleteById(List<string> id);
        public ServiceResult Insert(Product product);
        public ServiceResult Update(Product product, string id);
        public Product GetBySlug(string slug);
    }
}
