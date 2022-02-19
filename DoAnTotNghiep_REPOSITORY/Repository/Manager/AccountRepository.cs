using DoAnTotNghiep_CORE.Entities;
using DoAnTotNghiep_CORE.Helpers;
using DoAnTotNghiep_CORE.Interfaces.Repository.Manager;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTotNghiep_REPOSITORY.Repository.Manager
{
    public class AccountRepository : IAccountRepository
    {
        protected IMongoDatabase _mongoConnect;
        protected IConfiguration _configuration;
        protected  string key;
        protected ServiceResult serviceResult;
        public AccountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongoConnect = new MongoClient(_configuration.GetConnectionString("CuaHangThuyHang")).GetDatabase("CuaHangThuyHang");
            key = _configuration.GetSection("JwtKey").ToString();
            serviceResult = new ServiceResult();
        }
        public object Authenticate(string username, string password)
        {
            var base64Password = Helper.Base64Encode(password);
            var user = _mongoConnect.GetCollection<Account>("Account").Find(x => x.Username == username && x.Password == base64Password).FirstOrDefault();
            if(user == null)
            {
                return new
                {
                    isSuccess = false,
                    msg_error = Resource.FailLogin

                };
            }
            else
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(key);
                var tokenDescriptor = new SecurityTokenDescriptor()

                {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Email, username),
                    }),
                    Expires = DateTime.UtcNow.AddHours(24),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
            if (user.Role == 0)
                {
                    return new
                    {
                        access_token = tokenHandler.WriteToken(token),
                        role = "admin",
                        isSuccess = true,
                    };
                }
            else
                {
                    return new
                    {
                        access_token=tokenHandler.WriteToken(token),
                        role="user",
                        isSuccess=true,
                    };
                }
            }
        }

        public ServiceResult Insert(Customer customer)
        {
            var isExistsCustomer = _mongoConnect.GetCollection<Customer>("Customer").Find(x => x.Account.Username == customer.Account.Username).FirstOrDefault();

            if(isExistsCustomer != null)
            {
                serviceResult.MSG = Resource.FailRegister; 
                return serviceResult;
            }
            else
            {
                //Gen Id cho khach hang
                customer.CustomerId = Helper.GenId();
                _mongoConnect.GetCollection<Customer>("Customer").InsertOneAsync(customer);
                //Mã hóa mật khẩu
                customer.Account.Password = Helper.Base64Encode(customer.Account.Password);
                //Gen id cho account
                 customer.Account.AccountId = Helper.GenId();
                //Role account auto : 1
                customer.Account.Role = 1;
                _mongoConnect.GetCollection<Account>("Account").InsertOneAsync(customer.Account);
                serviceResult.IsSuccess = true;
                serviceResult.MSG = Resource.SuccessRegister;
                return serviceResult;
            }
        }
    }
}
