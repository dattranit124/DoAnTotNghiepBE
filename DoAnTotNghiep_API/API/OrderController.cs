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
    public class OrderController : ControllerBase
    {
        protected IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        [HttpPost]
        public IActionResult act(Order order)
        {
            try
            {
                var res = _orderRepository.act(order);
                return Ok(res);
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
                var res = _orderRepository.get();
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
                var res = _orderRepository.DeleteById(id);
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
                var res = _orderRepository.getById(id);
                return Ok(res);

            }
            catch (Exception ex)
            {
                return StatusCode(500, Helper.HadleExceptionResult(ex));
            }
        }

    }
}
