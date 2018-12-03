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
    [Route("api/car")]
    public class CarController : AbstractController
    {
        private readonly IBaseService<CarEntity> _service;

        public CarController(IBaseService<CarEntity> service, DomainTaskStatus taskStatus) : base(taskStatus)
        {
            _service = service;
        }

        [HttpGet]
        [Route("get_all")]
        [ProducesResponseType(typeof(IEnumerable<CarEntity>), 200)]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(CarEntity), 200)]
        public IActionResult Get(int id)
        {
            return Ok(_service.Get(id));
        }

        [HttpPost]
        public IActionResult Insert([FromBody] CarEntity entity)
        {
            _service.Insert(entity);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] CarEntity entity)
        {
            _service.Update(entity);
            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}