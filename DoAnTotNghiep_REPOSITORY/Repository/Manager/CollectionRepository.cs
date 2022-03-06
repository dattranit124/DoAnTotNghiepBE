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
  public  class CollectionRepository : ICollectionRepository
    {
        protected IMongoDatabase _mongoConnect;
        protected IConfiguration _configuration;
        protected ServiceResult serviceResult;
        public CollectionRepository(IConfiguration configuration)
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
            var filter = Builders<Collection>.Filter.Eq("CollectionId", id);
            var check = _mongoConnect.GetCollection<Collection>("Collection").DeleteOne(filter);
                if(check == null)
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

        public List<Collection> GetAll()
        {
          var list =  _mongoConnect.GetCollection<Collection>("Collection").AsQueryable();
            return list.ToList();
        }

        public Collection GetById(string id)
        {
            var collection = _mongoConnect.GetCollection<Collection>("Collection").Find(x => x.CollectionId == id).FirstOrDefault();
            return collection;
        }

        public ServiceResult Insert(Collection collection)
        {
            var existsSlug = false;
            var numberSlug = 0;
            
            collection.CollectionSlug = Helper.GenSlug(collection.CollectionName);
            var checkSlug = _mongoConnect.GetCollection<Collection>("Collection").Find(x => x.CollectionSlug.Contains(collection.CollectionSlug)).ToList();
            
            if(checkSlug.Count>1)
            {
                existsSlug = true;
            }
            if(existsSlug)
            {
                var stringSlug = checkSlug[checkSlug.Count - 1].CollectionSlug.Substring(checkSlug[checkSlug.Count - 1].CollectionSlug.Length - 1);
                var checkNumber = int.TryParse(stringSlug, out numberSlug);
                if (checkNumber)
                {
                    collection.CollectionSlug = collection.CollectionSlug + "-" + (numberSlug + 1);
                }
                else collection.CollectionSlug = collection.CollectionSlug + "-" + 1;
            }    
            if(String.IsNullOrEmpty(collection.CollectionId))
            {
                collection.CollectionId = Helper.GenId();
                 var check = _mongoConnect.GetCollection<Collection>("Collection").InsertOneAsync(collection);
                if (check != null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.MSG = Resource.SuccessAdd;
                    serviceResult.Id = collection.CollectionId;
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
                var filter = Builders<Collection>.Filter.Eq(g => g.CollectionId, collection.CollectionId);
                var check = _mongoConnect.GetCollection<Collection>("Collection").ReplaceOneAsync(filter, collection);
                if (check != null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.MSG = Resource.SuccessUpdate;
                    serviceResult.Id = collection.CollectionId;
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

        public ServiceResult Update(Collection collection, string id)
        {
           var filter = Builders<Collection>.Filter.Eq(g => g.CollectionId, id);
            var numberSlug = 0;
            var existsSlug = false;
            collection.CollectionSlug = Helper.GenSlug(collection.CollectionName);
            var checkSlug = _mongoConnect.GetCollection<Collection>("Collection").Find(x => x.CollectionSlug.Contains(collection.CollectionSlug)).ToList();

            if (checkSlug.Count > 1)
            {
                existsSlug = true;
            }
            if (existsSlug)
            {
                var stringSlug = checkSlug[checkSlug.Count - 1].CollectionSlug.Substring(checkSlug[checkSlug.Count - 1].CollectionSlug.Length - 1);
                var checkNumber = int.TryParse(stringSlug, out numberSlug);
                if (checkNumber)
                {
                    collection.CollectionSlug = collection.CollectionSlug + "-" + (numberSlug + 1);
                }
                else collection.CollectionSlug = collection.CollectionSlug + "-" + 1;
            }
            var check = _mongoConnect.GetCollection<Collection>("Collection").ReplaceOneAsync(filter, collection);
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
