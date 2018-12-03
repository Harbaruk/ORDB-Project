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
    [Route("api/documentation/income")]
    public class IncomeReceiptController : AbstractController
    {
        private readonly IBaseService<IncomeReceiptEntity> _service;

        public IncomeReceiptController(IBaseService<IncomeReceiptEntity> service, DomainTaskStatus taskStatus) : base(taskStatus)
        {
            _service = service;
        }

        [HttpGet]
        [Route("get_all")]
        [ProducesResponseType(typeof(IEnumerable<IncomeReceiptEntity>), 200)]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(IncomeReceiptEntity), 200)]
        public IActionResult Get(int id)
        {
            return Ok(_service.Get(id));
        }

        [HttpPost]
        public IActionResult Insert([FromBody] IncomeReceiptEntity hrDocumentation)
        {
            _service.Insert(hrDocumentation);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] IncomeReceiptEntity hrDocumentation)
        {
            _service.Update(hrDocumentation);
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