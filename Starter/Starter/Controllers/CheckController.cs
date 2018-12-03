using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Starter.Common.DomainTaskStatus;
using Starter.DAL.Entities;
using Starter.Services.ADOServices;

namespace Starter.API.Controllers
{
    [Produces("application/json")]
    [Route("api/check")]
    public class CheckController : AbstractController
    {
        private readonly IBaseService<CheckEntity> _service;

        public CheckController(IBaseService<CheckEntity> service, DomainTaskStatus taskStatus) : base(taskStatus)
        {
            _service = service;
        }

        [HttpGet]
        [Route("get_all")]
        [ProducesResponseType(typeof(IEnumerable<CheckEntity>), 200)]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet]
        [Route("id:int")]
        [ProducesResponseType(typeof(CheckEntity), 200)]
        public IActionResult Get(int id)
        {
            return Ok(_service.Get(id));
        }

        [HttpPost]
        public void Insert([FromBody] CheckEntity entity)
        {
            _service.Insert(entity);
        }

        [HttpPut]
        public void Update([FromBody] CheckEntity entity)
        {
            _service.Update(entity);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}