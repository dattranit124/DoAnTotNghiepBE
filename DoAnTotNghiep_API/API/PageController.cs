using DoAnTotNghiep_CORE.Entities;
using DoAnTotNghiep_CORE.Helpers;
using DoAnTotNghiep_CORE.Interfaces.Repository.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep_API.API
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PageController : ControllerBase
    {
        protected IPageRepository _pageRepository;
        public PageController(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = _pageRepository.GetAll();
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
                var result = _pageRepository.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, Helper.HadleExceptionResult(ex));
            }
        }
        [AllowAnonymous]
        [HttpGet("slug")]
        public IActionResult GetBySlug( string slug)
        {
            try
            {
                var result = _pageRepository.GetBySlug(slug);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, Helper.HadleExceptionResult(ex));
            }
        }
        [HttpPost]
        public IActionResult Create([FromBody] Page page)
        {
            try
            {
                var result = _pageRepository.Insert(page);
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

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] Page page, string id)
        {
            try
            {
                var result = _pageRepository.Update(page, id);
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

                var result = _pageRepository.DeleteById(id);
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
