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
    public class PageRepository : IPageRepository
    {
        protected IMongoDatabase _mongoConnect;
        protected IConfiguration _configuration;
        protected ServiceResult serviceResult;
        public PageRepository(IConfiguration configuration)
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
                var filter = Builders<Page>.Filter.Eq(e=>e.PageId, id);
                var check = _mongoConnect.GetCollection<Page>("Page").DeleteOne(filter);
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

        public List<Page> GetAll()
        {
            var list = _mongoConnect.GetCollection<Page>("Page").AsQueryable();
            return list.ToList();
        }

        public Page GetById(string id)
        {
            var collection = _mongoConnect.GetCollection<Page>("Page").Find(x => x.PageId == id).FirstOrDefault();
            return collection;
        }

        public Page GetBySlug(string slug)
        {
            var collection = _mongoConnect.GetCollection<Page>("Page").Find(x => x.PageSlug == slug).FirstOrDefault();
            return collection;
        }

        public ServiceResult Insert(Page page)
        {
            var existsSlug = false;
            int numberSlug = 0;
            page.PageSlug = Helper.GenSlug(page.Title);
            page.DateCreated = DateTime.Now;
            var checkSlug = _mongoConnect.GetCollection<Page>("Page").Find(x => x.PageSlug.Contains(page.PageSlug)).ToList();

            if (checkSlug.Count > 0)
            {
                existsSlug = true;
            }
            if (existsSlug)
            {
                var stringSlug = checkSlug[checkSlug.Count - 1].PageSlug.Substring(checkSlug[checkSlug.Count - 1].PageSlug.Length - 1);
                var checkNumber = int.TryParse(stringSlug, out numberSlug);
                if (checkNumber)
                {
                    page.PageSlug = page.PageSlug + "-" + (numberSlug + 1);
                }
                else page.PageSlug = page.PageSlug + "-" + 1;
            }
            if (String.IsNullOrEmpty(page.PageId))
            {

                page.PageId = Helper.GenId();
                var check = _mongoConnect.GetCollection<Page>("Page").InsertOneAsync(page);
                if (check != null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.MSG = Resource.SuccessAdd;
                    serviceResult.Id = page.PageId;
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
                var filter = Builders<Page>.Filter.Eq(g => g.PageId, page.PageId);
                var check = _mongoConnect.GetCollection<Page>("Page").ReplaceOneAsync(filter, page);
                if (check != null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.MSG = Resource.SuccessUpdate;
                    serviceResult.Id = page.PageId;
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

        public ServiceResult Update(Page page, string id)
        {
            var filter = Builders<Page>.Filter.Eq(g => g.PageId, id);
            var numberSlug = 0;
            var existsSlug = false;
            page.PageSlug = Helper.GenSlug(page.Title);
            page.DateCreated = DateTime.Now;
            var checkSlug = _mongoConnect.GetCollection<Page>("Page").Find(x => x.PageSlug.Contains(page.PageSlug)).ToList();

            if (checkSlug.Count > 1)
            {
                existsSlug = true;
            }
            if (existsSlug)
            {
                var stringSlug = checkSlug[checkSlug.Count - 1].PageSlug.Substring(checkSlug[checkSlug.Count - 1].PageSlug.Length - 1);
                var checkNumber = int.TryParse(stringSlug, out numberSlug);
                if (checkNumber)
                {
                    page.PageSlug = page.PageSlug + "-" + (numberSlug + 1);
                }
                else page.PageSlug = page.PageSlug + "-" + 1;
            }
            var check = _mongoConnect.GetCollection<Page>("Page").ReplaceOneAsync(filter, page);
            if (check != null)
            {
                serviceResult.IsSuccess = true;
                serviceResult.MSG = Resource.SuccessUpdate;
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
