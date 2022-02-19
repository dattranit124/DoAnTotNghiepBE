using DoAnTotNghiep_CORE.Entities;
using DoAnTotNghiep_CORE.Interfaces.Repository.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep_API.API
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController :ControllerBase
    {
        protected IAccountRepository _accountRepository ;
        
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Login([FromBody] Account account)
        {
            try
            {
                var objToken = _accountRepository.Authenticate(account.Username, account.Password);
                return Ok(objToken);
            }
            catch (Exception ex)
            {

                return HadleExceptionResult(ex);
            }
            
        }
        [AllowAnonymous]
        //[Authorize]
        [HttpPost]
        public IActionResult Register([FromBody] Customer customer)
        {
            try
            {
                var objResult = _accountRepository.Insert(customer);
                if (objResult.IsSuccess)
                {
                    return Ok(objResult);
                }
                else return BadRequest(objResult);

            }
            catch (Exception ex)
            {

                return HadleExceptionResult(ex);
            }
        }

        private ObjectResult HadleExceptionResult(Exception ex)
        {
            var err = new
            {
                devMsg = ex.Message,
                userMsg = "Có lỗi ! Vui lòng liên hệ Admin"
            };
            return StatusCode(500, err);
        }
        

    }
}
