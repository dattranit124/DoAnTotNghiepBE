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
    public class CollectionController : ControllerBase
    {
        protected ICollectionRepository _collectionRepository;

        public CollectionController(ICollectionRepository collectionRepository)
        {
            _collectionRepository = collectionRepository;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
              var result =  _collectionRepository.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500,Helper.HadleExceptionResult(ex));
            }
        } 
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var result = _collectionRepository.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, Helper.HadleExceptionResult(ex));
            }
        }
        [HttpPost]
        public IActionResult act([FromBody] Collection collection)
        {
            try
            {
                var result = _collectionRepository.Insert(collection);
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
        public IActionResult Update([FromBody] Collection collection, string id)
        {
            try
            {
                var result = _collectionRepository.Update(collection,id);
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

            var result = _collectionRepository.DeleteById(id);
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
