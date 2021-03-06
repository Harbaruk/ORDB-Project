﻿using Microsoft.AspNetCore.Mvc;
using Starter.Common.DomainTaskStatus;
using Starter.DAL.Entities;
using Starter.Services.ADOServices;

namespace Starter.API.Controllers
{
    [Produces("application/json")]
    [Route("api/worker")]
    public class WorkerController : AbstractController
    {
        private readonly IBaseService<WorkerEntity> _workerService;

        public WorkerController(IBaseService<WorkerEntity> workerService, DomainTaskStatus taskStatus) : base(taskStatus)
        {
            _workerService = workerService;
        }

        [HttpGet]
        [Route("get_all")]
        [ProducesResponseType(typeof(WorkerEntity), 200)]
        public IActionResult GetWorkers()
        {
            return Ok(_workerService.GetAll());
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Insert([FromBody] WorkerEntity worker)
        {
            _workerService.Insert(worker);
            return Ok();
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromBody] WorkerEntity worker)
        {
            _workerService.Update(worker);
            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            _workerService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(WorkerEntity), 200)]
        public IActionResult Get(int id)
        {
            return Ok(_workerService.Get(id));
        }
    }
}