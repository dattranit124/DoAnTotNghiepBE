using DoAnTotNghiep_CORE.Entities;
using DoAnTotNghiep_CORE.Helpers;
using DoAnTotNghiep_CORE.Interfaces.Repository.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        protected IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll(string searchText, string collectionId, int pageSize, int pageIndex)
        {

            try
            {
                var result = _productRepository.GetByFilter(searchText,collectionId,pageSize,pageIndex);
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
                var result = _productRepository.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, Helper.HadleExceptionResult(ex));
            }
        }
        [AllowAnonymous]
        [HttpGet("slug")]
        public IActionResult GetBySlug(string slug)
        {
            try
            {
                var result = _productRepository.GetBySlug(slug);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, Helper.HadleExceptionResult(ex));
            }
        }
        [HttpPost]
        public IActionResult act([FromBody] Product product)
        {
            try
            {
                var result = _productRepository.Insert(product);
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
        public IActionResult Update([FromBody] Product product, string id)
        {
            try
            {
                var result = _productRepository.Update(product, id);
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

                var result = _productRepository.DeleteById(id);
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
        [HttpGet("size")]
        public IActionResult GetBySize(string size)
        {
            var res = _productRepository.getBySize(size);
            return Ok(res);
        }

    }
}
