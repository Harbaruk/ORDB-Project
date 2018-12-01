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
    [Route("api/dispatcher")]
    public class DispatcherController : AbstractController
    {
        private readonly IBaseService<DispatcherEntity> _dispatcherService;

        public DispatcherController(IBaseService<DispatcherEntity> workerService, DomainTaskStatus taskStatus) : base(taskStatus)
        {
            _dispatcherService = workerService;
        }

        [HttpGet]
        [Route("get_all")]
        [ProducesResponseType(typeof(DispatcherEntity), 200)]
        public IActionResult GetWorkers()
        {
            return Ok(_dispatcherService.GetAll());
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Insert([FromBody] DispatcherEntity worker)
        {
            _dispatcherService.Insert(worker);
            return Ok();
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromBody] DispatcherEntity worker)
        {
            _dispatcherService.Update(worker);
            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            _dispatcherService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(DispatcherEntity), 200)]
        public IActionResult Get(int id)
        {
            return Ok(_dispatcherService.Get(id));
        }
    }
}