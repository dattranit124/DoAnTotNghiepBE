using DoAnTotNghiep_CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTotNghiep_CORE.Interfaces.Repository.Manager
{
 public interface IAccountRepository
    {
        public Object Authenticate(string username, string password);

        public ServiceResult Insert(Customer customer);

    }
}
