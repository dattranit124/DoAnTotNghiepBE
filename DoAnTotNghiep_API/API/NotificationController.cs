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
    [Authorize(Roles ="Admin")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        protected INotificationRepository _notificationRepository;
        public NotificationController(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = _notificationRepository.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Helper.HadleExceptionResult(ex));
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var result = _notificationRepository.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, Helper.HadleExceptionResult(ex));
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Create([FromBody] Notification notification)
        {
            try
            {
                var result = _notificationRepository.Insert(notification);
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
        [HttpDelete]
        public IActionResult DeleteByid([FromBody] List<string> id)
        {
            try
            {

                var result = _notificationRepository.DeleteById(id);
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
