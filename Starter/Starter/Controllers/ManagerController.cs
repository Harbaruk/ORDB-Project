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
    [Route("api/manager")]
    public class ManagerController : AbstractController
    {
        private readonly IBaseService<ManagerEntity> _managerService;

        public ManagerController(IBaseService<ManagerEntity> workerService, DomainTaskStatus taskStatus) : base(taskStatus)
        {
            _managerService = workerService;
        }

        [HttpGet]
        [Route("get_all")]
        [ProducesResponseType(typeof(WorkerEntity), 200)]
        public IActionResult GetWorkers()
        {
            return Ok(_managerService.GetAll());
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Insert([FromBody] ManagerEntity worker)
        {
            _managerService.Insert(worker);
            return Ok();
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromBody] ManagerEntity worker)
        {
            _managerService.Update(worker);
            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            _managerService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(WorkerEntity), 200)]
        public IActionResult Get(int id)
        {
            return Ok(_managerService.Get(id));
        }
    }
}