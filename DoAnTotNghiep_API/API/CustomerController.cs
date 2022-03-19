using DoAnTotNghiep_CORE.Entities;
using DoAnTotNghiep_CORE.Helpers;
using DoAnTotNghiep_CORE.Interfaces.Repository.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DoAnTotNghiep_API.API
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        protected ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Update([FromBody] Customer customer, string id)
        {
            try
            {
                var result = _customerRepository.Update(customer, id);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else return BadRequest(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, Helper.HadleExceptionResult(ex));
            }
        }
        [HttpGet("me")]
        public IActionResult GetMe()
        {
            try
            {
                var id = "123";
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    var claims = identity.Claims.ToArray();
                    id = claims[3].Value;

                }
                var customer = _customerRepository.GetById(id);
                return Ok(customer);

            }
            catch (Exception ex)
            {

                return StatusCode(500, Helper.HadleExceptionResult(ex));
            }
        }
        [AllowAnonymous]
        [HttpPost("Cart")]
        public IActionResult UpdateCart([FromBody] Cart cart, string id)
        {
            try
            {
                var result = _customerRepository.UpDateCart(cart, id);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else return BadRequest(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, Helper.HadleExceptionResult(ex));
            }
        }
        [AllowAnonymous]
        [HttpPost("Order/{id}")]
        public IActionResult CreateOrder([FromBody] Order order, string id)
        {
            try
            {
                var result = _customerRepository.UpDateOrder(order, id);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else return BadRequest(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, Helper.HadleExceptionResult(ex));
            }
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var res = _customerRepository.GetCustomer();
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, Helper.HadleExceptionResult(ex));
            }
        }
        [HttpDelete]
        public IActionResult Delete([FromBody] List<string> id)
        {
            try
            {
                var res = _customerRepository.Delete(id);
                if (res.IsSuccess)
                {
                    return Ok(res);
                }
                else return BadRequest(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, Helper.HadleExceptionResult(ex));
            }
        }

        [HttpGet("{id}")]
        public IActionResult get(string id)
        {
            try
            {
                var res = _customerRepository.GetById(id);
                return Ok(res);
                    
            }
            catch (Exception ex)
            {
                return StatusCode(500, Helper.HadleExceptionResult(ex));
            }
        }
        [HttpPost("act")]
        public IActionResult act(Customer customer)
        {
            try
            {
                var res = _customerRepository.act(customer);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, Helper.HadleExceptionResult(ex));
            }
        }
        [AllowAnonymous]
        [HttpGet("orderId")]
        public IActionResult get(string searchText, int pageSize, int pageIndex, string orderId)
        {
            var res = _customerRepository.GetByFilter(searchText, pageSize, pageIndex, orderId);
            return Ok(res);
        }
    }
}
