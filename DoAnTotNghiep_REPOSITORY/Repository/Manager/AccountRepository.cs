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
                var customer = _mongoConnect.GetCollection<Customer>("Customer").Find(x => x.Account.Username == username && x.Account.Password == password).FirstOrDefault();
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() ),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("id", customer.CustomerId),


                };
                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]));

                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
                    return new
                    {
                        access_token = new JwtSecurityTokenHandler().WriteToken(token),
                        token_type = "bearer",
                        expires_in = ((DateTimeOffset)token.ValidTo).ToUnixTimeSeconds(),
                        isSuccess = true
                    };
            
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
                customer.Role ="customer";
                _mongoConnect.GetCollection<Customer>("Customer").InsertOneAsync(customer);
                //Mã hóa mật khẩu
                customer.Account.Password = Helper.Base64Encode(customer.Account.Password);
                //Gen id cho account
                 customer.Account.AccountId = Helper.GenId();
                _mongoConnect.GetCollection<Account>("Account").InsertOneAsync(customer.Account);
                serviceResult.IsSuccess = true;
                serviceResult.MSG = Resource.SuccessRegister;
                return serviceResult;
            }
        }
    }
}
