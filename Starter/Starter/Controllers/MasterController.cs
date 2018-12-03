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
    [Route("api/worker/specialist")]
    public class MasterController : AbstractController
    {
        private readonly IBaseService<SpecialistEntity> _service;

        public MasterController(IBaseService<SpecialistEntity> service, DomainTaskStatus taskStatus) : base(taskStatus)
        {
            _service = service;
        }

        [HttpGet]
        [Route("get_all")]
        [ProducesResponseType(typeof(IEnumerable<SpecialistEntity>), 200)]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(SpecialistEntity), 200)]
        public IActionResult Get(int id)
        {
            return Ok(_service.Get(id));
        }

        [HttpPost]
        public IActionResult Insert([FromBody] SpecialistEntity specialist)
        {
            _service.Insert(specialist);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] SpecialistEntity specialist)
        {
            _service.Update(specialist);
            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok();
        }
    }
}