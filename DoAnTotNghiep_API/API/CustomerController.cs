using DoAnTotNghiep_CORE.Entities;
using DoAnTotNghiep_CORE.Helpers;
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
                var result = _customerRepository.Update(customer,id);
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
    }
}
