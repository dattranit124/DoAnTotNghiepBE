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
    public class ProductRepository : IProductRepository
    {
        protected IMongoDatabase _mongoConnect;
        protected IConfiguration _configuration;
        protected ServiceResult serviceResult;
        public ProductRepository(IConfiguration configuration)
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
                var filter = Builders<Product>.Filter.Eq("ProductId", id);
                var check = _mongoConnect.GetCollection<Product>("Product").DeleteOne(filter);
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

        public object GetByFilter(string searchText, string collectionId, int pageSize, int pageIndex)
        {
            var filter = Builders<Product>.Filter.Empty;
            if (!string.IsNullOrEmpty(searchText))
            {
                filter &= Builders<Product>.Filter.Where(x => x.ProductName.ToLower().Contains(searchText.ToLower()));
            }
            if (!string.IsNullOrEmpty(collectionId))
            {
                filter &= Builders<Product>.Filter.Eq(x => x.Collection.CollectionId, collectionId);
            }
            if(pageSize==0)
            {
                pageSize = 20;
            }
            if(pageIndex==0)
            {
                pageIndex = 1;
            }    
            var project = Builders<Product>.Projection.Include(x => x.ProductName).Include(x=>x.Price).Include(X=>X.Total).Include(x=>x.DateCreated).Include(x=>x.Description).Include(x=>x.Image);
            var sort = Builders<Product>.Sort.Descending("ProductId");
            var list = _mongoConnect.GetCollection<Product>("Product").Find(filter).Project<Product>(project).Sort(sort).Limit(pageSize).Skip(pageSize * (pageIndex - 1)).ToList();
            var totalPage = 0;
            if(list.Count%pageSize ==0)
            {
                totalPage = list.Count / pageSize;
            }
            else
            {
                totalPage = list.Count / pageSize + 1;
            }
            return new {product = list, totalpage = totalPage };
        }

        public Product  GetById(string id)
        {
            var product = _mongoConnect.GetCollection<Product>("Product").Find(x => x.ProductId == id).FirstOrDefault();
            return product;
        }

        public Product GetBySlug(string slug)
        {
            var product = _mongoConnect.GetCollection<Product>("Product").Find(x => x.Slug == slug).FirstOrDefault();
            return product;
        }

        public ServiceResult Insert(Product product)
        {
            var existsSlug = false;
            int numberSlug = 0;
            product.ProductId = Helper.GenId();
            product.Slug = Helper.GenSlug(product.ProductName);
            product.DateCreated = DateTime.Now;
            var checkSlug = _mongoConnect.GetCollection<Product>("Product").Find(x => x.Slug.Contains(product.Slug)).ToList();

            if (checkSlug.Count > 0)
            {
                existsSlug = true;
            }
            if (existsSlug)
            {
                var stringSlug = checkSlug[checkSlug.Count - 1].Slug.Substring(checkSlug[checkSlug.Count - 1].Slug.Length - 1);
                var checkNumber = int.TryParse(stringSlug, out numberSlug);
                if (checkNumber)
                {
                    product.Slug = product.Slug + "-" + (numberSlug + 1);
                }
                else product.Slug = product.Slug + "-" + 1;
            }

            var check = _mongoConnect.GetCollection<Product>("Product").InsertOneAsync(product);
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

        public ServiceResult Update(Product product, string id)
        {
            var filter = Builders<Product>.Filter.Eq(g => g.ProductId, id);
            var numberSlug = 0;
            var existsSlug = false;
            product.Slug = Helper.GenSlug(product.ProductName);
            product.DateCreated = DateTime.Now;
            var checkSlug = _mongoConnect.GetCollection<Product>("Product").Find(x => x.Slug.Contains(product.Slug)).ToList();

            if (checkSlug.Count > 1)
            {
                existsSlug = true;
            }
            if (existsSlug)
            {
                var stringSlug = checkSlug[checkSlug.Count - 1].Slug.Substring(checkSlug[checkSlug.Count - 1].Slug.Length - 1);
                var checkNumber = int.TryParse(stringSlug, out numberSlug);
                if (checkNumber)
                {
                    product.Slug = product.Slug + "-" + (numberSlug + 1);
                }
                else product.Slug = product.Slug + "-" + 1;
            }
            var check = _mongoConnect.GetCollection<Product>("Product").ReplaceOneAsync(filter, product);
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
